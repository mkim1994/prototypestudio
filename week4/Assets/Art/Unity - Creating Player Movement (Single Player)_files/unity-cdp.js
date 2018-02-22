// *******NOTE******
// Do not move file - used on multiple sites, hotlinking to store. Talk to @andreasp
// *******NOTE******
(function () {
"use strict";

// Find GTM script tag, and when loaded - load CDP script
$(function() {
  var $gtm_script = $('script[src*="gtm.js"]');

  if ($gtm_script.length === 0) {
    loadCDP();
  } else {
    $gtm_script.ready(delayLoadCDP);
    $gtm_script.on('error', delayLoadCDP);
    $gtm_script.on('load', delayLoadCDP);
    setTimeout(loadCDP, 1500);
  }
});

// Add a small delay to let GTM script execute JS code and instantiate variables
function delayLoadCDP() {
  setTimeout(loadCDP, 10);
}

function loadCDP() {
  if (window.cdp)
    return false;

  window.cdp = (function () {
    const SERVICE_NAME = 'unityWeb',
          SCHEMA_VERSION = 'v1',
          BASE_HOST = location.host.split('.').slice(-2).join('.'), // Will be 'unity3d.com' or 'unity.com', care for co.uk
          LOAD_TIME = new Date().getTime();

    var api = loadAPI();

    // Send initial page view event
    sendPageView();


    // Exposed functions
    function sendPageView(cb) {
      var common_header = getCommonHeader(),
          pageView = new PageViewEvent();

      api.send_events_with_retry(common_header, [pageView]).done(cb || (function () {
      }));
    }

    function sendInteraction(category, action, label, value, cb) {
      var common_header = getCommonHeader(),
          pageInteraction = new PageInteractionEvent(category, action, label, value, cb);

      api.send_events_with_retry(common_header, [pageInteraction]).done(cb || (function () {
      }));
    }

    // Event objects
    function Event(type) {
      this.type = [SERVICE_NAME, type, SCHEMA_VERSION].join('.');
      this.msg = {ts: new Date().getTime()};
    }

    function PageViewEvent() {
      Event.call(this, 'pageView');
      $.extend(this.msg, {
        referrer: document.referrer || null,
        lang: getLangCode(),
        ga_blocked: !(window.ga && window.ga.toString().indexOf('Z.D.apply') > -1),
        gtm_blocked: !window.google_tag_manager,
        utm_campaign: getCookie('utm_campaign'),
        utm_content: getCookie('utm_content'),
        utm_medium: getCookie('utm_medium'),
        utm_source: getCookie('utm_source'),
        utm_term: getCookie('utm_term')
      })
    }

    function PageInteractionEvent(category, action, label, value) {
      Event.call(this, 'pageInteraction');
      $.extend(this.msg, {
        delta_ts: new Date().getTime() - LOAD_TIME,
        category: category,
        action: action,
        label: label,
        value: value
      })
    }

    function getCommonHeader() {
      return {
        uuid: getUUID(),
        user_id: getUnityId(),
        domain: location.host,
        path: getCanonicalPath(),
        path_raw: location.pathname,
        title: document.title,
        query: location.search.substr(1) || undefined,
        fragment: location.hash || undefined
      };
    }

    // Data collector helpers
    function getUnityId() {
      var unity_id_cname = 'unityId' + (isStg() ? '_stg' : ''),
          unity_id = null;
      // Drupal 7
      if (window.UnityUser && UnityUser.unityId)
        unity_id = UnityUser.unityId;
      // Drupal 8
      if (window.CmsUser && CmsUser.unityId)
        unity_id = CmsUser.unityId;
      // Forum
      if (window.XenForo && XenForo.visitor && XenForo.visitor.unity_id)
        unity_id = XenForo.visitor.unity_id;
      // Cookie
      if (!unity_id)
        unity_id = getCookie(unity_id_cname);

      if (unity_id)
        setCookie(unity_id_cname, unity_id, 1000 * 60 * 60 * 24 * 365, '/', BASE_HOST); // Save unityId if defined for 1 year

      return unity_id;
    }

    function getUUID() {
      var uuid_cname = 'unityWebUUID' + (isStg() ? '_stg' : ''),
          uuid = getCookie(uuid_cname);

      // Generate unique id if none exist, and then save to cookie
      if (!uuid) {
        uuid = Math.random().toString(36).substring(2) + (new Date()).getTime().toString(36);
        setCookie(uuid_cname, uuid, 1000 * 60 * 60 * 24 * 365, '/', BASE_HOST);
      }

      return uuid;
    }

    function getLangCode() {
      var languages = getLanguages(),
          dir1 = location.pathname.split('/')[1],
          lang = 'en'; // English is the default language, if none is defined.

      if (languages.indexOf(dir1) > -1)
        lang = dir1;

      return lang;
    }

    function getCanonicalPath() {
      var languages = getLanguages(),
          path = location.pathname.split('/');

      // Remove langcode
      if (languages.indexOf(path[1]) > -1)
        path.splice(1, 1);

      // Remove trailing slash;
      if (path.length > 2 && path.slice(-1)[0] == '')
        path.pop();

      return path.join('/');
    }

    function getLanguages() {
      return ['cn', 'de', 'es', 'ja', 'ru', 'fr', 'kr', 'pt', 'en'];
    }

    function isStg() {
      var reg = /localhost|127\.0\.0\.1|hq\.unity3d\.com|staging\.unity\.com|int\.unity.com/;
      return location.host.match(reg);
    }

    function setCookie(name, value, ms, path, domain) {
      if (!name || !value)
        return;

      var d,
          cpath = path ? '; path=' + path : '',
          cdomain = domain ? '; domain=' + domain : '',
          expires = '',
          secure = location.protocol == 'https:' ? '; secure' : '';

      if (ms) {
        d = new Date();
        d.setTime(d.getTime() + ms);
        expires = '; expires=' + d.toUTCString();
      }
      document.cookie = name + "=" + value + expires + cpath + cdomain + secure;
    }

    function getCookie(name) {
      var value = document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)');
      return value ? value.pop() : null;
    }

    // CDP API
    function loadAPI() {
      return {
        self: this,
        getEndpoint: function () {
          var endpoint = 'https://prd-lender.cdp.internal.unity3d.com/v1/events';

          // If domain is staging development, switch to stg endpoint.
          if (isStg())
            endpoint = 'https://stg-lender.cdp.internal.unity3d.com/v1/events';

          return endpoint;
        },

        send_events: function (common, events) {
          var data = JSON.stringify({common: common}) + "\n";
          events.forEach(function (event) {
            data += JSON.stringify(event) + "\n";
          });
          return $.ajax({
            type: 'POST',
            contentType: 'application/json',
            url: this.getEndpoint(),
            data: data,
            dataType: 'text'
          });
        },

        _send_events_with_retry: function (common, events, max_retries, timeout, deferred) {
          var self = this;
          this.send_events(common, events)
              .done(deferred.resolve)
              .fail(function () {
                if (max_retries > 0) {
                  setTimeout(function () {
                    self._send_events_with_retry(common, events, max_retries - 1, timeout * 2, deferred);
                  }, timeout);
                } else {
                  deferred.reject();
                }
              });
        },

        send_events_with_retry: function (common, events, max_retries, timeout) {
          max_retries = max_retries || 3;
          timeout = timeout || 3;

          var deferred = $.Deferred();
          this._send_events_with_retry(common, events, max_retries, timeout, deferred);
          return deferred.promise();
        }
      }
    }

    return {
      sendPageView: sendPageView,
      sendInteraction: sendInteraction
    }

  })();
}

})();
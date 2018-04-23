
label time1start:
    scene white with fastdissolve
    window show
    "Did you know yourself back then?"
    menu:
        extend ""
        "yes":
            jump time1yes
        "no":
            jump time1no
    #me0 ""

    #jump time1end

label time1yes:
    scene white with fastdissolve
    "It is your sophomore year in high school."
    scene bg 1yes1 with fastdissolve
    "You always sit next to her on the bus."
    "Sometimes it feels as though your heart will seize up somehow, right next to her, and you know exactly why."
    "Today is no different. Except it is."
    scene bg 1yes2 with fastdissolve
    "\"How do you say I love you in Korean?\" she asks."
    scene bg 1yes3 with fastdissolve
    "You pause."
    "Surely she doesn't know about how your heart goes wild with every word exchanged with her."
    "You decide to humor her. She can't know, but what's the big deal in pretending for a bit?"
    scene bg 1yes4 with fastdissolve
    "\"Sahranghae.\""
    scene bg 1yes2 with fastdissolve
    "\"Sah...rang hae?\""
    scene bg 1yes4 with fastdissolve
    "\"Sah-rang-hae.\""
    scene bg 1yes5 with fastdissolve
    "\"Sahranghae!\" she says, in that shy, feline way."
    scene bg 1yes3 with fastdissolve
    "You can feel yourself blush, but decide to forego the feeling in favor of a more urgent question."
    scene bg 1yes4 with fastdissolve
    "\"What is it in Chinese?\""
    scene bg 1yes2 with fastdissolve
    "\"Wo ai ni!\" she says, without hesitation."
    scene bg 1yes4 with fastdissolve
    "\"Wo...ai...ni?\" you try it out. It's difficult, and she giggles at your attempts, but eventually you--"
    scene bg 1yes7 with fastdissolve
    "\"Wo ai ni, wo ai ni!\""
    "Suddenly, you feel bold."
    scene bg 1yes8 with fastdissolve
    "You take her hands, something you've casually done with her, as she does with you."
    "It feels different this time, and you hope that she knows (but also--you don't, because what if? What if?)"
    scene bg 1yes6 with fastdissolve
    "\"Wo ai ni.\""
    "\"Saranghae.\""
    scene white with slowdissolve
    "You're happier than you've ever been on this bus ride, sharing the seat with her."
    "You can't tell her your feelings, but surely they've reached her in all the important ways."
    "That's...enough for now."

    scene white with slowdissolve
    jump time1end

label time1no:
    scene white with fastdissolve
    "It is your sophomore year in high school."
    scene bg 1no1 with fastdissolve
    "You always sit next to her on the bus."
    "Sometimes it feels as though your heart will seize up somehow, right next to her, and you don't know why, but it feels good."
    "Today is no different. Except it is."
    scene bg 1no2 with fastdissolve
    "\"How do you say I love you in Korean?\" she asks."
    scene bg 1no3 with fastdissolve
    "You pause."
    "It's not a hard question--you say it all the time to your parents--but somehow, it feels daring this time."
    "\"Sahranghae.\""
    scene bg 1no2 with fastdissolve
    "\"Sah...rang hae?\""
    scene bg 1no3 with fastdissolve
    "\"Sah-rang-hae.\""
    scene bg 1no4 with fastdissolve
    "\"Sahranghae!\" she says, in that shy, feline way."
    scene bg 1no3 with fastdissolve
    "It excites you. You feel as though nothing else exists in the bus except you two, and you don't know how to name these feelings surging in you."
    "You ignore them as you usually do to things about yourself that you don't know how to name. It's suffocating."
    "Instead, you decide to reciprocate, appropriately."
    "\"What is it in Chinese?\" you ask."
    scene bg 1no2 with fastdissolve
    "\"Wo ai ni!\" she says, without hesitation."
    scene bg 1no3 with fastdissolve
    "\"Wo...ai...ni?\" you try it out. It's difficult, and she giggles at your attempts, but eventually you--"
    scene bg 1no6 with fastdissolve
    "\"Wo ai ni!\""
    scene bg 1no5 with fastdissolve
    "\"Saranghae.\""
    scene bg 1no7 with fastdissolve
    "You're happier than you've ever been on this bus ride, sharing the seat with her."
    "You don't know why, but you want to jump out of your seat, nameless energy brimming from your bones."
    scene bg 1no8 with fastdissolve
    dark "You're overcome with the urge to take her hands, hug her, do something."
    scene bg 1no9 with fastdissolve
    dark "But you don't."

    scene black with slowdissolve
    window hide
    $ renpy.pause(3.0,hard=True)
    jump time1end

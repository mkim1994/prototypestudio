using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NPCBehavior : MonoBehaviour {

    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    private Canvas panicCanvas;

    private bool insideInfluence;


    private List<GameObject> panictexts;

    public string[] panicthoughts ={
        "oh god", "oh my god", "oh god why","fuck", "fuck fuck fuck", "ahhhhhh","what the fuck is wrong with me",
        "what the fuck", "why", "why why why", "please"
    };

    void Start(){
        panicCanvas = GameObject.FindWithTag("Panic").GetComponent<Canvas>();
        insideInfluence = false;
        panictexts = new List<GameObject>();
    }
    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag != "Enemy")
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        } else{
            // timer += Time.deltaTime;

            /* if (timer >= wanderTimer)
             {
                 Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                 agent.SetDestination(newPos);
                 timer = 0;
             }*/
            agent.SetDestination(GameObject.FindWithTag("Player").transform.position);
        }

        if(insideInfluence){
            SpawnRandomText();
            if(panicCanvas.GetComponent<AudioSource>().volume == 0f){
                panicCanvas.GetComponent<AudioSource>().DOFade(1f, 0.5f);
            }
        } else{
            DespawnRandomText();
            if (panicCanvas.GetComponent<AudioSource>().volume == 1f)
            {
                panicCanvas.GetComponent<AudioSource>().DOFade(0f, 0.5f);
            }
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void SpawnRandomText(){
        if (panictexts.Count < 800)
        {
            GameObject textObj = Instantiate(Services.Prefabs.PanicText, Services.Prefabs.PanicText.transform.position, Quaternion.identity);
            textObj.transform.SetParent(panicCanvas.gameObject.transform);
            textObj.GetComponent<TextMeshProUGUI>().text = panicthoughts[Random.Range(0, panicthoughts.Length)];
            textObj.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-350f, 350f), Random.Range(-220f, 220f));
            textObj.GetComponent<RectTransform>().localRotation = Random.rotation;
            panictexts.Add(textObj);
        }

    }

    void DespawnRandomText(){
        if(panictexts.Count > 0){
            GameObject p = panictexts[panictexts.Count - 1];
            panictexts.RemoveAt(panictexts.Count - 1);
            Destroy(p);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            insideInfluence = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player"){
            insideInfluence = false;
        }
    }
}

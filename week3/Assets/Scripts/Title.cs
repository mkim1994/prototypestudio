using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Title : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Tab))
        {
            if (SceneManager.GetActiveScene().name == "fallend")
            {
                SceneManager.LoadScene("titlescreen");
            }
            else
            {
                if (transform.childCount > 0)
                {
                    if (!transform.GetChild(0).gameObject.activeSelf)
                    {
                        GetComponent<TMP_Text>().SetText("");
                        transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        SceneManager.LoadScene("main");
                    }
                }
            }
        }
    }
}

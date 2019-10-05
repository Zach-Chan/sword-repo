using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPosition : MonoBehaviour
{
    public string prevSceneName;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        prevSceneName = PlayerPrefs.GetString("lastLoadedScene");
        player = GameObject.FindGameObjectWithTag("Player");

        if(prevSceneName == "Scene1")
        {
            player.transform.position = new Vector2(-4.0f, -0.69f);            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

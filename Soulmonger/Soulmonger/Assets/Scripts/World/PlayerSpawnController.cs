using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerPrefab;

    public GameObject SpawnLocation;
    
    void Awake()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.transform.position = SpawnLocation.transform.position;
        }
        else
        {
            Player = Instantiate(PlayerPrefab);
            Player.transform.position = SpawnLocation.transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

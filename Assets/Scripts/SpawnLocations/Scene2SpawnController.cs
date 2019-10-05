using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2SpawnController : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerPrefab;

    public GameObject[] SpawnLocation;

    public string lastScene;

    void Awake()
    {
        getLastScene();
        addSpawnlocations();

        if(getLastScene() == "Scene1")
        {
        respawnAt(0);
        }
        else if(getLastScene() == "Scene2")
        {
        respawnAt(0);
        }
        else if(getLastScene() == "Scene3")
        {
        respawnAt(1);
        }
        else if(getLastScene() == "Scene4")
        {
        respawnAt(2);
        }
        else
        {
        respawnAt(0);
        }
    }

    string getLastScene()
    {
        if(PlayerPrefs.GetString("lastLoadedScene") != null)
        {
        lastScene = PlayerPrefs.GetString("lastLoadedScene");
        return lastScene;
        } 
        else
        {
            return null;
        }
    }

    void addSpawnlocations() //populates the spawnlocation array with the empty PlayerSpawn game objects
    {
        SpawnLocation=GameObject.FindGameObjectsWithTag("PlayerSpawn");
    }

    void respawnAt(int x) //respawns the player at this array indexed PlayerSpawn object
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.transform.position = SpawnLocation[x].transform.position;
        }
        else
        {
            Player = Instantiate(PlayerPrefab);
            Player.transform.position = SpawnLocation[x].transform.position;
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

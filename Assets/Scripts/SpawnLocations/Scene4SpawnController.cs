using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4SpawnController : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerPrefab;

    public GameObject[] SpawnLocation;

    public string lastScene;

    void Awake()
    {
        getLastScene();
        addSpawnlocations();

        if(getLastScene() == "Scene2") //spawn the player at this position based on his last scene
        {
        respawnAt(0);
        }
        else //spawn player at 0 if no previous scene can can be found
        {
        respawnAt(0);
        }
    }

    string getLastScene() //returns the string of the last scene that the player visited
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
}

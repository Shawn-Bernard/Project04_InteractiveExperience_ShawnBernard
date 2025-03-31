using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string spawnPointName;

    public void LoadSceneWithSpawnPoint(string sceneName, string triggerSpawnPoint)
    {
        //Throwing in my trigger string into my spawn point name
        spawnPointName = triggerSpawnPoint;

        //Loading in my scene name with the string from the trigger
        SceneManager.LoadScene(sceneName);

        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    public void LoadSceneWithSpawnPoint(string sceneName)
    {

        //Loading in my scene name with the string from the trigger
        SceneManager.LoadScene(sceneName);


        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //after loading in scene throwing in my spawnPointName
        if (spawnPointName != null)
        {
            // Added this if so I could stop seeing the error when I switch to main menu
            SetPlayerSpawnPoint(spawnPointName);
        }
        

        //Unsubbing so I don't loop
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }


    private void SetPlayerSpawnPoint(string spawnPointName)
    {
        //Finding my spawn point from the trigger spawn point
        GameObject spawnPoint = GameObject.Find(spawnPointName);

        //Finding my player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //Debug.Log($"Player {player}");

        //Debug.Log($"Spawn {spawnPoint}");
        if (spawnPoint == null)
        {
            Debug.Log("No spawn point");
        }
        else
        {
            player.transform.position = spawnPoint.transform.position;
        }

        
        

    }
}

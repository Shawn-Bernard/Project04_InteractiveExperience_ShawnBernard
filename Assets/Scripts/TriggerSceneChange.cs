using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerSceneChange : MonoBehaviour
{
    // Serialize Field will show the "data" even when private
    [SerializeField] private string nextSceneName;

    [SerializeField] private string nextSpawnName;

    [SerializeField] private GameManager gameManager;



    private void Start()
    {
        //Need this to get my game manager, so I can use it
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.LevelManger.LoadSceneWithSpawnPoint(nextSceneName, nextSpawnName);
            //Debug.Log($" scene {nextSceneName} spawn {nextSpawnName}");
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a wireframe cube matching the BoxCollider2D size
        Gizmos.color = new Color(0, 255, 255, 0.75f); // Set Gizmo color to cyan

        // Get the BoxCollider2D component
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            // Calculate the center position and size of the WireCube
            Vector3 center = transform.position + (Vector3)boxCollider.offset;
            Vector3 size = new Vector3(boxCollider.size.x, boxCollider.size.y, 0);

            // Draw the WireCube
            Gizmos.DrawCube(center, size);
        }
        else
        {
            //Debug.LogWarning("BoxCollider2D component not found on the GameObject.");
        }
    }
}

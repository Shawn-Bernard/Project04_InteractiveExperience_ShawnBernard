using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Managers
    public UIManager UImanager;

    public LevelManager LevelManger;

    public GameStateManager gameStateManager;

    public DialogueManager dialogueManager;

    public PlayerMovement playerMovement;

    public PlayerInteraction playerInteraction;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
}

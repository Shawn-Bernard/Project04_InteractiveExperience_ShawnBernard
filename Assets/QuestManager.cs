using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public List<string> activeQuests = new List<string>();
    private List<string> completedQuests = new List<string>();

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void AddNewQuest(string questName)
    {
        Debug.Log("Hit adding quest");
        if (!activeQuests.Contains(questName) && !completedQuests.Contains(questName))
        {
            activeQuests.Add(questName);
            Debug.Log($"Quest added {questName}");
            gameManager.UImanager.ChangeGameplayText();
        }
    }

    public void CompleteQuest(string questName)
    {
        if (activeQuests.Contains(questName))
        {
            activeQuests.Remove(questName);
            completedQuests.Add(questName);
            Debug.Log($"Quest completed {questName}");
            gameManager.UImanager.ChangeGameplayText();
        }
    }

    public bool IsQuestDone(string questName)
    {
        return completedQuests.Contains(questName);
    }

    public bool HasQuestActive(string questName)
    {
        return activeQuests.Contains(questName);
    }
}

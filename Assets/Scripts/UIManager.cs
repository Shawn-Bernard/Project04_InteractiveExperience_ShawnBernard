using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameplay;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject info;

    [SerializeField] private TextMeshProUGUI gameplayText;

    [SerializeField] private TextMeshProUGUI infoText;

    [SerializeField] private TextMeshProUGUI inventoryText;

    private List<string> infoList = new List<string>(); // List to hold info lines
    public void EnableMainMenu()
    {
        DisableAllMenus();
        //This will show my main menu canvas and so on
        mainMenu.SetActive(true);
        Debug.Log("Im main menu");
    }
    public void EnableGameplay()
    {
        DisableAllMenus();
        gameplay.SetActive(true);
        Debug.Log("Im gameplay menu");
    }
    public void EnablePause()
    {
        DisableAllMenus();
        pause.SetActive(true);
        Debug.Log("Im pause menu");
    }
    public void EnableOptions()
    {
        DisableAllMenus();
        options.SetActive(true);
        Debug.Log("Im options menu");
    }
    public void QuitGame()
    {
        Application.Quit();
        //This will take you outta play mode also = to false works, I googled it and got this
        //EditorApplication.isPlaying = !EditorApplication.isPlaying;
        //PS unity doesn't like this when trying to build :C
    }

    //making a method to disable all my games object so I'm only have to do it once 
    public void DisableAllMenus()
    {
        mainMenu.SetActive(false);
        gameplay.SetActive(false);
        pause.SetActive(false);
        options.SetActive(false);
        info.SetActive(false);

    }
    public IEnumerator StartInfo(string[] infoToPlay)
    {
        info.SetActive(true);// Makes the info text box visible

        infoList.Clear();// Clears old info text

        // Adding each line from the passed in array to the info list
        foreach (string line in infoToPlay)
        {
            infoList.Add(line);
        }

        if (infoList.Count == 0)
        {
            info.SetActive(false);// If theres no more lines hide info box
            yield break;// Breaks out of coroutine
        }

        // Going through the lines in infoList and chaning text one at a time
        foreach (string line in infoList)
        {
            infoText.text = line;
            yield return new WaitForSeconds(3f);// Holds for 3 seconds before the next line
        }

        info.SetActive(false);// Setting text box to false after all the lines are done 
    }

    public void ChangeGameplayText()
    {
        string QuestString = "Active Quest";

        foreach (var Quest in GameManager.Instance.questManager.activeQuests)
        {
            QuestString += "\n" + Quest;
        }

        Debug.Log("Added quest");
        gameplayText.text = QuestString;  // Update the UI with the built inventory string
    }

    public void ChangeInventoryText()
    {
        string inventoryString = "";  // Creating a string that holds the inventory items

        // Adding each inventory item in the string
        foreach (GameObject item in GameManager.Instance.Inventory)
        {
            inventoryString += item.name + "\n";  // Adds the item then splits the string
        }

        inventoryText.text = inventoryString;  // Update the UI with the built inventory string
    }

}
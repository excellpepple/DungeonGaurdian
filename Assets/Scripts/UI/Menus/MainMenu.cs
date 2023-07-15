using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public MenuManager menuManager;
    public void PlayGame()
    {
        Debug.Log("Starting Game"); 
        Application.LoadLevel(1);
        menuManager.HandleMainMenu();
    }

    public void DisplaySettings()
    {
        Debug.Log("Displaying Settings");
        menuManager.HandleSettings();
    }

    public void DisplayInventory()
    {
        Debug.Log("Displaying Inventory");
        menuManager.HandleInventory();
    }
}

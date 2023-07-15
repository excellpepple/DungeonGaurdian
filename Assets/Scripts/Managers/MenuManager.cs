using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject settingsPage;

    public GameObject pauseScreen;
    public GameObject inventoryPage;

    private bool mainMenuvisible;
    private bool pauseScreenvisible;
    private bool inventoryPagevisible;
    private bool settingsPagevisible;
    

    private void Start()
    {
        mainMenuvisible = mainMenu.activeInHierarchy;
       
    }

    public void HandleMainMenu()
    {
        if (mainMenuvisible)
        {
            MenuRender(mainMenuValue:!mainMenuvisible);
        }
        else
        {
            MenuRender(
                mainMenuValue:true,
                pauseScreenValue:pauseScreenvisible,
                inventoryValue: inventoryPagevisible,
                settingsValue:settingsPagevisible);
        }
    }
    
    public void HandleSettings()
    {
        if (settingsPagevisible)
        {
            MenuRender(settingsValue:!settingsPagevisible);
        }
        else
        {
            MenuRender(
                mainMenuValue:mainMenuvisible,
                pauseScreenValue:pauseScreenvisible,
                inventoryValue: inventoryPagevisible,
                settingsValue:true);
        }
    }
    
    public void HandlePauseScreen()
    {
        if (pauseScreenvisible)
        {
            MenuRender(pauseScreenValue:!pauseScreenvisible);
        }
        else
        {
            MenuRender(
                mainMenuValue:mainMenuvisible,
                pauseScreenValue:true,
                inventoryValue: inventoryPagevisible,
                settingsValue:settingsPagevisible);
        }
    }

    public void HandleInventory()
    {
        if (inventoryPagevisible)
        {
            MenuRender(inventoryValue:!inventoryPagevisible);
        }
        else
        {
            MenuRender(
                mainMenuValue:mainMenuvisible,
                pauseScreenValue:pauseScreenvisible,
                inventoryValue: true,
                settingsValue:settingsPagevisible);
        }
    }
    
    private void MenuRender(
        bool mainMenuValue = false, 
        bool pauseScreenValue = false, 
        bool settingsValue = false, 
        bool inventoryValue = false)
    {
        mainMenu.gameObject.SetActive(mainMenuValue);
        settingsPage.gameObject.SetActive(settingsValue);
        pauseScreen.gameObject.SetActive(pauseScreenValue);
        inventoryPage.gameObject.SetActive(inventoryValue);
    }
    
}

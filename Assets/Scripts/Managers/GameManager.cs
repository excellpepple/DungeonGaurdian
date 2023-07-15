using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Game States
    public Hashtable _managers;

    public Hashtable weapons;

    public Hashtable armor;
    
    
    
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _managers = new Hashtable();
        weapons = new Hashtable();
        armor = new Hashtable();
        foreach (GameObject manager in GameObject.FindGameObjectsWithTag("Manager"))
        {
            _managers.Add(manager.name, manager);
            Debug.Log("Manager: " + manager.name);
            DontDestroyOnLoad(manager.gameObject);
        };
    }

    void Awake()
    {
        //Handling Managers Level Transitions
        DontDestroyOnLoad(this.gameObject);
       
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}

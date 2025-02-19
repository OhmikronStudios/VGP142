﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject finishText;
    [SerializeField] GameObject finishTextLoc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                        Application.Quit();
            #endif
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ReloadScene();
        }


    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void FinishText()
    {
        Instantiate(finishText, finishTextLoc.transform.position, Quaternion.identity);
    }
}

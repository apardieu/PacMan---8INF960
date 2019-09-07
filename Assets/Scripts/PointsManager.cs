﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private bool gameEnded;
    public string sceneVictory;
    public string sceneDefeat;
    private int nblife = 3;

    void Start()
    {
        gameEnded = false;
    }
  

    void Update()
    {
        if("24600".Equals(scoreText.text)&&!gameEnded)
        {
            gameEnded = true;
            changeScene();
        }
    }

    public void changeScene()
    {
        if ("24600".Equals(scoreText.text))
        {
            SceneManager.LoadScene(sceneVictory);
        }
        else
        {
            SceneManager.LoadScene(sceneDefeat);
        }
    }

    public void decrementeLife()
    {
        nblife--;
        if (nblife == 2)
            Destroy(GameObject.FindWithTag("life1"));
        if (nblife == 1)
            Destroy(GameObject.FindWithTag("life2"));
        if(nblife==0)
            changeScene();
    }
}

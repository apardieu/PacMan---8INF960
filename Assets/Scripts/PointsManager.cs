using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if("24600".Equals(scoreText.text)&&!gameEnded)
        {
            print("end");
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
        if(nblife==0)
        {
            changeScene();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private bool gameEnded;
    public string sceneNameToLoad;
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
        SceneManager.LoadScene(sceneNameToLoad);
    }
}

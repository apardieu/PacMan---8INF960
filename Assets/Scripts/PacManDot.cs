using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PacManDot : MonoBehaviour
{


    [SerializeField] private Text scoreText;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile spriteBleu;
    [SerializeField] private Rigidbody2D pacmanRigidBody;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 position = GameObject.FindWithTag("pacman").transform.position;
        Sprite sprite = tilemap.GetSprite(tilemap.WorldToCell(position));

        if (sprite == null)
        {
        }
        else
        {
            if (sprite.name == "pacman_map_3")
            {
                tilemap.SetTile(tilemap.WorldToCell(position), spriteBleu);
                int score = Int32.Parse(scoreText.text)+100;
                scoreText.text = score.ToString();

            }


        }
    }
}


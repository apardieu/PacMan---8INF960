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
    [SerializeField] private Tile spriteBlue;
    [SerializeField] private Rigidbody2D pacmanRigidBody;
    

    void FixedUpdate()
    {

        Vector3 position = GameObject.FindWithTag("pacman").transform.position;
        Sprite sprite = tilemap.GetSprite(tilemap.WorldToCell(position));

        
        if(sprite != null)
        {
            if (sprite.name == "pacman_map_3")
            {
                tilemap.SetTile(tilemap.WorldToCell(position), spriteBlue);
                int score = Int32.Parse(scoreText.text)+100;
                scoreText.text = score.ToString();

            }


        }
    }
}


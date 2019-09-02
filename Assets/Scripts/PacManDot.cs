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
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        print("Pacman Position : " + GameObject.FindWithTag("pacman").transform.position);
        //print(tilemap.cellBounds);
        Vector3 position = GameObject.FindWithTag("pacman").transform.position;
        Vector3Int positionInt = Vector3Int.zero;
        positionInt.x = Mathf.FloorToInt(position.x);
        positionInt.y = Mathf.FloorToInt(position.y);
        positionInt.z = Mathf.FloorToInt(position.z);

        print("Tile Position in Tilemap : " + tilemap.WorldToCell(position));

        Sprite sprite = tilemap.GetSprite(tilemap.WorldToCell(position));
        string newTileSprite;
        print("Tile Sprite"+sprite);
        if (sprite == null)
        {
            newTileSprite = "Null";
        }
        else
        {
            if (sprite.name == "pacman_map_3")
            {
                tilemap.SetTile(tilemap.WorldToCell(position), spriteBleu);
                int score = Int32.Parse(scoreText.text)+100;
                scoreText.text = score.ToString();

            }
            newTileSprite = "Pacman";

        }
        newTileSprite = sprite.name;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacManDot : MonoBehaviour
{

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile spriteBleu;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(tilemap.cellBounds);
        Vector3 position = GameObject.FindWithTag("pacman").transform.position;
        Vector3Int positionInt = Vector3Int.zero;
        positionInt.x = Mathf.FloorToInt(position.x);
        positionInt.y = Mathf.FloorToInt(position.y);
        positionInt.z = Mathf.FloorToInt(position.z);
        Sprite sprite = tilemap.GetSprite(positionInt);
        string tileSprite;
        if (sprite == null)
        {
            tileSprite = "Null";
        }
        else
        {
            if (sprite.name == "pacman_map_6")
            {
                tilemap.SetTile(positionInt,spriteBleu);
            }
            tileSprite = "Pacman";

        }
        tileSprite = sprite.name;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class FantomeIA : MonoBehaviour
{
  private Vector2 dest = Vector2.zero;
  private Vector2 destDebut1 = new Vector2(0, 1.12f);
  private Vector2 destDebut2 = new Vector2(0.48f, 1.12f);
  private int direction;
  private int oldDirection;
  public float speed = 0.05f;
  public float step = 0.32f;
  public float stepCollider = 0.32f;
  private int cpt = 0;
  private bool debut1 = false;
  private bool debut2 = false;

  [SerializeField] private Tilemap tilemap;



  void Start()
  {
    direction = Random.Range(0, 3);
    oldDirection = direction;
    debut1 = true;
    debut2 = true;
  }


  int ChassePacman2()
  {
    Vector3 positionPacman = GameObject.FindWithTag("pacman").transform.position;
    Debug.Log(transform.position.ToString("F10") + "    " + positionPacman.ToString("F10") + "     " + transform.name);
    Sprite sprite = null;
    Debug.Log(System.Math.Round(transform.position.x, 2) == System.Math.Round(positionPacman.x, 2));
    Debug.Log(System.Math.Round(transform.position.y, 2) == System.Math.Round(positionPacman.y, 2));


    if (System.Math.Round(transform.position.x, 2) == System.Math.Round(positionPacman.x, 2)) //sur la meme ligne verticale
    {
      Debug.Log("Pacman sur meme ligne verticale    " + transform.name);
      float difference = Mathf.Abs(positionPacman.y - transform.position.y);
      float ratio = difference / step;

      for (int i = 1; i < ratio; i++)
      {
        if (positionPacman.y >= transform.position.y) // pacman est au dessus du fantome
          sprite = tilemap.GetSprite(tilemap.WorldToCell(new Vector2(transform.position.x, transform.position.y + i * step)));

        else
          sprite = tilemap.GetSprite(tilemap.WorldToCell(new Vector2(transform.position.x, transform.position.y - i * step)));

        Debug.Log(sprite);
        if (sprite == null)
          return -1;
        else if (!((sprite.name == "pacman_map_3") || (sprite.name == "pacman_map_6") || (sprite.name == "pacman_map_4")))
          return -1;
      }
      if (positionPacman.y >= transform.position.y)
      {
        if (valid(0))
          return 0;
      }
      else
      {
        if (valid(2))
          return 2;
      }

    }
    else if (System.Math.Round(transform.position.y, 2) == System.Math.Round(positionPacman.y, 2))
    {
      Debug.Log("Pacman sur meme ligne horizontale    " + transform.name);
      float difference = Mathf.Abs(positionPacman.x - transform.position.x);
      float ratio = difference / step;

      for (int i = 1; i < ratio; i++)
      {
        if (positionPacman.x >= transform.position.x) //pacman est à droite du fantome
          sprite = tilemap.GetSprite(tilemap.WorldToCell(new Vector2(transform.position.x + i * step, transform.position.y)));

        else
          sprite = tilemap.GetSprite(tilemap.WorldToCell(new Vector2(transform.position.x - i * step, transform.position.y)));

        Debug.Log(sprite);
        if (sprite == null)
          return -1;
        else if (!((sprite.name == "pacman_map_3") || (sprite.name == "pacman_map_6") || (sprite.name == "pacman_map_6")))
          return -1;
      }
      if (positionPacman.x >= transform.position.x)
      {
        if(valid(1))
         return 1;
      }
      else
      {
        if(valid(3))
          return 3;
      }
        
    }

    return -1;
  }

  int ChassePacman()
  {
    Vector2 positionPacman = GameObject.FindWithTag("pacman").transform.position;
    Vector2 difference = -((Vector2)transform.position - positionPacman);
    float choix1 = Mathf.Max(Mathf.Abs(difference.x), Mathf.Abs(difference.y));
    float choix2 = Mathf.Max(Mathf.Abs(difference.x), Mathf.Abs(difference.y));
    int directionChoix1 = -1;
    int directionChoix2 = -1;
    if (choix1 == Mathf.Abs(difference.x))
    {
      if (difference.x >= 0)
        directionChoix1 = 1;
      else
        directionChoix1 = 3;

      if (difference.y >= 0)
        directionChoix2 = 0;
      else
        directionChoix2 = 2;
    }
    else if (choix1 == Mathf.Abs(difference.y))
    {
      if (difference.y >= 0)
        directionChoix1 = 0;
      else
        directionChoix1 = 2;

      if (difference.x >= 0)
        directionChoix2 = 1;
      else
        directionChoix2 = 3;
    }

    if (valid(directionChoix1))
      return directionChoix1;
    else if (valid(directionChoix2))
      return directionChoix2;
    else
      return -1;

  }


  void StartFantome()
  {
    if (transform.name == "blinky")
    {
      Vector2 p = Vector2.MoveTowards(transform.position, destDebut1, speed);
      GetComponent<Rigidbody2D>().MovePosition(p);
      if ((Vector2)transform.position == destDebut1)
        debut1 = false;
      if ((debut1 == false) && (debut2 == true))
      {
        p = Vector2.MoveTowards(transform.position, destDebut2, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
      }
      if ((Vector2)transform.position == destDebut2)
        debut2 = false;


    }

    else if (transform.name == "clyde")
    {
      if (cpt >= 180)
      {
        Vector2 p = Vector2.MoveTowards(transform.position, destDebut1, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
        if ((Vector2)transform.position == destDebut1)
          debut1 = false;
        if ((debut1 == false) && (debut2 == true))
        {
          p = Vector2.MoveTowards(transform.position, destDebut2, speed);
          GetComponent<Rigidbody2D>().MovePosition(p);
        }
        if ((Vector2)transform.position == destDebut2)
          debut2 = false;
      }
    }

    else if (transform.name == "inky")
    {
      if (cpt >= 360)
      {
        Vector2 p = Vector2.MoveTowards(transform.position, destDebut1, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
        if ((Vector2)transform.position == destDebut1)
          debut1 = false;
        if ((debut1 == false) && (debut2 == true))
        {
          p = Vector2.MoveTowards(transform.position, destDebut2, speed);
          GetComponent<Rigidbody2D>().MovePosition(p);
        }
        if ((Vector2)transform.position == destDebut2)
          debut2 = false;
      }
    }

    else if (transform.name == "pinky")
    {
      if (cpt >= 480)
      {
        Vector2 p = Vector2.MoveTowards(transform.position, destDebut1, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
        if ((Vector2)transform.position == destDebut1)
          debut1 = false;
        if ((debut1 == false) && (debut2 == true))
        {
          p = Vector2.MoveTowards(transform.position, destDebut2, speed);
          GetComponent<Rigidbody2D>().MovePosition(p);
        }
        if ((Vector2)transform.position == destDebut2)
          debut2 = false;
      }
    }

    if (debut2 == false)
      dest = transform.position;

    cpt++;
  }

  void FixedUpdate()
  {
    if ((debut1 == true) || (debut2 == true))
      StartFantome();

    else
    {
      int directionChassePacman;
      Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
      GetComponent<Rigidbody2D>().MovePosition(p);


      if ((Vector2)transform.position == p)
      {
        //oldDirection = direction;
        directionChassePacman = ChassePacman2();
        Debug.Log(directionChassePacman + "   " + transform.name);
        if (directionChassePacman == (-1))
        {
          if (direction == 0)
          {
            if (valid(0))
            {
              dest.y = transform.position.y + step;
              oldDirection = direction;
            }
            else
            {
              if ((oldDirection == 1) || (oldDirection == 3))
                direction = 2;

              else
              {
                while ((direction == 0) || (direction == 2))
                {
                  direction = Random.Range(0, 3);
                }
              }
            }
          }

          else if (direction == 1)
          {
            if (valid(1))
            {
              dest.x = transform.position.x + step;
              oldDirection = direction;
            }
            else
            {
              if ((oldDirection == 0) || (oldDirection == 2))
                direction = 3;

              else
              {
                while ((direction == 1) || (direction == 3))
                {
                  direction = Random.Range(0, 3);
                }
              }
            }
          }

          else if (direction == 2)
          {
            if (valid(2))
            {
              dest.y = transform.position.y - step;
              oldDirection = direction;
            }
            else
            {
              if ((oldDirection == 1) || (oldDirection == 3))
                direction = 0;

              else
              {
                while ((direction == 0) || (direction == 2))
                {
                  direction = Random.Range(0, 3);
                }
              }
            }
          }

          else if (direction == 3)
          {
            if (valid(3))
            {
              dest.x = transform.position.x - step;
              oldDirection = direction;
            }
            else
            {
              if ((oldDirection == 0) || (oldDirection == 2))
                direction = 1;

              else
              {
                while ((direction == 1) || (direction == 3))
                {
                  direction = Random.Range(0, 3);
                }
              }
            }
          }

        }

        else
        {
          direction = directionChassePacman;
          if(direction == 0)
            dest.y = transform.position.y + step;
          else if(direction == 1)
            dest.x = transform.position.x + step;
          else if (direction == 2)
            dest.y = transform.position.y - step;
          else if (direction == 3)
            dest.x = transform.position.x - step;
        }
      }

      Vector2 dir = dest - (Vector2)transform.position;
      GetComponent<Animator>().SetFloat("DirX", dir.x);
      GetComponent<Animator>().SetFloat("DirY", dir.y);
    }
  }


  bool valid(int direction)
  {
    if (direction == (-1))
      return false;


    Vector2 pos = transform.position;
    RaycastHit2D hit = Physics2D.Linecast(pos, pos); ;
    if (direction == 0)
      hit = Physics2D.Linecast(pos + new Vector2(0, stepCollider), pos);
    else if (direction == 1)
      hit = Physics2D.Linecast(pos + new Vector2(stepCollider, 0), pos);
    else if (direction == 2)
      hit = Physics2D.Linecast(pos - new Vector2(0, stepCollider), pos);
    else if (direction == 3)
      hit = Physics2D.Linecast(pos - new Vector2(stepCollider, 0), pos);

    if (hit.collider.name == "Walls")
    {
      return false;
    }
    else if (hit.collider.name == "pacman")
    {
      //Debug.Log("Collision avec pacman: " + transform.name);

      return true;
    }
    else
    {
      return true;
    }
  }
  void OnCollisionEnter2D(Collision2D col)
  {
    print("Ghost");
  }
}

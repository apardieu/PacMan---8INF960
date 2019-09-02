using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
  


  void Start()
  {
    direction = Random.Range(0, 3); 
    oldDirection = direction;
    debut1 = true;
    debut2 = true;
  }

  void StartFantome()
  {
    if(transform.name == "blinky")
    {
      Vector2 p = Vector2.MoveTowards(transform.position, destDebut1, speed);
      GetComponent<Rigidbody2D>().MovePosition(p);
      if ((Vector2)transform.position == destDebut1)
        debut1 = false;
      if((debut1 == false) && (debut2 == true))
      {
        p = Vector2.MoveTowards(transform.position, destDebut2, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
      }
      if ((Vector2)transform.position == destDebut2)
        debut2 = false;


    }

    else if(transform.name == "clyde")
    {
      if(cpt >= 180)
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

    if(debut2 == false)
      dest = transform.position;

    cpt++;
  }


  void FixedUpdate()
  {
    if((debut1 == true) || (debut2 == true))
      StartFantome();

    else
    {
      Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
      GetComponent<Rigidbody2D>().MovePosition(p);


      if ((Vector2)transform.position == p)
      {
        if (direction == 0)
        {
          if (valid(new Vector2(0, stepCollider)))
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
          if (valid(new Vector2(stepCollider, 0)))
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
          if (valid(new Vector2(0, -stepCollider)))
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
          if (valid(new Vector2(-stepCollider, 0)))
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
    }
    
    Vector2 dir = dest - (Vector2)transform.position;
    GetComponent<Animator>().SetFloat("DirX", dir.x);
    GetComponent<Animator>().SetFloat("DirY", dir.y);
  }

  

  bool valid(Vector2 dir)
  {
    Vector2 pos = transform.position;
    RaycastHit2D hit = Physics2D.Linecast(pos+ dir, pos);
    if (hit.collider == GetComponent<Collider2D>())
    {
      //Debug.Log("Collision avec self: " + transform.name);
      return true;
    }
    else if (hit.collider.name == "Walls")
    {
      //Debug.Log("Collision avec murs: " + transform.name);
      return false;
    }
    else if(hit.collider.name == "pacman")
    {
      //Debug.Log("Collision avec pacman: " + transform.name);
      return true;
    }
    else
    {
      //Debug.Log("Collision avec autre fantome: " + transform.name + "   " + hit.collider.name);
      return true;
    }
  }
}

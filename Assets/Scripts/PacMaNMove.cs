﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMaNMove : MonoBehaviour
{
  public float step = 0.32f;
  public float stepCollider = 0.32f;
    public PointsManager p;

    [SerializeField]
  private float speed = 4f;
    private bool warp = false;
  private Vector3 deplacement = Vector3.zero;
  public static Vector2 dest = Vector2.zero;
    private float vulnerabilityCountDown = 0;
    

  // Start is called before the first frame update
    void Start()
  {
    dest = transform.position;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
        fantomes();
        Vector2 p;
        // Move closer to Destination
        if (warp)
        {
            p = Vector2.MoveTowards(transform.position, dest, 100000);
            warp = false;
        }
        else
        {
             p = Vector2.MoveTowards(transform.position, dest, speed);
        }
    
    GetComponent<Rigidbody2D>().MovePosition(p);
    

    // Check for Input if not moving
    if ((Vector2)transform.position == p)
    {
            if ((Input.GetKey(KeyCode.UpArrow)) && (valid(new Vector2(0, stepCollider))))

                dest = (Vector2)transform.position + new Vector2(0, step);
            else if ((Input.GetKey(KeyCode.RightArrow)) && (valid(new Vector2(stepCollider, 0))))
                if (p.x > 4.317f)
                {
                    warp = true;
                    dest = new Vector2(-4.317f, 0.16f);
                }
                else
                    dest = (Vector2)transform.position + new Vector2(step, 0);
            else if ((Input.GetKey(KeyCode.DownArrow)) && (valid(new Vector2(0, -stepCollider))))
                dest = (Vector2)transform.position - new Vector2(0, step);
            else if ((Input.GetKey(KeyCode.LeftArrow)) && (valid(new Vector2(-stepCollider, 0))))
                if (p.x < -4.317f)
                {
                    dest = new Vector2(4.317f, 0.16f);
                    warp = true;
                }
                else
                    dest = (Vector2)transform.position - new Vector2(step, 0);


      // Animation Parameters
      Vector2 dir = dest - (Vector2)transform.position;
            if (warp)
                dir *= -1;

      GetComponent<Animator>().SetFloat("DirX", dir.x);
      GetComponent<Animator>().SetFloat("DirY", dir.y);
            

        }


  }

  bool valid(Vector2 dir)
  {
    Vector2 pos = transform.position;
    RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
    
    if (hit.collider.name == "Walls")
      return false;
    else
        {
            
            
            return true;
        }
     
  }

    void fantomes()
    {
        if(vulnerabilityCountDown<=0)
        {
            Vector2 pos = transform.position;
            Collider2D hitFront = Physics2D.OverlapCircle(pos, 0.32f);

            if (hitFront.name == "blinky" || hitFront.name == "pinky" || hitFront.name == "inky" || hitFront.name == "clyde")
            {
                print("fantome");
                vulnerabilityCountDown = 50;
                p.decrementeLife();

            }

        }
        else
        {
            vulnerabilityCountDown--;
            
        }
        

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMaNMove : MonoBehaviour
{
  public float step = 0.32f;
  public float stepCollider = 0.32f;
  public PointsManager pointsManager;
  public GameObject inky;
  public GameObject pinky;
  public GameObject blinky;
  public GameObject clyde;

  [SerializeField]
  private float speed = 4f;
  private bool warp = false;
  public static Vector2 destination = Vector2.zero;
  private float vulnerabilityCountDown = 0;

  
  void Start()
  {
    destination = transform.position;
  }
  

  void FixedUpdate()
  {
    PacmanEaten();
    Vector2 tempPosition;
    if (warp)
    {
      tempPosition = Vector2.MoveTowards(transform.position, destination, 100000);
      warp = false;
    }
    else
    {
      tempPosition = Vector2.MoveTowards(transform.position, destination, speed);
    }

    GetComponent<Rigidbody2D>().MovePosition(tempPosition);


    // Check for Input if not moving
    if ((Vector2)transform.position == tempPosition)
    {
      if ((Input.GetKey(KeyCode.UpArrow)) && (ColliderCheck(new Vector2(0, stepCollider))))

        destination = (Vector2)transform.position + new Vector2(0, step);
      else if ((Input.GetKey(KeyCode.RightArrow)) && (ColliderCheck(new Vector2(stepCollider, 0))))
        if (tempPosition.x > 4.317f)
        {
          warp = true;
          destination = new Vector2(-4.317f, 0.16f);
        }
        else
          destination = (Vector2)transform.position + new Vector2(step, 0);
      else if ((Input.GetKey(KeyCode.DownArrow)) && (ColliderCheck(new Vector2(0, -stepCollider))))
        destination = (Vector2)transform.position - new Vector2(0, step);
      else if ((Input.GetKey(KeyCode.LeftArrow)) && (ColliderCheck(new Vector2(-stepCollider, 0))))
        if (tempPosition.x < -4.317f)
        {
          destination = new Vector2(4.317f, 0.16f);
          warp = true;
        }
        else
          destination = (Vector2)transform.position - new Vector2(step, 0);


      // Animation Parameters
      Vector2 animation = destination - (Vector2)transform.position;
      if (warp)
        animation *= -1;

      GetComponent<Animator>().SetFloat("DirX", animation.x);
      GetComponent<Animator>().SetFloat("DirY", animation.y);


    }


  }

  bool ColliderCheck(Vector2 dir)
  {
    Vector2 pos = transform.position;
    RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);

    if (hit.collider.name == "Walls")
      return false;
    else
      return true;

  }

  

  void PacmanEaten()
  {
    if (vulnerabilityCountDown <= 0)
    {
      Vector2 pos = transform.position;
      Vector2 pos1 = blinky.transform.position;
      Vector2 pos2 = inky.transform.position;
      Vector2 pos3 = pinky.transform.position;
      Vector2 pos4 = clyde.transform.position;
      float col1 = (pos - pos1).magnitude;
      float col2 = (pos - pos2).magnitude;
      float col3 = (pos - pos3).magnitude;
      float col4 = (pos - pos4).magnitude;


      if (col1 <= 0.32 || col2 <= 0.32 || col3 <= 0.32 || col4 <= 0.32)
      {
        vulnerabilityCountDown = 100;
        pointsManager.decrementeLife();
        blinky.GetComponent<FantomeIA>().warp = true;
        pinky.GetComponent<FantomeIA>().warp = true;
        inky.GetComponent<FantomeIA>().warp = true;
        clyde.GetComponent<FantomeIA>().warp = true;
        destination = new Vector2(0.16f, -2.72f);
        warp = true;
      }

    }
    else
    {
      vulnerabilityCountDown--;

    }
  }



}

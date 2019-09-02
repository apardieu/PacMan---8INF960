using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMaNMove : MonoBehaviour
{
  public float step = 0.32f;
  public float stepCollider = 0.32f;

  [SerializeField]
  private float speed = 4f;

  private Vector3 deplacement = Vector3.zero;
  private Vector2 dest = Vector2.zero;

  // Start is called before the first frame update
  void Start()
  {
    dest = transform.position;
  }

  // Update is called once per frame
  void FixedUpdate()
  {

    // Move closer to Destination
    Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
    GetComponent<Rigidbody2D>().MovePosition(p);
    

    // Check for Input if not moving
    if ((Vector2)transform.position == p)
    {
      if ((Input.GetKey(KeyCode.UpArrow)) && (valid(new Vector2(0, stepCollider))))
        dest = (Vector2)transform.position + new Vector2(0, step);
      else if ((Input.GetKey(KeyCode.RightArrow)) && (valid(new Vector2(stepCollider, 0))))
        dest = (Vector2)transform.position + new Vector2(step, 0);
      else if ((Input.GetKey(KeyCode.DownArrow)) && (valid(new Vector2(0, -stepCollider))))
        dest = (Vector2)transform.position - new Vector2(0, step);
      else if ((Input.GetKey(KeyCode.LeftArrow)) && (valid(new Vector2(-stepCollider, 0))))
        dest = (Vector2)transform.position - new Vector2(step, 0);


      // Animation Parameters
      Vector2 dir = dest - (Vector2)transform.position;
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
      return true;
  }
}

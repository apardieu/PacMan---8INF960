using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMaNMove : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    
    [SerializeField]
    private float speed=4f;

    private Vector3 deplacement=Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
       if (Input.GetKey(KeyCode.UpArrow))
       {
           deplacement=Vector3.up;
       }
       if (Input.GetKey(KeyCode.DownArrow))
       {
           deplacement=Vector3.down;
       } 
       if (Input.GetKey(KeyCode.RightArrow))
       {
           deplacement=Vector3.right;
       } 
       if (Input.GetKey(KeyCode.LeftArrow))
       {
           deplacement=Vector3.left;
       } 

       transform.Translate(deplacement*speed*Time.deltaTime);

       // S'il y a un contact avec un mur, la vitesse est nulle donc on arrête le déplacement
       //if (rigidbody.velocity.x==0f || rigidbody.velocity.y==0f)
       //{
         //  deplacement=Vector3.zero;
       //}
        
    }
}

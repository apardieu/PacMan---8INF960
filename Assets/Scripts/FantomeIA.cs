using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class FantomeIA : MonoBehaviour
{
    private Vector2 destination = Vector2.zero;
    private Vector2 destinationStart1 = new Vector2(0, 1.12f);
    private Vector2 destinationStart2 = new Vector2(0.48f, 1.12f);
    private int direction;
    private int oldDirection;
    public float speed = 0.05f;
    public float step = 0.32f;
    public float stepCollider = 0.32f;
    private int counter = 0;
    private bool start1 = false;
    private bool start2 = false;
    public bool warp = false;

    [SerializeField] private Tilemap tilemap;



    public void Start()
    {
        direction = Random.Range(0, 3);
        oldDirection = direction;
        start1 = true;
        start2 = true;
    }


    int ChasePacman()
    {
        Vector3 positionPacman = GameObject.FindWithTag("pacman").transform.position;

        Sprite sprite = null;


        if (System.Math.Round(transform.position.x, 2) == System.Math.Round(positionPacman.x, 2)) //on the same horizontal line
        {
            float difference = Mathf.Abs(positionPacman.y - transform.position.y);
            float ratio = difference / step;

            for (int i = 1; i < ratio; i++)
            {
                if (positionPacman.y >= transform.position.y) // pacman is on top of the ghost
                    sprite = tilemap.GetSprite(tilemap.WorldToCell(new Vector2(transform.position.x, transform.position.y + i * step)));

                else
                    sprite = tilemap.GetSprite(tilemap.WorldToCell(new Vector2(transform.position.x, transform.position.y - i * step)));
                
                if (sprite == null)
                    return -1;
                else if (!((sprite.name == "pacman_map_3") || (sprite.name == "pacman_map_6") || (sprite.name == "pacman_map_4")))
                    return -1;
            }
            if (positionPacman.y >= transform.position.y)
            {
                if (ColliderCheck(0))
                    return 0;
            }
            else
            {
                if (ColliderCheck(2))
                    return 2;
            }

        }
        else if (System.Math.Round(transform.position.y, 2) == System.Math.Round(positionPacman.y, 2))
        {
            float difference = Mathf.Abs(positionPacman.x - transform.position.x);
            float ratio = difference / step;

            for (int i = 1; i < ratio; i++)
            {
                if (positionPacman.x >= transform.position.x) //pacman is on the right 
                    sprite = tilemap.GetSprite(tilemap.WorldToCell(new Vector2(transform.position.x + i * step, transform.position.y)));

                else
                    sprite = tilemap.GetSprite(tilemap.WorldToCell(new Vector2(transform.position.x - i * step, transform.position.y)));
                
                if (sprite == null)
                    return -1;
                else if (!((sprite.name == "pacman_map_3") || (sprite.name == "pacman_map_6") || (sprite.name == "pacman_map_6")))
                    return -1;
            }
            if (positionPacman.x >= transform.position.x)
            {
                if (ColliderCheck(1))
                    return 1;
            }
            else
            {
                if (ColliderCheck(3))
                    return 3;
            }

        }

        return -1;
    }

    public void InitGhost()
    {
        if (transform.name == "blinky")
        {
            Vector2 tempPosition = Vector2.MoveTowards(transform.position, destinationStart1, speed);
            GetComponent<Rigidbody2D>().MovePosition(tempPosition);
            if ((Vector2)transform.position == destinationStart1)
                start1 = false;
            if ((start1 == false) && (start2 == true))
            {
                tempPosition = Vector2.MoveTowards(transform.position, destinationStart2, speed);
                GetComponent<Rigidbody2D>().MovePosition(tempPosition);
            }
            if ((Vector2)transform.position == destinationStart2)
                start2 = false;


        }

        else if (transform.name == "clyde")
        {
            if (counter >= 180)
            {
                Vector2 tempPosition = Vector2.MoveTowards(transform.position, destinationStart1, speed);
                GetComponent<Rigidbody2D>().MovePosition(tempPosition);
                if ((Vector2)transform.position == destinationStart1)
                    start1 = false;
                if ((start1 == false) && (start2 == true))
                {
                    tempPosition = Vector2.MoveTowards(transform.position, destinationStart2, speed);
                    GetComponent<Rigidbody2D>().MovePosition(tempPosition);
                }
                if ((Vector2)transform.position == destinationStart2)
                    start2 = false;
            }
        }

        else if (transform.name == "inky")
        {
            if (counter >= 360)
            {
                Vector2 tempPosition = Vector2.MoveTowards(transform.position, destinationStart1, speed);
                GetComponent<Rigidbody2D>().MovePosition(tempPosition);
                if ((Vector2)transform.position == destinationStart1)
                    start1 = false;
                if ((start1 == false) && (start2 == true))
                {
                    tempPosition = Vector2.MoveTowards(transform.position, destinationStart2, speed);
                    GetComponent<Rigidbody2D>().MovePosition(tempPosition);
                }
                if ((Vector2)transform.position == destinationStart2)
                    start2 = false;
            }
        }

        else if (transform.name == "pinky")
        {
            if (counter >= 480)
            {
                Vector2 tempPosition = Vector2.MoveTowards(transform.position, destinationStart1, speed);
                GetComponent<Rigidbody2D>().MovePosition(tempPosition);
                if ((Vector2)transform.position == destinationStart1)
                    start1 = false;
                if ((start1 == false) && (start2 == true))
                {
                    tempPosition = Vector2.MoveTowards(transform.position, destinationStart2, speed);
                    GetComponent<Rigidbody2D>().MovePosition(tempPosition);
                }
                if ((Vector2)transform.position == destinationStart2)
                    start2 = false;
            }
        }

        if (start2 == false)
            destination = transform.position;

        counter++;
    }

    void FixedUpdate()
    {

        if (warp)
        {
            warp = false;
            counter = 0;
            start1 = true;
            start2 = true;
            transform.position = new Vector2(0, 0.16f);
        }

        if ((start1 == true) || (start2 == true))
            InitGhost();

        else
        {
            int directionChasePacman;
            Vector2 p = Vector2.MoveTowards(transform.position, destination, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);


            if ((Vector2)transform.position == p)
            {
                speed = 0.05f;

                directionChasePacman = ChasePacman();
                if (directionChasePacman == (-1))
                {
                    if (direction == 0) //up
                    {
                        if (ColliderCheck(0))
                        {
                            destination.y = transform.position.y + step;
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

                    else if (direction == 1) //right
                    {
                        if (ColliderCheck(1))
                        {
                                destination.x = transform.position.x + step;
                            if (destination.x > 4.317f)
                            {
                                destination.x = -4.317f;
                                speed = 10000;
                            }


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

                    else if (direction == 2) //down
                    {
                        if (ColliderCheck(2))
                        {
                            destination.y = transform.position.y - step;
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

                    else if (direction == 3) //left
                    {
                        if (ColliderCheck(3))
                        {
                            destination.x = transform.position.x - step;
                            if (destination.x < -4.317f)
                            {
                                destination.x = 4.317f;
                                speed = 10000;
                            }

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
                    direction = directionChasePacman;
                    if (direction == 0)
                        destination.y = transform.position.y + step;
                    else if (direction == 1)
                        destination.x = transform.position.x + step;
                    else if (direction == 2)
                        destination.y = transform.position.y - step;
                    else if (direction == 3)
                        destination.x = transform.position.x - step;
                }
            }

            Vector2 animation = destination - (Vector2)transform.position;
            GetComponent<Animator>().SetFloat("DirX", animation.x);
            GetComponent<Animator>().SetFloat("DirY", animation.y);
        }
    }


    bool ColliderCheck (int direction)
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
        else
        {
            return true;
        }
    }
    
}

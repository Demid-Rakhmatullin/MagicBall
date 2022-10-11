using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public float velocity;
    public float height;
    public float moveDelay;

    private float startY;
    private bool elevate;
    private bool moveUp;
    private bool moveDown;

    void Start()
    {
        startY = transform.position.y;
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player") && !elevate)
        {
            Invoke("MoveUp", moveDelay);
        }
    }

    void FixedUpdate()
    {
        if (elevate)
        {
            var currY = transform.position.y;
            if (moveUp)
            {
                var nextY = currY + velocity;
                if (nextY >= startY + height)
                {
                    nextY = startY + height;
                    elevate = false;
                    moveUp = false;
                    Invoke("MoveDown", moveDelay);
                }
                transform.position = new Vector3(transform.position.x, nextY, transform.position.z);
            }
            else if (moveDown)
            {
                var nextY = currY - velocity;
                if (nextY <= startY)
                {
                    nextY = startY;
                    elevate = false;
                    moveDown = false;
                }
                transform.position = new Vector3(transform.position.x, nextY, transform.position.z);
            }
        }
    }

    private void MoveUp()
    {
        elevate = true;
        moveUp = true;
    }

    private void MoveDown()
    {
        elevate = true;
        moveDown = true;
    }
}

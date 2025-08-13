using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movepad : MonoBehaviour
{
    public float moveDistance = 10f;
    public float speed = 4f;
    public int angle = 3;

    private Vector3 startPos;
    private bool moving = true;


    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        switch (angle)
        {
            case 1:
                MoveX();
                break;
            case 2:
                MoveY();
                break;
            case 3:
                MoveZ();
                break;
            default:
                break;
        }

    }

    void MoveX()
    {
        if (moving)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

            if (transform.position.x >= startPos.x + moveDistance)
            {
                moving = false;
            }
        }
        else
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

            if (transform.position.x <= startPos.x)
            {
                moving = true;
            }
        }
    }

    void MoveY()
    {
        if (moving)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);

            if (transform.position.y >= startPos.y + moveDistance)
            {
                moving = false;
            }
        }
        else
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

            if (transform.position.y <= startPos.y)
            {
                moving = true;
            }
        }
    }

    void MoveZ()
    {
        if (moving)
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);

            if (transform.position.z >= startPos.z + moveDistance)
            {
                moving = false;
            }
        }
        else
        {
            transform.position -= new Vector3(0, 0, speed * Time.deltaTime);

            if (transform.position.z <= startPos.z)
            {
                moving = true;
            }
        }
    }


}

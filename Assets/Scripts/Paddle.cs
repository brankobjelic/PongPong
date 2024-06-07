using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public int id;
    public float moveSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        float movement = ProcessInput();
        Move(movement);
    }

    private float ProcessInput()
    {
        float movement = 0f;
        switch (id)
        {
            case 1:
                movement = Input.GetAxis("MovePlayer1");    //  -1, 0 or 1
                break;
            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
        }
        return movement;
    }

    private void Move(float movement)
    {
        Vector2 vlct = rb2d.velocity;
        vlct.y = movement * moveSpeed;
        rb2d.velocity = vlct;
    }

    public bool IsLeftPaddle()
    {
        return id == 1;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public int id;
    public float moveSpeed = 2f;
    private Vector3 _originalLocalScale;
    private Vector3 _extendedScale;
    private Vector3 _shrunkScale;
    private bool extended = false;
    private bool shrunk = false;

    private void Start()
    {
        _originalLocalScale = transform.localScale;
        _shrunkScale = _originalLocalScale - new Vector3(0, 0.33f, 0);
        _extendedScale = _originalLocalScale + new Vector3(0, 0.5f, 0);
    }

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
                movement = Input.GetAxis("MovePlayer1");    //  -1, 0 or 1. "MovePlayer1" is the name given in Project Settings/Input Manager
                break;
            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
        }
        return movement;
    }

    private void Move(float movement)
    {
        //Vector2 vlct = rb2d.velocity;
        //vlct.y = movement * moveSpeed;
        //rb2d.velocity = vlct;
        rb2d.velocity = new Vector2(0, movement * moveSpeed);
    }

    public bool IsLeftPaddle()
    {
        return id == 1;
    }

    public void Extend()
    {
        this.transform.localScale = _extendedScale;
    }

    public void Shrink()
    {
        this.transform.localScale = _shrunkScale;
    }

    public void ResetPaddleSize()
    {
        this.transform.localScale = _originalLocalScale;
    }
}

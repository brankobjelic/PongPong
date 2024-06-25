using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //public GameManager gameManager;
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.67f;
    public float maxPaddleBounceAngle = 45f;
    public float moveSpeed = 3f;
    public float moveSpeedMultiplier = 1.05f;

    public Vector3 initialPosition;

    //on reset x position starts from the middle, y position will be random
    public float startX = 0f;
    private readonly float maxStartY = 0f;   //private so it cannot be changed from Inspector

    private void Start()
    {
        GameManager.instance.onReset += ResetBall;  //assign ResetBall method to OnReset Action variable (delegate)
        GameManager.instance.gameUI.onStartGame += ResetBall;
        initialPosition = transform.position;
    }
    
    private void InitialPush()
    {
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;
    }

    private void ResetBall()
    {
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new(startX, posY);
        transform.position = position;
        InitialPush();
    }

    // MonoBehaviour.OnTriggerEnter2D(Collider2D) - Sent when another object enters a trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<ScoreZone>(out var scoreZone))
        {
            GameManager.instance.OnScoreZoneReached(scoreZone.id);
        } 
        if(collision.TryGetComponent<PlusOne>(out var plusOne))
        {
            GameManager.instance.OnPlusOnePickedUp(plusOne);
        }
        if(collision.TryGetComponent<ExtendPaddle>(out var extendPaddle))
        {
            GameManager.instance.OnExtendPaddlePickedUp(extendPaddle);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<Paddle>(out var paddle))
        {
            rb2d.velocity *= moveSpeedMultiplier;
            AdjustAngle(paddle, collision);
            GameManager.instance.audio.PlayPaddleHitSound();
            GameManager.instance.lastPlayed = paddle.id;
        }

        if (collision.collider.TryGetComponent<Wall>(out var wall))
        {
            GameManager.instance.audio.PlayWallHitSound();
        }
    }

    private void AdjustAngle(Paddle paddle, Collision2D collision)
    {
        //ball bounce angle depending on point on a paddle
        float absoluteDistanceFromCenter = collision.otherCollider.transform.position.y - paddle.transform.position.y;
        float relativeDistanceFromCenter = absoluteDistanceFromCenter * 2 / paddle.transform.localScale.y;

        int angleSign = paddle.IsLeftPaddle() ? 1 : -1;
        float paddleBounceAngle = relativeDistanceFromCenter * angleSign * maxPaddleBounceAngle;
        Quaternion rotation = Quaternion.AngleAxis(paddleBounceAngle, Vector3.forward);

        Vector2 direction = paddle.IsLeftPaddle() ? Vector2.right : Vector2.left;
        rb2d.velocity = rotation * direction * rb2d.velocity.magnitude;

    }
}

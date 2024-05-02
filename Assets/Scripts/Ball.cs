using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 3f;

    //on reset x position starts from the middle, y position will be random
    public float startX = 0f;
    private float maxStartY = 0f;   //private so it cannot be changed from Inspector

    private void Start()
    {
        InitialPush();
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
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();  //checking if object with witch the ball collided has the ScoreZone component
        if(scoreZone != null )
        {
            gameManager.OnScoreZoneReached(scoreZone.id);
            ResetBall();
            InitialPush();
        }
    }
}

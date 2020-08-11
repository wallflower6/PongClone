using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{

    private Rigidbody2D ballRb;
    private float minSpeed = 10f;
    private float maxSpeed = 30f;
    private float speed = 10f;

    // time taken for transition
    private float duration = 60f;
    private float startTime;

    private int scoreLeft = 0;
    private int scoreRight = 0;

    private Transform topWall;
    private Transform bottomWall;

    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        topWall = GameObject.Find("WallTop").transform;
        bottomWall = GameObject.Find("WallBottom").transform;

        // starting velocity and time
        ballRb.velocity = Vector2.right * speed;
        startTime = Time.time;
    }

    void Update() {

        // updates scoreboard
        GameObject.Find("ScoreLeft").GetComponent<Text>().text = scoreLeft.ToString();
        GameObject.Find("ScoreRight").GetComponent<Text>().text = scoreRight.ToString();
        
        // increases speed of the ball over a period of 60 seconds
        float t = (Time.time - startTime) / duration;
        speed = Mathf.SmoothStep(minSpeed, maxSpeed, t);

        // if ball is somehow pushed out of the top/bottom collider, reset its position and give it the initial velocity
        if (transform.position.y > topWall.position.y || transform.position.y < bottomWall.position.y) {
            transform.position = new Vector3(0, 0, 0);
            ballRb.velocity = Vector2.right * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "RacketLeft") {
            float y = hitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);

            // calculate direction and normalize (length = 1)
            Vector2 direction = new Vector2(1, y).normalized;
            ballRb.velocity = direction * speed;
            
        } else if (other.gameObject.name == "RacketRight") {
            float y = hitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);

            // calculate direction and normalize (length = 1)
            Vector2 direction = new Vector2(-1, y).normalized;
            ballRb.velocity = direction * speed;

        } else if (other.gameObject.name == "WallLeft") {
            // updates opponent score if hit wall
            scoreRight++;
        } else if (other.gameObject.name == "WallRight") {
            // updates opponent score if hit wall
            scoreLeft++;
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight) {
        // divide the ball's y coordinate by the racket's height
        // we subtract the racketPos.y from the ballPos.y to have a relative position
        return (ballPos.y - racketPos.y) / racketHeight;
    }
}

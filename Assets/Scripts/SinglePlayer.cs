using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : MonoBehaviour
{

    private GameObject ball;
    private GameObject racketLeft;

    private float d;
    private Vector3 move;
    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
        racketLeft = GameObject.Find("RacketLeft");
    }

    // Update is called once per frame
    void Update()
    {
        d = ball.transform.position.y - racketLeft.transform.position.y;
        if (d > 0) {
            move.y = speed * Mathf.Min(d, 1.0f);
        } else if (d < 0) {
            move.y = -(speed * Mathf.Min(-d, 1.0f));
        }
        racketLeft.transform.position += move * Time.deltaTime;

        // racketLeft.transform.position = new Vector3(racketLeft.transform.position.x, ball.transform.position.y, 0);
    }
}

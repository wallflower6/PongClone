using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float speed = 20f;
    private AudioSource popAudio;

    private GameObject singlePlayerController;

    public string Axis = "Vertical";

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        popAudio = GetComponent<AudioSource>();
        singlePlayerController = GameObject.Find("SinglePlayer");
    }

    // upwards and downwards only
    // x component always 0, y component upwards 1 downwards -1, depends on user input
    void FixedUpdate() {
        float y;
        if (!PlayerSelection.singlePlayerMode) {
            singlePlayerController.SetActive(false);
            y = Input.GetAxisRaw(Axis);
        } else {
            singlePlayerController.SetActive(true);
            y = Input.GetAxisRaw("Vertical2");
        }
        rb.velocity = new Vector2(0, y) * speed;
    }

    void OnCollisionEnter2D(Collision2D ball) {
        popAudio.Play();
    }
}

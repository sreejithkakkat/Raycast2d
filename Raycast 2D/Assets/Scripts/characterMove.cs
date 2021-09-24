using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMove : MonoBehaviour {

    float moveInput;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float obstacleRayDistance;

    private Rigidbody2D rb;

    public GameObject groundRayObject;
    public GameObject obstacleRayObject;

    bool jumpOn;
    Animator anim;

    float characterDirection;

    public LayerMask layerMask;

    private void Start () {

        rb = GetComponent<Rigidbody2D> ();
        jumpOn = false;
        characterDirection = 0f;

        //ANIMATION
        anim = GetComponent<Animator> ();
        //ANIMATION

    }

    private void FixedUpdate () {

        //Movement
        moveInput = Input.GetAxis ("Horizontal");
        rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);
        //Movement

        //ANIMATION
        if (moveInput < 0) {
            anim.SetBool ("leftRun", true);
            anim.SetBool ("rightRun", false);
            characterDirection = -1f;
        } else if (moveInput > 0) {
            anim.SetBool ("leftRun", false);
            anim.SetBool ("rightRun", true);
            characterDirection = 1f;
        } else {
            anim.SetBool ("leftRun", false);
            anim.SetBool ("rightRun", false);
            characterDirection = 0f;
        }
        //ANIMATION

        //Jump
        RaycastHit2D hitGround = Physics2D.Raycast (groundRayObject.transform.position, -Vector2.up);
        Debug.DrawRay (groundRayObject.transform.position, -Vector2.up * hitGround.distance, Color.red);
        if (hitGround.collider != null) {

            if (hitGround.distance <= .2) {
                jumpOn = true;

            } else {
                jumpOn = false;
            }
        } else {
            return;
        }
        //Jump

    }

    private void Update () {
        //Enemy
        RaycastHit2D hitObstacle = Physics2D.Raycast (obstacleRayObject.transform.position, Vector2.right * new Vector2 (characterDirection, 0f), obstacleRayDistance, layerMask);

        if (hitObstacle.collider != null) {

            Debug.DrawRay (obstacleRayObject.transform.position, Vector2.right * hitObstacle.distance * new Vector2 (characterDirection, 0f), Color.red);
            Debug.Log ("Enemy Detected");
        } else {
            Debug.DrawRay (obstacleRayObject.transform.position, Vector2.right * obstacleRayDistance * new Vector2 (characterDirection, 0f), Color.green);

        }
        //Enemy

        //Jump
        if (Input.GetKeyDown (KeyCode.Space)) {

            if (jumpOn == true) {
                rb.velocity = Vector2.up * jumpForce;

            } else {
                return;
            }

        }
        //Jump

    }

}
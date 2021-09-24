using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    float moveInput;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float obstacleRayDistance;
    Rigidbody2D rb;

    public GameObject groundRayObject;
    public GameObject obstacleRayObject;

    float characterDirection;

    bool jumpOn;

    public LayerMask layerMask;

    void Start () {
        jumpOn = false;
        rb = GetComponent<Rigidbody2D> ();
        characterDirection = 0f;
    }

    void FixedUpdate () {
        moveInput = Input.GetAxis ("Horizontal");
        rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);

        RaycastHit2D hitGround = Physics2D.Raycast (groundRayObject.transform.position, -Vector2.up);
        Debug.DrawRay (groundRayObject.transform.position, -Vector2.up * hitGround.distance, Color.red);

        if (hitGround.collider != null) {
            if (hitGround.distance <= 0.2) {
                jumpOn = true;
            } else {
                jumpOn = false;
            }
        }

    }

    private void Update () {

        if (moveInput < 0) {
            characterDirection = -1f;
        } else if (moveInput > 0) {
            characterDirection = 1;
        } else {
            characterDirection = 0f;
        }

        RaycastHit2D hitObstacle = Physics2D.Raycast (obstacleRayObject.transform.position, Vector2.right * new Vector2 (characterDirection, 0f), obstacleRayDistance, layerMask);

        if (hitObstacle.collider != null) {
            Debug.Log ("Enemy Detected  Attack Mode Activated");
            Debug.DrawRay (obstacleRayObject.transform.position, Vector2.right * hitObstacle.distance * new Vector2 (characterDirection, 0f), Color.red);

        } else {
            Debug.Log (" NO Enemy");
            Debug.DrawRay (obstacleRayObject.transform.position, Vector2.right * obstacleRayDistance * new Vector2 (characterDirection, 0f), Color.green);
        }

        if (Input.GetKeyDown (KeyCode.Space)) {

            if (jumpOn == true) {
                rb.velocity = Vector2.up * jumpForce;
            } else {
                return;
            }

        }
    }
}
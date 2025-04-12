using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrogger : MonoBehaviour
{
    public float accelerationSpeed = 10f;
    public float maxSpeed = 10f;
    public float jumpForce = 10f;
    public float jumpTime = .5f;
    public bool grounded = true;
    public ForceMode2D forceMode;

    Vector3 moveDir;
    Vector3 lastMove;
    Vector3 jumpScale;
    Rigidbody2D rb;
    Collider2D col;
    float jumpEnd;
    bool overGround = true;
    
    float SqrMaxSpeed
    {
        get => maxSpeed * maxSpeed;
    }

    void GetInput()
    {
        moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir = moveDir + Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir = moveDir + Vector3.left;
            //transform.localScale = new Vector2(-flipScale, transform.localScale.y);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir = moveDir + Vector3.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir = moveDir + Vector3.right;
            //transform.localScale = new Vector2(flipScale, transform.localScale.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (moveDir != Vector3.zero) lastMove = moveDir;

    }

    void Jump()
    {
        if (grounded)
        {
            jumpEnd = Time.time + jumpTime;
            grounded = false;
            jumpScale = transform.localScale;
            rb.AddForce(lastMove * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        if (!grounded && Time.time >= jumpEnd)
        {
            grounded = true;
        }

        if (Time.time < jumpEnd)
        {
            
            float start = jumpEnd - jumpTime;
            float progress = (jumpEnd - Time.time) / (jumpTime);
            float progScale = progress < .5f ? Mathf.Lerp(1f, 3f, progress) : Mathf.Lerp(3f, 1f, progress);
            //print($"erm? {progress} {progScale}");
            transform.localScale = new Vector2(jumpScale.x * progScale, jumpScale.y * progScale);
        }

        if (!overGround && grounded)
        {
            print("we should be dead!");
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude < SqrMaxSpeed && grounded)
        {
            rb.AddForce(moveDir.normalized * accelerationSpeed, forceMode);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            overGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            overGround = false;
        }
    }
}

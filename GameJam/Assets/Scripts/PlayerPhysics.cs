using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    public float accelerationSpeed = 10f;
    public float maxSpeed = 10f;
    public float jumpForce = 10f;
    public float jumpUpMult = 2f;
    public ForceMode2D forceMode;

    public ContactFilter2D groundFilter;
    public bool IsGrounded => rb.IsTouching(groundFilter);

    Vector3 moveDir;
    Rigidbody2D rb;
    Collider2D col;
    float flipScale;

    float SqrMaxSpeed
    {
        get => maxSpeed * maxSpeed;
    }

    public bool IsGroundedTo(Collider2D col)
    {
        return rb.IsTouching(col, groundFilter);
    }

    void GetInput()
    {
        moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            //moveDir = moveDir + Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir = moveDir + Vector3.left;
            transform.localScale = new Vector2(-flipScale, transform.localScale.y);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //moveDir = moveDir + Vector3.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir = moveDir + Vector3.right;
            transform.localScale = new Vector2(flipScale, transform.localScale.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (!IsGrounded) return;

        moveDir += Vector3.up * jumpUpMult;
        rb.AddForce(moveDir.normalized * jumpForce, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        flipScale = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude < SqrMaxSpeed && IsGrounded)
        {
            print($"{IsGrounded} {moveDir}");
            rb.AddForce(moveDir.normalized * accelerationSpeed, forceMode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //col.Raycast()
        GetInput();

    }
}

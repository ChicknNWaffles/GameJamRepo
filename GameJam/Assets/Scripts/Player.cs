using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } = null;
    public float Speed;
    public float JumpHeight;
    private bool Jumping;
    private bool Mode; // false = side, true = top
    private bool CanGoLeft = true;
    private bool CanGoRight = true;
    private bool CanGoUp = true;
    private bool CanGoDown = true;
    private bool CanJump = true;
    private Transform transform;
    private int Health;
    private Vector3 Target;
    private bool OnTheGround = true;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start(){
        transform = gameObject.transform;
        Target = transform.position;
        
    }

    // Update is called once per frame
    void Update(){
        // respond to player input
        Pathfind();
        // apply gravity
        Gravity();
        // move the player towards the target
        transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
        // if we've reached the target, and the player is jumping, stop jumping, and let gravity take over
        if (Jumping && transform.position == Target) {
            Jumping = false;
        }

    }

    #region movement

    void Pathfind() {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)) {
            ResetTarget();
        }else if (Input.GetKey(KeyCode.W)) {
            GoUp();
        }else if (Input.GetKey(KeyCode.A)) {
            GoLeft();
        }else if (Input.GetKey(KeyCode.S)) {
            GoDown();
        }else if (Input.GetKey(KeyCode.D)) {
            GoRight();
        }else if (Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }else if (Input.GetKeyDown(KeyCode.Return)){
            print("shoot");
        }
    }

    void Gravity() {
        // if not currently on our way up with a jump, not in top down mode, and not currently
        // in contact with the ground
        if (!Jumping && Mode && !OnTheGround) {
            float x = Target.x;
            float y = Target.y;
            // move the target down
            float curZ = Target.z;
            float newZ = curZ + 1;
            Target = new (x, y, newZ);
        }
    }

    void Jump() {
        // if on the ground and in from side mode and if can jump
        if (OnTheGround && !Mode && CanJump) { 
            float x = Target.x;
            float y = Target.y;
            // move the target up
            float curZ = Target.z;
            float newZ = curZ - JumpHeight;
            Target = new (x, y, newZ);
            Jumping = true;
        }
    }

    void GoLeft() { 
        if (CanGoLeft) {
            float y = Target.y;
            float z = Target.z;
            // move the target to the left
            float curX = Target.x;
            float newX = curX - 10;
            Target = new(newX, y, z);
            print("OldX = " + curX + ", NewX = " + newX);
        }
    }

    void GoRight() { 
        if (CanGoRight) {
            float y = Target.y;
            float z = Target.z;
            // move the target to the right
            float curX = Target.x;
            float newX = curX + 10;
            Target = new(newX, y, z);
            print("OldX = " + curX + ", NewX = " + newX);
        }
    }

    void GoUp() { 
        if (CanGoUp && Mode) {
            float x = Target.x;
            float z = Target.z;
            // move the target up
            float curY = Target.x;
            float newY = curY - 10;
            Target = new(x, newY, z);
        }
    }

    void GoDown() { 
        if (CanGoDown && Mode) {
            float x = Target.x;
            float z = Target.z;
            // move the target down
            float curY = Target.x;
            float newY = curY + 10;
            Target = new(x, newY, z);
        }
    }

    void ResetTarget() {
        print("resetting target");
        Target = transform.position;
    }

    #endregion


    #region collision

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "collide") {
            // if colliding on the left
            if (transform.position.x <= collision.gameObject.transform.position.x) {
                // disable movement to the left
                CanGoLeft = false;
            }
            // if colliding on the right
            else if (transform.position.x > collision.gameObject.transform.position.x) {
                // disable movement to the right
                CanGoRight = false;
            }
            // if colliding above
            else if (transform.position.y <= collision.gameObject.transform.position.y) {
                // disable movement upward
                CanGoUp = false;
                // disable jumping
                CanJump = false;
            }
            // if colliding below
            else if (transform.position.y > collision.gameObject.transform.position.y) {
                // disable movement downward
                CanGoDown = false;
                // let the program know you're on the ground if in side mode
                if (!Mode) {
                    OnTheGround = true;
                }
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.tag == "collide") {
            // if colliding on the left
            if (transform.position.x <= collision.gameObject.transform.position.x) {
                //enable movement to the left
                CanGoLeft = true;
            }
            // if colliding on the right
            else if (transform.position.x > collision.gameObject.transform.position.x) {
                //enable movement to the right
                CanGoRight = true;
            }
            // if colliding above
            else if (transform.position.y <= collision.gameObject.transform.position.y) {
                //enable movement upward
                CanGoUp = true;
                //enable jumping
                CanJump = true;
            }
            // if colliding below
            else if (transform.position.y > collision.gameObject.transform.position.y) {
                //enable movement downward
                CanGoDown = true;
                // let the program know you're not on the ground if in side mode
                if (!Mode)
                {
                    OnTheGround = false;
                }
            }
        }
    }

    #endregion




}

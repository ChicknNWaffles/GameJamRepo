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
    public bool CanGoLeft = true;
    public bool CanGoRight = true;
    public bool CanGoUp = true;
    public bool CanGoDown = true;
    public bool CanJump = true;
    private Rigidbody2D rb;
    private int Health;
    private Vector3 Target;
    private bool OnTheGround = false;
    private bool PanningLeft = false;
    private bool PanningRight = false;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start(){
        Target = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        // check if it needs to move the player or the camera;
        CheckIfPanning();
        // apply gravity
        Gravity();
        // respond to player input
        Pathfind();
        // move the player towards the target
        Vector3 dir = (Target - transform.position).normalized;
        rb.MovePosition(transform.position + (dir * Speed * Time.deltaTime));
        // if we've reached the target, and the player is jumping, stop jumping, and let gravity take over
        if (Jumping && transform.position == Target) {
            Jumping = false;
        }

    }

    #region movement

    void Pathfind() {
        ResetTarget();
        if (Input.GetKey(KeyCode.W)) {
            GoUp();
        }else if (Input.GetKey(KeyCode.A)) {
            GoLeft();
        }else if (Input.GetKey(KeyCode.S)) {
            GoDown();
        }else if (Input.GetKey(KeyCode.D)) {
            GoRight();
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Return)){
            print("shoot");
        }
    }

    void CheckIfPanning() {
        if (Game.Instance.camera.transform.position.x <= 0 && transform.position.x >= Game.Instance.camera.transform.position.x - 3) {
            PanningLeft = false;
            PanningRight = true;
        }else if (Game.Instance.camera.transform.position.x >= Game.Instance.LevelWidth && transform.position.x <= Game.Instance.camera.transform.position.x + 3) {
            PanningLeft = true;
            PanningRight = false;
        }else if (Game.Instance.camera.transform.position.x <= 0 || Game.Instance.camera.transform.position.x >= Game.Instance.LevelWidth){
            PanningLeft = false;
            PanningRight = false;
        }else{
            PanningLeft = true;
            PanningRight = true;
        }
    }

    void Gravity() {
        // if not currently on our way up with a jump, not in top down mode, and not currently
        // in contact with the ground
        if (!Jumping && !Mode && !OnTheGround) {
            float x = Target.x;
            float z = Target.z;
            // move the target down
            float curY = Target.y;
            float newY = curY - 10;
            Target = new(x, newY, z);
        }
    }

    void Jump() {
        // if on the ground and in from side mode and if can jump
        if (OnTheGround && !Mode && CanJump) {
            float x = Target.x;
            float z = Target.z;
            // move the target up
            float curY = Target.y;
            float newY = curY + JumpHeight;
            Target = new(x, newY, z);
            Jumping = true;
        }
    }

    void GoLeft() { 
        if (CanGoLeft) {
            float y = Target.y;
            float z = Target.z;
            // move the target to the left
            float curX = transform.position.x;
            float newX = curX - 10;
            Target = new(newX, y, z);
            if (PanningLeft){
                Game.Instance.camera.transform.Translate(Vector3.left * Speed * Time.deltaTime);
            }
        }

        
    }

    void GoRight() { 
        if (CanGoRight) {
            float y = Target.y;
            float z = Target.z;
            // move the target to the right
            float curX = transform.position.x;
            float newX = curX + 10;
            Target = new(newX, y, z);
            if (PanningRight) {
                Game.Instance.camera.transform.Translate(Vector3.right * Speed * Time.deltaTime); 
            }
        }
    }

    void GoUp() { 
        if (CanGoUp && Mode) {
            float x = Target.x;
            float z = Target.z;
            // move the target up
            float curY = transform.position.y;
            float newY = curY + 10;
            Target = new(x, newY, z);
        }
    }

    void GoDown() { 
        if (CanGoDown && Mode) {
            float x = Target.x;
            float z = Target.z;
            // move the target down
            float curY = transform.position.y;
            float newY = curY - 10;
            Target = new(x, newY, z);
        }
    }

    void ResetTarget() {
        Target = transform.position;
    }

    #endregion


    #region collision

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Collide") {
            // if colliding on the left
            if (transform.position.x < collision.GetContact(0).point.x && collision.GetContact(0).point.y > transform.position.y - 0.7f) {
                // disable movement to the left
                CanGoLeft = false;
            }
            // if colliding on the right
            else if (transform.position.x > collision.GetContact(0).point.x && collision.GetContact(0).point.y > transform.position.y - 0.7f) {
                // disable movement to the right
                CanGoRight = false;
            }
            // if colliding above
            else if (transform.position.y > collision.GetContact(0).point.y) {
                // disable movement upward
                CanGoUp = false;
                // disable jumping
                CanJump = false;
            }
            // if colliding below
            else if (transform.position.y < collision.GetContact(0).point.y) {
                // disable movement downward
                CanGoDown = false;
                // let the program know you're on the ground if in side mode
                if (!Mode) {
                    OnTheGround = true;
                }
            }
            Debug.Log("Collide " + collision.gameObject.name);
        }
    }

    private void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject.tag == "Collide") {
            // if colliding on the left
            if (transform.position.x < collision.GetContact(0).point.x && collision.GetContact(0).point.y > transform.position.y - 0.7f) {
                // disable movement to the left
                CanGoLeft = false;
            }
            // if colliding on the right
            else if (transform.position.x > collision.GetContact(0).point.x && collision.GetContact(0).point.y > transform.position.y - 0.7f) {
                // disable movement to the right
                CanGoRight = false;
            }
            // if colliding above
            else if (transform.position.y > collision.GetContact(0).point.y) {
                // disable movement upward
                CanGoUp = false;
                // disable jumping
                CanJump = false;
            }
            // if colliding below
            else if (transform.position.y < collision.GetContact(0).point.y) {
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
        // set everything to be able to move and let
        // OnCollisionStay fix it if that was wrong
        CanGoLeft = true;
        CanGoRight = true;
        CanGoUp = true;
        CanJump = true;
        CanGoDown = true;
        if (!Mode){
            OnTheGround = false;
        }
    }

    #endregion




}

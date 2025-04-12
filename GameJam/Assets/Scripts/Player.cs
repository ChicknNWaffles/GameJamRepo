using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } = null;
    private bool Jumping;
    private bool Mode; // false = side, true = top
    private float Speed;
    private bool CanGoLeft = true;
    private bool CanGoRight = true;
    private bool CanGoUp = true;
    private bool CanGoDown = true;
    private bool CanJump = true;
    private Vector3 position;
    private int health;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start(){
        position = gameObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region movement
    void Gravity() {
        if (!Jumping) { 
        }
    }

    void Jump() { 

    }

    void GoLeft() { 

    }

    void GoRight() { 

    }

    void Crouch() { 

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "collide") {
            // if colliding on the left
            if (position.x <= collision.gameObject.transform.position.x) {
                // disable movement to the left
                CanGoLeft = false;
            }
            // if colliding on the right
            else if (position.x > collision.gameObject.transform.position.x) {
                // disable movement to the right
                CanGoRight = false;
            }
            // if colliding above
            else if (position.y <= collision.gameObject.transform.position.y) {
                // disable movement upward
                CanGoUp = false;
                // disable jumping
                CanJump = false;
            }
            // if colliding below
            else if (position.y > collision.gameObject.transform.position.y) {
                // disable movement downward
                CanGoDown = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.tag == "collide") {
            // if colliding on the left
            if (position.x <= collision.gameObject.transform.position.x) {
                //enable movement to the left
                CanGoLeft = true;
            }
            // if colliding on the right
            else if (position.x > collision.gameObject.transform.position.x) {
                //enable movement to the right
                CanGoRight = true;
            }
            // if colliding above
            else if (position.y <= collision.gameObject.transform.position.y) {
                //enable movement upward
                CanGoUp = true;
                //enable jumping
                CanJump = true;
            }
            // if colliding below
            else if (position.y > collision.gameObject.transform.position.y) {
                //enable movement downward
                CanGoDown = true;
            }
        }
    }


    #endregion


}

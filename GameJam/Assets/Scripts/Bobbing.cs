using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    public float BobbingSpeed;
    public float BobbingDistance;
    private float UpperBound;
    private float LowerBound;
    private bool goingUp = true;

    // Start is called before the first frame update
    void Start(){
        UpperBound = transform.position.y + BobbingDistance;
        LowerBound = transform.position.y - BobbingDistance;
    }

    // Update is called once per frame
    void Update(){

        if (goingUp) { 
            transform.Translate(Vector3.up * Time.deltaTime * BobbingSpeed);
        }else { 
            transform.Translate(Vector3.down  * Time.deltaTime * BobbingSpeed);
        }

        if (transform.position.y >= UpperBound) {
            goingUp = false;
        }else if (transform.position.y <= LowerBound) {
            goingUp = true;
        }else{
            print("Not at either bound, Upper bound is " + UpperBound + ", LowerBound is " + LowerBound + ", Currently at, " + transform.position.y);
        }

    }
}

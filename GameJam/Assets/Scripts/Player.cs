using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } = null;
    private bool Jumping;
    private float Speed;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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


    #endregion


}

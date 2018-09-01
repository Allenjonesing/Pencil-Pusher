// Player Control
//    Handles user input, keyboard=debug Touch=user


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Private variables
    private Rigidbody2D rb2D;
    private float thrustx = 10.0f;   //thrust along x axis
    private float x = 0f;           //User input x axis for debug
    private float y = 0f;           //User input x axis for debug

    //public variables
    public float thrusty;           //thrust along x axis
    public int angVelInc;
    public int hp = 100;

    // Init
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    //Collect user input
    void GetInput()
    {
        //Get key input
        x = Input.GetAxisRaw("Horizontal"); //-1, 0 or 1
        y = Input.GetAxisRaw("Vertical"); //-1, 0 or 1

        rb2D.AddForce(transform.up * thrusty * y);
        rb2D.AddForce(transform.right * thrustx * x);

        if (Input.GetKeyDown("space"))
        {
            //Debug.Log("Space pressed, Angular Velocity incremented by angVelInc");
            rb2D.angularVelocity+= angVelInc;

        }
    }
}
    

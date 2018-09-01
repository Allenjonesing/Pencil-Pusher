//pencilControl.cs
//Houses equilibrium code, COM, Rotation, dampaning AngVel.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilControl : MonoBehaviour {

    //Private variables
    private Rigidbody2D rb2D;

    //public variables
    public Vector3 com;             //Center of mass
    public Vector3 onGroundCom;     //COM while on the ground
    public Vector3 inAirCom;        //COM while in the air
    public Vector3 stuckCom;        //COM while stuck lying down
    public float maxAngVel;
    public float minRot;
    private int timer;
    private int timer2;
    public bool onGround;

    public float kickupBoost;
    public float initKickBoost;

    public float jumpAngBoost;
    private float rb2DRoatation;

    public bool dead;
    public Vector3 lastPos;
    public  float stuckThresh;
    public  int stuckTimeOut;

    //initialization
    void Start ()
    {
        timer = 0;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.centerOfMass = com;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Dampen();
            GetUp();
        }
        

    }
    //Dampen ANgVel for standing
    void Dampen()
    {
        //clamp rotaion
        if (rb2D.rotation > 360) 
            rb2D.rotation -= 360;
        if (rb2D.rotation < 0) 
            rb2D.rotation += 360;

        //apply dampen
        if ((Mathf.Abs(rb2D.angularVelocity) > maxAngVel) 
            && (Mathf.Abs(rb2D.rotation) < minRot) 
            && (timer <= 0) 
            && onGround)
        {
            //Too Fast!!! Angular velocity halved, timer set to 30");
            rb2D.angularVelocity /= 5;
            timer = 30;
        }
        timer--;
    }

    //Get up when stuck lying down
    void GetUp()
    {
        //for debug
        rb2DRoatation = rb2D.rotation;
        //CHeck if stuck
        /*if (((Mathf.Abs(rb2D.rotation) > 89) && (Mathf.Abs(rb2D.rotation) < 91)) 
            || ((Mathf.Abs(rb2D.rotation) > 269) && (Mathf.Abs(rb2D.rotation) < 271)))
        */
        lastPos = transform.position;
        if (Mathf.Abs(lastPos.x-transform.position.x) < stuckThresh)
        {
            timer2++;
            //Check if stuck for long
            if (timer2 >= stuckTimeOut)
            {
                //Talk to us
                Debug.LogAssertion("Stuck was indicated, init recovery");
                //NEgative indicates lying with head to the righht
                //COM to head to init 
                rb2D.centerOfMass = com = stuckCom;
                //give boost to raise feet, 
                rb2D.angularVelocity += rb2D.rotation * initKickBoost;
                //give boost downward to kick up
                rb2D.AddForce(transform.up * kickupBoost);
                //give boost to rotate and land
                rb2D.angularVelocity += rb2D.rotation * jumpAngBoost;
                //COM to feet to continue
                rb2D.centerOfMass = com = onGroundCom;
                //Reset timer
                timer2 = 0;

            }
           // else
                //timer2=0;   //Spending time lying doen
        }
        else
            timer2 = 0;     //Reset timer because not stuck
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //check that the collision was with floor
        if (collision.gameObject.tag == "Ground" && !dead)
        {
            //("Collision! COM and com set to onGroundCom, onGround true");
            rb2D.centerOfMass = com = onGroundCom;
            onGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //check that the collision was with floor
        if (collision.gameObject.tag == "Ground" && !dead)
        {
            //Debug.Log("Collision Exited, COm and com set to inAirCom, onGround false");
            onGround = false;
            rb2D.centerOfMass = com = inAirCom;

        }
    }

}

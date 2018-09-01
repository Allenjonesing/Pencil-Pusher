//Enemycontroller.cs
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PencilControl pencilControl;
    private GameObject player;
    private Rigidbody2D rb2D;
    private float thrust = 10.0f;
    private float x = 0f;
    private float y = 0f;
    public int hp = 100;

    public float thrusty;
    public float playerDistLimit;
    public float playerDistInRange;
    public float playerAngRotLimit;
    public float velocityLimit;
    public float angVelLimit;
    public float moveForce;

   
    float playerDist;
    bool playerInAir;
    float playerAngRot;
    Vector3 playerPos;

    enum AIStates
    {
        Wait,
        Approach,
        jump,
        Kick,
        Land,
        Recover,
        Avoid
    } AIStates aiState;

    public float angVelInc;

    public float minRot;
    public float maxRot;

    public float angVelIncPump;

    public float maxVel;

    public int dmg;


    // Use this for initialization
    void Start()
    {
        if (this.gameObject.tag == "MockPlayer")
            player = GameObject.FindGameObjectWithTag("Enemy");
        else
            player = GameObject.FindGameObjectWithTag("MockPlayer");

        aiState = AIStates.Wait;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        pencilControl = gameObject.GetComponent<PencilControl>();
    }



    // Update is called once per frame
    void Update()
    {
        TakeStimuli();
        Decide();
        Act();
        AmAlive();

    }

    private void AmAlive()
    {
        //am i alive?
        if (hp <= 0)
        {
            gameObject.GetComponent<PencilControl>().dead = true;
            rb2D.centerOfMass = new Vector3(0,0);
            enabled = false;
        }
    }

    private void TakeStimuli()
    {
        //any necessary information
        playerPos = player.transform.position;                              //useful for approach and avoid
        playerDist = Vector3.Distance(playerPos, transform.position);       //useful for jump and attack
        playerInAir = !player.GetComponent<PencilControl>().onGround;       //useful for attack and land
        playerAngRot = player.GetComponent<Rigidbody2D>().angularVelocity;  //useful for avoid 
    }

    private void Decide()
    {
        //decision makiing...Importance in order of AIStates  
        //Firstly, are we too far to attack
        if (playerDist >= playerDistLimit)
        {
            //Too far, approach if target is grounded
            if (!playerInAir)
            {
                //Approach
                aiState = AIStates.Approach;

            }
            else //Player in air, possibly attacking
            {
                //avoid
                aiState = AIStates.Avoid;
            }
        }
        else //not too far to attack
        {
            //if player is grounded, attack
            if (!playerInAir)
            {
                //jump
                aiState = AIStates.jump;
            }
            else // player in air
            {
                aiState = AIStates.Avoid;
            }
        } 
        //Next, are we in the air, we may have just jumped
        if (!pencilControl.onGround)
        {
            //We've jumped, time an attack
            if (playerDist <= playerDistInRange)
            {
                //kick
                aiState = AIStates.Kick;

            }
            else //wait for time to attack, unless falling to ground
            {
                //not in range, falling?
                if (rb2D.velocity.y >= velocityLimit)
                {
                    //landing
                    //causeing issue, approach for recovery //aiState = AIStates.Land;
                    aiState = AIStates.Approach;
                }
                else //wait for attack
                {
                    aiState = AIStates.Wait;
                }
            }
            //Finally, check for recovery options
            if ((rb2D.angularVelocity >= angVelLimit) || (rb2D.rotation > minRot) || (rb2D.rotation < maxRot))
            {
                //if on ground, recovber
                if (pencilControl.onGround)
                {
                    aiState = AIStates.Wait;
                }
            }

        }
        
    }

    private void Act()
    {
        //Act on decision using switch
        switch(aiState)
        {
            case AIStates.Approach:
                //Too far to attack, move closer
                //allocate temp var//Determine direction to spin
                float difference = angVelInc = playerPos.x - transform.position.x;
                //Determine direction to move
                if (rb2D.velocity.x<maxVel)
                    rb2D.AddForce(new Vector2(difference * moveForce, 0));
                //amp the angVelInc
                angVelInc *= angVelIncPump;
                     
                break;

            case AIStates.Avoid:
                //Too close to attack, Move further
                //Determine direction to move
                rb2D.AddForce(new Vector2((transform.position.x - playerPos.x) * moveForce, 0));
                break;

            case AIStates.jump:
                //In range, init attack by jumping
                rb2D.AddForce(transform.up * thrusty);
                break;

            case AIStates.Kick:
                //In air and range for attack, init spin
                rb2D.angularVelocity += angVelInc;
                break;

            case AIStates.Land:
                //Coming down, rotate for landing
                //not very important
                break;

            case AIStates.Recover:
                //AngVel too fast while on ground, wait for recovery
                //deprecatable
                break;

            case AIStates.Wait:
                //Default, wait.
                break;

            default:
                break;
        }
    }
}
/*
void OnCollisionEnter(Collision collision)
{
    rb2D.centerOfMass = com = onGroundCom;
}

void OnCollisionExit(Collision collision)
{
    rb2D.centerOfMass = com = inAirCom;
}
}*/

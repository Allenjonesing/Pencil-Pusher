using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public int dmgInc;
    private int timer3;

    // Use this for initialization
    void Start()
    {
        timer3 = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* void OnCollisionEnter2D(Collision2D collision)
     {
         Debug.Log("HEAD Collision! That's gonna Hurt!");
         // Debug-draw all contact points and normals
         if (collision.gameObject.tag == "Foot")
             Debug.LogAssertion("FOOT TO FACE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
         */

    /*foreach (ContactPoint2D contact in collision.contacts)
    {
        Debug.DrawRay(contact.point, contact.normal, Color.white);
    }*/

    // if (collision.relativeVelocity.magnitude > 2)
    // Debug.LogWarning("OH, that's gonna hurt ALOT!!!!!");
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Decrease hp
        //if (this.gameObject.tag == "MockPlayer")
        if (timer3 >= 0)
        {
            this.gameObject.GetComponentInParent<EnemyController>().hp -= 1;
            timer3--;
            //else
        }
            //this.gameObject.GetComponentInParent<EnemyController>().hp -= 0;
        Debug.Log((gameObject.name) + " TOOK A HEAD Collision! That's gonna Hurt!");
        // Debug-draw all contact points and normals
        if ((collision.gameObject.tag == "Foot")/* && (collision.GetComponentInParent<GameObject>().tag != this.GetComponentInParent<GameObject>().tag)*/)
        {
            Debug.LogAssertion((gameObject.name) + " TOOK A FOOT TO FACE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            timer3 = gameObject.GetComponentInParent<EnemyController>().dmg;
            //if (this.gameObject.tag == "MockPlayer")
                this.gameObject.GetComponentInParent<EnemyController>().hp -= gameObject.GetComponentInParent<EnemyController>().dmg;
            //else
               // this.gameObject.GetComponentInParent<EnemyController>().hp -= 10;
        }

    }
}

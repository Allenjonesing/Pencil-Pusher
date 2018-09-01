// TouchControl
// Allen Jones
// For use with Dew
// Created 8/30/16
//
// determines if an object is touched

using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour
{
    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private Rigidbody2D rb2D;
    public float maxVel;
    public float moveForce;
    public float thrusty;
    public float minXSwipe;
    public float minYSwipe;

    public GameManager GameManager;

    //Var
    private Vector2 touchCache;
    private int screenHeight;
    private int screenWidth;
    // private float maxPickingDistance = 10000;// increase if needed, depending on your scene size
    // private Vector3 startPos;
    // private Transform pickedObject = null;

    //The renderer component
    private SpriteRenderer rend;

    //future pos of touch in 3d space
    // private Vector2 pos, pos2;
    //  private Vector3 movement;
    private GameObject ooMover;
    private RaycastHit2D hitInfo;
    private float xx;
    private float yy;
    private Touch touch;
    public bool firstTouch;

    // Use this for initialization
    void Start()
    {
        firstTouch = true;
        //Set rend, our renderer component
        rend = GetComponent<SpriteRenderer>();

        //Cache called function variables
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        rb2D = gameObject.GetComponent<Rigidbody2D>();
        ooMover = null;
    }

    // Update is called once per frame
    void Update()
    {

        //////////If running game in editor////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR

        MouseSwipe();

#endif
        //////////END If running game in editor////////////////////////////////////////////////////////////////////

        Swipe();
        ApplyForce();
        currentSwipe = Vector2.zero;
    }

    public void MouseSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

           
        }
       
    }

    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

               
            }
        }
    }

    public void ApplyForce()
    {
        
        if (currentSwipe.x>minXSwipe)
            rb2D.AddForce(new Vector2(currentSwipe.x * moveForce, 0));
        if ((currentSwipe.y > minYSwipe) && (GetComponent<PencilControl>().onGround))
            rb2D.AddForce(new Vector2(0, currentSwipe.y * thrusty));



    }
}

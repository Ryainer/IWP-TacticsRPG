using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    private Touch theTouch;
    private CharacterController controller;

    public GameObject Player;
    public GameObject AtkButton;
    public GameObject SkillButton;
    public GameObject CancelButton;

    public Joystick joystick;

    private Vector2 touchStart, touchEnd, touchNew, joystickDir;
    private bool stop = false;
    private bool foward, backward, right, left;
    private float speed = 0.2f;
    private float gravity = 9.8f;
    private Vector3 move;

    private void Awake()
    {
        
        
    }

    private void Start()
    {
       // controller = Player.GetComponent<CharacterController>();
        //move = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Touch.activeTouches.Count > 0) //check how many touches
        {
            theTouch = Touch.activeTouches[0];

            if (theTouch.phase == TouchPhase.Began || restart) //check if began or it went over the screen
            {
                restart = false;
                touchStart = theTouch.screenPosition;

                if (touchStart.x > Screen.width * 0.5f || touchStart.y > Screen.height * 0.7f)
                {
                    Move(Vector2.zero);
                    return;
                }
            }
            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended) //when we move joystick
            {
                if (AtkButton.activeInHierarchy && SkillButton.activeInHierarchy && CancelButton.activeInHierarchy)
                {
                    AtkButton.SetActive(false);
                    SkillButton.SetActive(false);
                    CancelButton.SetActive(false);
                }

                touchEnd = theTouch.screenPosition;

                Vector2 offset = touchEnd - touchNew;

                if (offset.sqrMagnitude > (Screen.width / 4) * (Screen.height / 4))
                {
                    restart = true;
                }
                touchNew = touchEnd; //store the previous end position into a new variable;

                joystickDir = (touchEnd - touchStart) / 2;

                Move(joystickDir);
            }
            if (theTouch.phase == TouchPhase.Ended)
            {
                GameObject snap = closestTile();
                Vector3 changes = Player.transform.position;

                changes.x = snap.transform.position.x;
                changes.z = snap.transform.position.z;

                Player.transform.position = changes;

                if (!AtkButton.activeInHierarchy && !SkillButton.activeInHierarchy && !CancelButton.activeInHierarchy)
                {
                    AtkButton.SetActive(true);
                    SkillButton.SetActive(true);
                    CancelButton.SetActive(true);
                }
            }
        }*/
        controller = Player.GetComponent<CharacterController>();
        controller.Move(new Vector3(joystick.Horizontal * 0.2f, -(gravity * Time.deltaTime), joystick.Vertical * 0.2f));
        if (controller.velocity != Vector3.zero)
        {
            if (AtkButton.activeInHierarchy && SkillButton.activeInHierarchy && CancelButton.activeInHierarchy)
            {
                AtkButton.SetActive(false);
                SkillButton.SetActive(false);
                CancelButton.SetActive(false);
            }
          
        }
        else if (controller.velocity == Vector3.zero )
        {
            if (!AtkButton.activeInHierarchy && !SkillButton.activeInHierarchy && !CancelButton.activeInHierarchy)
            {
                AtkButton.SetActive(true);
                SkillButton.SetActive(true);
                CancelButton.SetActive(true);
            }
            
        }

        //move.y -= gravity * Time.deltaTime;
        //move.Normalize();
        //controller.Move(move);
    }

   

    //public void onUpButtonPress()
    //{
    //    move.z = 2f;

    //    move.y -= gravity * Time.deltaTime;
    //    move.Normalize();

    //    controller.Move(move);
    //}

    //public void onDownButtonPress()
    //{
    //    Vector3 moveDown = new Vector3(0f, 0f, -2f);

    //    moveDown.y -= gravity * Time.deltaTime;

    //    controller.Move(moveDown);
    //}

    //private void Move(Vector2 dir)
    //{
    //    foward = (dir.y > 0);
    //    backward = (dir.y < 0);
    //    left = (dir.x < -0.5);
    //    right = (dir.x > 0.5);

    //    Vector3 nxtMove = Vector3.zero;

    //    if (foward)
    //        nxtMove += Vector3.forward * Time.deltaTime * speed;
    //    if (backward)
    //        nxtMove += Vector3.back * Time.deltaTime * speed;
    //    if (right)
    //        nxtMove += Vector3.right * Time.deltaTime * speed;
    //    if (left)
    //        nxtMove += Vector3.left * Time.deltaTime * speed;

    //    nxtMove.y -= gravity * Time.deltaTime;

    //    nxtMove.Normalize();

    //    controller.Move(nxtMove);
    //}

    
}

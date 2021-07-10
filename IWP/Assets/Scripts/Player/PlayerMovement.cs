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

    public GameObject actionButtons;
    public Camera camera;

    public Joystick joystick;

    private Vector2 touchStart, touchEnd, touchNew, joystickDir;
    private bool stop = false;
    private bool foward, backward, right, left;
    private float speed = 0.2f;
    private float gravity = 9.8f;
    private Vector3 move;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
       // controller = Player.GetComponent<CharacterController>();
        //move = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMov = joystick.Horizontal;
        float verticalMov = joystick.Vertical;

        Vector3 camForward = camera.transform.forward;
        Vector3 camRight = camera.transform.right;

        camForward.y = 0.0f;
        camRight.y = 0.0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 direction = camForward * verticalMov + camRight * horizontalMov;


        controller = Player.GetComponent<CharacterController>();
        controller.Move(new Vector3(direction.x * 0.2f, -(gravity * Time.deltaTime), direction.z * 0.2f));
        if (controller.velocity != Vector3.zero)
        {
            if (actionButtons.activeInHierarchy)
            {
                actionButtons.SetActive(false);
            }
          
        }
        else if (controller.velocity == Vector3.zero )
        {
            if (!actionButtons.activeInHierarchy)
            {
               // actionButtons.SetActive(true);

                if (Player.GetComponent<Warrior>() != null && Player.GetComponent<Warrior>().state != "attack")
                {
                    actionButtons.SetActive(true);
                }
                else if (Player.GetComponent<Archer>() != null && Player.GetComponent<Archer>().state != "attack")
                {
                    actionButtons.SetActive(true);
                }
            }
        }
    }

    private void LateUpdate()
    {
        camera.transform.LookAt(Player.transform.position);
    }

}

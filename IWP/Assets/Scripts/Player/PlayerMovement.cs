using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;

    public GameObject Player;
    public GameObject AtkButton;
    public GameObject SkillButton;
    public GameObject CancelButton;

    public GameObject actionButtons;
    public Camera camera;

    public Joystick joystick;

    private float gravity = 9.8f;
    public Vector3 origin;
    private string tileTag;
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



        if (Player != null)
        {
            controller = Player.GetComponent<CharacterController>();
            Vector3 nextPosition = new Vector3((Player.transform.position.x + direction.x * 0.05f), Player.transform.position.y + 1,
                (Player.transform.position.z + direction.z * 0.05f));
            // Debug.Log("Position of player is: " + nextPosition + "\n The direction is: " + direction);
            Ray ray = new Ray(nextPosition, Vector3.down);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                tileTag = hit.collider.gameObject.tag;
               
            }
            else
            {
                Debug.Log("nothing hit");
            }


            if (tileTag != "Tiles")
            {
                controller.Move(new Vector3(direction.x * 0.05f, -(gravity * Time.deltaTime), direction.z * 0.05f));

                if (Player.transform.position.y < 0)
                {
                    GameObject respawnLocation = closestTile();

                    Vector3 respawn = Player.transform.position;
                    respawn.x = respawnLocation.transform.position.x;
                    respawn.z = respawnLocation.transform.position.z;

                    Player.transform.position = respawn;
                }

            }
            

            if (controller.velocity != Vector3.zero)
            {
                if (actionButtons.activeInHierarchy)
                {
                    actionButtons.SetActive(false);
                }

            }
            else if (controller.velocity == Vector3.zero)
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

        if (Player != null)
        {
            if (Player.GetComponent<Warrior>() != null && Player.GetComponent<Warrior>().state != "attack")
            {
                camera.transform.LookAt(Player.transform.position);
            }
            else if (Player.GetComponent<Archer>() != null && Player.GetComponent<Archer>().state != "attack")
            {
                camera.transform.LookAt(Player.transform.position);
            }
        }
    }


    GameObject closestTile()
    {
        GameObject[] closestTiles;

        closestTiles = GameObject.FindGameObjectsWithTag("SelectedTiles");
        GameObject theClosest = null;
        float distance = Mathf.Infinity;
        Vector3 position = Player.transform.position;

        foreach (GameObject indiviTile in closestTiles)
        {
            Vector3 diff = indiviTile.transform.position - position;
            float currentDist = diff.sqrMagnitude;

            if (currentDist < distance)
            {
                theClosest = indiviTile;
                distance = currentDist;
            }
        }

        return theClosest;
    }

}

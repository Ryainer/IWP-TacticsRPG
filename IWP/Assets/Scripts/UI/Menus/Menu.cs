using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject warriorMenu;
    public GameObject healerMenu;
    public GameObject archerMenu;
    public GameObject atkConfirmation;
    public GameObject character;
    public GameObject turns;

    public GameObject combatControlButtons;
    public GameObject actionButtons;
    public GameObject joycon;
    public GameObject pauseButton;
    public GameObject dmgpanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onAtkBtnPress()
    {
        if (character.GetComponentInChildren<PlayerRanges>().enemies.Count > 0)
        {
            string[] chck = character.name.Split(' ');

            if (chck[0] == "archerBlue")
            {
                character.GetComponent<Archer>().state = "attack";
            }
            else if (chck[0] == "knightBlue")
            {
                character.GetComponent<Warrior>().state = "attack";
            }

            actionButtons.SetActive(false);
            dmgpanel.SetActive(false);
            pauseButton.SetActive(false);
            joycon.SetActive(false);

            combatControlButtons.SetActive(true);
            atkConfirmation.SetActive(true);
            atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("attack");

            FindObjectOfType<cullingMask>().ignoreMask(true);
        }
        else
        {
            Debug.Log("Error no enemies");
        }
    }

    public void onSkillBtnPress()
    {
        if(character != null)
        {
            string[] chck = character.name.Split(' ');

            if (chck[0] == "archerBlue")
            {
                archerMenu.SetActive(true);
                archerMenu.GetComponent<ArcherMenu>().Archer = character;
            }
            else if (chck[0] == "knightBlue")
            {
                warriorMenu.SetActive(true);
                warriorMenu.GetComponent<WarriorMenu>().warrior = character;
            }
        }
        
    }

    public void onEndTurnBtnPress()
    {
        if(turns.GetComponent<TurnsManager>().getTurn())
        {
            GameObject[] Tiles;

            Tiles = GameObject.FindGameObjectsWithTag("SelectedTiles");

            foreach(GameObject tile in Tiles)
            {
                Material OGMaterial = Resources.Load<Material>("Materials/grass");

                tile.GetComponent<MeshRenderer>().material = OGMaterial;
                tile.tag = "Tiles";
            }


            turns.GetComponent<TurnsManager>().setTurn(false);
            turns.GetComponent<TurnsManager>().swapControls();
            if(character != null)
            {
                character.transform.GetChild(5).gameObject.SetActive(false);
                character.transform.GetChild(6).gameObject.SetActive(false);
                character = null;
            }
            
        }
    }

}

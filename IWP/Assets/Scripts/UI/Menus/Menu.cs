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
    public GameObject archer;
    public GameObject turns;

    public GameObject combatControlButtons;
    public GameObject actionButtons;
    public GameObject joycon;

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
        
        if(character.name == "archerBlue")
        {
            character.GetComponent<Archer>().state = "attack";
        }
        else if(character.name == "knightBlue")
        {
            character.GetComponent<Warrior>().state = "attack";
        }

        actionButtons.SetActive(false);
        joycon.SetActive(false);

        combatControlButtons.SetActive(true);
        atkConfirmation.SetActive(true);
        atkConfirmation.GetComponent<PlayerChooseTarget>().targetsSelect("attack");
    }

    public void onSkillBtnPress()
    {
        if(character != null)
        {
            if(character.name == "archerBlue")
            {
                archerMenu.SetActive(true);
                archerMenu.GetComponent<ArcherMenu>().Archer = character;
            }
            else if (character.name == "knightBlue")
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
            turns.GetComponent<TurnsManager>().setTurn(false);
            turns.GetComponent<TurnsManager>().swapControls();
            if(character != null)
            {
                character.transform.GetChild(5).gameObject.SetActive(false);
                character = null;
            }
            else if(archer != null)
            {
                archer.transform.GetChild(5).gameObject.SetActive(false);
                archer = null;
            }
        }
    }

}

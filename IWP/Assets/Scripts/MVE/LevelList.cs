using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelList : MonoBehaviour
{
    public int levelChosen = 1;
    private int img = 0;

    public List<LevelData> levels = new List<LevelData>();
    public List<Sprite> imgs = new List<Sprite>();

    public Image imageofLevel;
    public TextMeshProUGUI lvltxt;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void onNxtPress()
    {
        if(levelChosen < 3 && img < 3)
        {
            levelChosen++;
            img++;
            imageofLevel.sprite = imgs[img];
            lvltxt.text = "Level " + (levelChosen + 1);
        }

    }

    public void onPrevPress()
    {
        if (levelChosen > 0 && img > 0)
        {
            levelChosen--;
            img--;
            imageofLevel.sprite = imgs[img];
            lvltxt.text = "Level " + (levelChosen + 1);
        }
    }
}

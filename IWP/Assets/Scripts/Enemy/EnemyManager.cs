using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject soldierEnemy;
    public GameObject archerEnemy;
    private GameObject enemytype;
    GameObject[] Tiles;
    public GameObject turns;
    int maximum = 0;
    public bool chooseNewEnemyControl;
    public LayerMask ignore;

    // Start is called before the first frame update
    void Start()
    {
        chooseNewEnemyControl = false;
        Tiles = GameObject.FindGameObjectsWithTag("Tiles");
        createEnemies();  
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject enemy in enemies)
        { 
            if(enemy.GetComponent<EnemyBehaviour>().eneHealth <= 0)
            {
                enemies.Remove(enemy);
                GameObject.Destroy(enemy);
            }
           
        }
    }

    public void createEnemies()
    {
        Debug.Log("Creating enemies");
        int num = 1;
        for(int i = 0; i < 3; ++i)
        {
            // Instantiate(soldierEnemy, new Vector3(5, 5, 5), Quaternion.identity);
            Vector3 spawnPos = getSpawnPos();

            spawnPos.y += 100f;

            Ray ray = new Ray(spawnPos, Vector3.down);

            RaycastHit hitinfo;

            if (Physics.Raycast(ray, out hitinfo, 100f, ~ignore))
            {
                spawnPos = hitinfo.point;
            }
            int choice = Random.Range(0, 1);
            if(choice == 0)
            {
                enemytype = Instantiate(soldierEnemy, spawnPos, Quaternion.identity);
                enemytype.name = "Soldier " + num;
            }
            else if(choice == 1)
            {
                enemytype = Instantiate(archerEnemy, spawnPos, Quaternion.identity);
                enemytype.name = "Archer " + num;
            }
            //spawnPos.y += 0.5f;
            //soldierEnemy.name = "soldier" + num;
            //num++;
            //enemytype = Instantiate(soldierEnemy, spawnPos, Quaternion.identity);
            //enemytype.GetComponent<Rigidbody>().AddForce(new Vector3(0f,3f,0f),ForceMode.Impulse);
            enemies.Add(enemytype);
        }
    }

    Vector3 getSpawnPos()
    {
       
        GameObject[] Players;
        Players = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log("Tiles count" + Tiles.Length);
        int randomTile = Random.Range(0, (Tiles.Length - maximum));
       // Debug.Log("chosen tile" + randomTile);
        Vector3 spawnPos = new Vector3(Tiles[randomTile].transform.position.x,
            Tiles[randomTile].transform.position.y, Tiles[randomTile].transform.position.z);

        //foreach (GameObject player in Players)
        //{
        //    if(spawnPos.sqrMagnitude <= player.transform.position.sqrMagnitude)
        //    {
        //        maximum++;
        //        getSpawnPos();
        //    }
        //}


        return spawnPos;
    }

    public void chooseAnEnemy()
    {
        int range = Random.Range(0, enemies.Count);
        Debug.Log(enemies[range].name);
        enemies[range].GetComponent<EnemyBehaviour>().setSwitch(true);
        enemies[range].GetComponent<EnemyBehaviour>().chooseAction();

        string name = enemies[range].name.Split(' ')[0];
       
        if(name == "Archer")
        {
            enemies[range].GetComponent<EnemyBehaviour>().namechck = name;
        }
        else if(name == "Soldier")
        {
            enemies[range].GetComponent<EnemyBehaviour>().namechck = name;
        }
    }
}

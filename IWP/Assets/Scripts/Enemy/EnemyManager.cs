﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //[HideInInspector]
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
        if(enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetComponent<EnemyBehaviour>().eneHealth <= 0)
                {
                    GameObject enemytodestroy = enemies[i];
                    enemies.Remove(enemies[i]);
                    Destroy(enemytodestroy);
                }
            }
            //GameObject.Find("TurnManager").GetComponent<TurnsManager>().currentEnemy = enemies;
        }
      
    }

    public void createEnemies()
    {
        Debug.Log("Creating enemies");
        int num = 1;
        //for(int i = 0; i < 3; ++i)
        //{
           
        //    Vector3 spawnPos = getSpawnPos();

        //    spawnPos.y += 100f;

        //    Ray ray = new Ray(spawnPos, Vector3.down);

        //    RaycastHit hitinfo;

        //    if (Physics.Raycast(ray, out hitinfo, 350f, ~ignore))
        //    {
        //        spawnPos = hitinfo.point;
        //    }
        //    int choice = Random.Range(0, 1);
        //    if(choice == 0)
        //    {
        //        enemytype = Instantiate(soldierEnemy, spawnPos, Quaternion.identity);
        //        //Debug.Log(enemytype.transform.position);
        //        enemytype.name = "Soldier " + num;
        //    }
        //    else if(choice == 1)
        //    {
        //        enemytype = Instantiate(archerEnemy, spawnPos, Quaternion.identity);
        //        //Debug.Log(enemytype.transform.position);
        //        enemytype.name = "Archer " + num;
        //    }
        //    num++;
        //    enemies.Add(enemytype);
        //}

        while(num != 4)
        {
            Vector3 spawnPos = getSpawnPos();

            if(Physics.CheckSphere(spawnPos, 0.5f, 10))
            {
                Debug.Log("inside someone");
            }
            else
            {
                spawnPos.y += 20f;

                Ray ray = new Ray(spawnPos, Vector3.down);

                RaycastHit hitinfo;

                if (Physics.Raycast(ray, out hitinfo, 350f, ~ignore))
                {
                    spawnPos = hitinfo.point;
                }
                int choice = Random.Range(0, 1);
                if (choice == 0)
                {
                    enemytype = Instantiate(soldierEnemy, spawnPos, Quaternion.identity);
                    //Debug.Log(enemytype.transform.position);
                    enemytype.name = "Soldier " + num;
                }
                else if (choice == 1)
                {
                    enemytype = Instantiate(archerEnemy, spawnPos, Quaternion.identity);
                    //Debug.Log(enemytype.transform.position);
                    enemytype.name = "Archer " + num;
                }
                num++;
                enemies.Add(enemytype);
            }
        }
    }

    Vector3 getSpawnPos()
    {
       
        GameObject[] Players;
        
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
        Update();
        if(enemies.Count > 0)
        {
            int range = Random.Range(0, enemies.Count);
            Debug.Log(enemies[range].name);
            enemies[range].GetComponent<EnemyBehaviour>().setSwitch(true);
            enemies[range].GetComponent<EnemyBehaviour>().chooseAction();
            enemies[range].GetComponent<EnemyBehaviour>().ignoreself = ignore;
            string name = enemies[range].name.Split(' ')[0];

            if (name == "Archer")
            {
                enemies[range].GetComponent<EnemyBehaviour>().namechck = name;
                Debug.Log("its an archer");
            }
            else if (name == "Soldier")
            {
                enemies[range].GetComponent<EnemyBehaviour>().namechck = name;
                Debug.Log("its a soldier" + enemies[range].name);
            }
        }
        //else if(enemies.Count == 0)
        //{
        //    GameObject.Find("TurnManager").GetComponent<TurnsManager>().endgame("enemy loss");
        //}
    }
}

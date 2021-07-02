using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanges : MonoBehaviour
{
    public List<GameObject> enemies;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            var results = enemies.FindAll(s => s.Equals(other.gameObject.name));
            if(results.Count <= 0)
            {
                enemies.Add(other.gameObject);
                Debug.Log("ADDED");
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
            enemies.Remove(other.gameObject);
            Debug.Log("Removed");
        
    }

    private void Update()
    {
        for (var i = enemies.Count - 1; i > -1; i--)
        {
            if (enemies[i] == null)
                enemies.RemoveAt(i);
        }
    }
}

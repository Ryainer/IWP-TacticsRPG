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
}

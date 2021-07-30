using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRange : MonoBehaviour
{
    public List<GameObject> playersInRange = new List<GameObject>();

    public List<GameObject> TilesInrange = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public List<GameObject> GetPlayersInCollider(/*Vector3 position*/)
    {
        List<GameObject> playersdetected = new List<GameObject>();

        Collider[] collidersdetected = Physics.OverlapSphere(transform.position, 5f);

        foreach (Collider collider in collidersdetected)
        {
            if (collider.gameObject.tag == "Player")
            {
                var results = playersdetected.FindAll(s => s.Equals(collider.gameObject.name));

                if (results.Count <= 0)
                {
                    playersdetected.Add(collider.gameObject);
                    Debug.Log("target added");
                }           
            }
        }

        return playersdetected;
    }

    public void GetTilesInCollider()
    {
        Collider[] collidersdetected = Physics.OverlapSphere(transform.position, 10f);

        foreach (Collider collider in collidersdetected)
        {
            if (collider.gameObject.tag == "Tiles")
            {
                var results = TilesInrange.Exists(s => s.Equals(s.transform.position));
                if(!results)
                {
                    TilesInrange.Add(collider.gameObject);
                    Debug.Log("tile added");
                }

                
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}

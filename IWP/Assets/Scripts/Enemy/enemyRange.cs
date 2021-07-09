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

    public void GetPlayersInCollider()
    {
        Collider[] collidersdetected = Physics.OverlapSphere(transform.position, 20f);

        foreach (Collider collider in collidersdetected)
        {
            if (collider.gameObject.tag == "Player")
            {
                playersInRange.Add(collider.gameObject);
                Debug.Log("target added");
            }
        }
    }

    public void GetTilesInCollider()
    {
        Collider[] collidersdetected = Physics.OverlapSphere(transform.position, 5f);

        foreach (Collider collider in collidersdetected)
        {
            if (collider.gameObject.tag == "Tiles")
            {
                TilesInrange.Add(collider.gameObject);
                Debug.Log("tile added");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 20f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullingMask : MonoBehaviour
{
    public LayerMask ignore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public  void ignoreMask(bool activate)
    {
        if(activate)
        {
            Camera.main.cullingMask = ~ignore;
        }
        else
        {
            Camera.main.cullingMask = 12 << 1;
        }
    }
}

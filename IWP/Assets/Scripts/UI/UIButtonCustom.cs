using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonCustom : MonoBehaviour
{
    
    public GameObject cameraRig;

  
    public float movementSpeed;
    public float movementTime;
    public float rotationamt;

    public Vector3 camPosition;
    public Quaternion camRotation;
    // Start is called before the first frame update
    void Start()
    {
        movementTime = 4f;

        rotationamt = 7f;
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.2f;
      

        camPosition = cameraRig.transform.position;
        camRotation = cameraRig.transform.rotation;
    }

    private void Update()
    {
        //cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation,
        //    camRotation, Time.deltaTime * movementTime);
    }

    public void onLeftArrow()
    {
        camRotation *= Quaternion.Euler(Vector3.up * rotationamt);
        cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation,
            camRotation, Time.deltaTime * movementTime);
    }

    public void onRightArrow()
    {
        camRotation *= Quaternion.Euler(Vector3.up * -rotationamt);
        cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation,
            camRotation, Time.deltaTime * movementTime);
    }

   public void onSliderChange(float vol)
    {
        FindObjectOfType<AudioManager>().adjustVolume(vol);
    }
}
   


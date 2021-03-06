using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonCustom : MonoBehaviour
{
    private Camera camera;
    public GameObject cameraRig;
    float rotatingspeed;

    public float movementSpeed;
    public float movementTime;
    public float rotationamt;

    public Vector3 camPosition;
    public Quaternion camRotation;
    // Start is called before the first frame update
    void Start()
    {
        movementTime = 4f;
        rotatingspeed = 5f;
        rotationamt = 7f;
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.2f;
        camera = Camera.main;

        camPosition = cameraRig.transform.position;
        camRotation = cameraRig.transform.rotation;
    }

    private void Update()
    {
        cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation, 
            camRotation, Time.deltaTime * movementTime);
    }

    public void onLeftArrow()
    {
        camRotation *= Quaternion.Euler(Vector3.up * rotationamt);
        
    }

    public void onRightArrow()
    {
        camRotation *= Quaternion.Euler(Vector3.up * -rotationamt);
    }

}
   


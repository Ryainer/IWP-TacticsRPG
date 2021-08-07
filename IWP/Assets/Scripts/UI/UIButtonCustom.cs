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

    public GameObject board;

    private bool turnRight;
    private bool turnLeft;
    // Start is called before the first frame update
    void Start()
    {
        movementTime = 0.5f;

        rotationamt = 0.4f;
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.2f;
      

        camPosition = cameraRig.transform.position;
        camRotation = cameraRig.transform.rotation;
    }

    private void Update()
    {
        //cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation,
        //    camRotation, Time.deltaTime * movementTime);

       
    }


   void LateUpdate()
   {
        if (turnRight)
        {
            camRotation *= Quaternion.Euler(Vector3.up * -rotationamt);
            cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation,
                camRotation, Time.deltaTime * movementTime);

            //cameraRig.transform.RotateAround(board.transform.position, Vector3.down, (movementTime * Time.deltaTime));
        }

        if(turnLeft)
        {
            camRotation *= Quaternion.Euler(Vector3.up * rotationamt);
            cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation,
                camRotation, Time.deltaTime * movementTime);
        }
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

    public void onRightPointerDown()
    {
        turnRight = true;
        
    }

    public void onRightPointerUp()
    {
        turnRight = false;
    }

    public void onLeftPointerDown()
    {
        turnLeft = true;
    }

    public void onLeftPointerUp()
    {
        turnLeft = false;
    }

   public void onSliderChange(float vol)
    {
        FindObjectOfType<AudioManager>().adjustVolume(vol);
    }
}
   


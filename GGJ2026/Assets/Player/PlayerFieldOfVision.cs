using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFieldOfVision : MonoBehaviour
{
    public Camera playerCamera;
    public PlayerTransformation playerTransformation;
    public float farFiledVisionMinValue = 30.0f;
    public float farFiledVisionMaxValue = 1000.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransformation.GetTransfomaitonFlag())
        {
            SetFieldOfVision(farFiledVisionMinValue);
        }
        else
        {
            SetFieldOfVision(farFiledVisionMaxValue);
        }
    }

    void SetFieldOfVision(float value)
    {
        playerCamera.farClipPlane = value;
    }
}

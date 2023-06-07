using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputScript : MonoBehaviour
{
    CarController _carController;
    
    private void Awake()
    {
        _carController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        //Get input from Unity's input system.
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        
        _carController.SetInputVector(inputVector);
        
    }
}

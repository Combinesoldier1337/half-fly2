using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AircraftController))]
public class Aircraft_UserControl : MonoBehaviour
{
    AircraftController ac;
    public float vertical;
    public float horizontal;
    public float roll;
    public float engine;
    public bool isBreaking = false;
    private void Start()
    {
        ac = GetComponent<AircraftController> ();
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
        ac.SetBreaking = isBreaking;
        ac.SetHorizontalInput = horizontal;
        ac.SetVerticalInput = vertical;
        ac.SetRollInput = roll;
        if (engine > 0)
        {
            ac.Accelerate();
        }
        if (engine < 0)
        {
            ac.Decelerate();
        }
    }

    private void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal"); 
        roll = Input.GetAxis("Roll"); 
        isBreaking = Input.GetButton("Brake");
        engine = Input.GetAxis("Engine");
    }
}

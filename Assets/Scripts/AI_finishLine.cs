using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AI_finishLine : MonoBehaviour
{


    public GameObject carHuman;
    public GameObject carAI;
    private CarController controller;

    // Start is called before the first frame update
    public void Start()
    {
        controller = carHuman.GetComponent<CarController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (String.Equals(other.gameObject.name,"Sphere_ai"))
        {
            controller.AI_time = controller.time;
        }
    }

}

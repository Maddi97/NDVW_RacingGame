using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement; 
using Random=System.Random;

public class CheckPoints_human : MonoBehaviour
{
    int check;
    static int nbrChecks = 64;
    GameObject[] checkPoints = new GameObject[nbrChecks];
    public GameObject humanCar;
    public GameObject aiCar;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i< nbrChecks; i++){
            checkPoints[i] = GameObject.Find((i+1).ToString());
        }
        check = 1;
        Debug.Log("Test");
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (String.Equals(other.gameObject.name,check.ToString()))
        {
            check++;
        
        }
        else if (String.Equals(other.gameObject.name, "FinishLine")){
            CarController humanController = humanCar.GetComponent<CarController>();
            TimeSpan tsHuman = TimeSpan.FromSeconds(humanController.time);
            StaticClass.HumanName = humanController.name;
            StaticClass.HumanTime = tsHuman.ToString("m\\:ss\\:ff");

            // Just add a random time for the AI if human finishes first
            CarController_simple aiController = aiCar.GetComponent<CarController_simple>();
            Random r = new Random();
            int range = 8;
            double randomDouble = r.NextDouble() * range;
            TimeSpan tsAi = TimeSpan.FromSeconds(humanController.time + randomDouble);
            StaticClass.AiName = aiController.name;
            StaticClass.AiTime = tsAi.ToString("m\\:ss\\:ff");            

            SceneManager.LoadScene(3);
        }
    }
}

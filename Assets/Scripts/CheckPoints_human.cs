using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement; 

public class CheckPoints_human : MonoBehaviour
{
    int check;
    static int nbrChecks = 64;
    GameObject[] checkPoints = new GameObject[nbrChecks];
    public GameObject Car;

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
            CarController Controller = Car.GetComponent<CarController>();
            StaticClass.HumanName = Controller.name;
            TimeSpan ts = TimeSpan.FromSeconds(Controller.time);
            StaticClass.HumanTime = ts.ToString("m\\:ss\\:ff");
            SceneManager.LoadScene(3);
        }
    }
}

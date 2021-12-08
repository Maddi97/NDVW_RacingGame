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
            Debug.Log("check passed : " + check.ToString());
            check++;
        
        }
        else if (check>nbrChecks && String.Equals(other.gameObject.name, "FinishLine")){
            //Debug.Log(check);
            SceneManager.LoadScene(3);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class FinishLine : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        //if (other.gameObject.name=="Car Human")
        //{
            SceneManager.LoadScene(3);
        //}
    }
}

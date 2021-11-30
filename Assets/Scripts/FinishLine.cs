using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CheckpointManager>() != null)
        {
            other.GetComponent<CheckpointManager>().FinishLineReached(this);
        }
    }
}

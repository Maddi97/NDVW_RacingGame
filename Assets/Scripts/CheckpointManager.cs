using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    //public float MaxTimeToReachNextCheckpoint = 10f;
    public float TimeLeft = 10f;
    public float t = 0;
    
    public CarAgent carAgent;
    public Checkpoint nextCheckPointToReach;
    
    private int CurrentCheckpointIndex;
    private List<Checkpoint> Checkpoints;
    private Checkpoint lastCheckpoint;
    private bool reachedAllCheckpoints = false;

    public event Action<Checkpoint> reachedCheckpoint;

    public int laps = 0;

    void Start()
    {
        Checkpoints = FindObjectOfType<Checkpoints>().checkPoints;
        ResetCheckpoints();
    }

    public void ResetCheckpoints()
    {
        CurrentCheckpointIndex = 0;
        //TimeLeft = MaxTimeToReachNextCheckpoint;
        
        SetNextCheckpoint();
    }

    private void Update()
    {
        t += Time.deltaTime;

        if (t > TimeLeft)
        {
            carAgent.AddReward(-1f);
            carAgent.EndEpisode();
            t = 0f;
            UnityEngine.Debug.Log("Ended episode because of time");

        }
    }

    public void CheckPointReached(Checkpoint checkpoint)
    {
        t -= 0.5f;

        if (nextCheckPointToReach != checkpoint) return;


        lastCheckpoint = Checkpoints[CurrentCheckpointIndex];
        reachedCheckpoint?.Invoke(checkpoint);
        CurrentCheckpointIndex++;

        if (CurrentCheckpointIndex >= Checkpoints.Count)
        {
            reachedAllCheckpoints = true;
            carAgent.AddReward(0.5f);
            UnityEngine.Debug.Log("rewarded and ended episode");
            carAgent.EndEpisode();
        }
        else
        {
            carAgent.AddReward((0.5f) / Checkpoints.Count);
            SetNextCheckpoint();
            UnityEngine.Debug.Log("Setting new checkpoint, rewarded: ");
        }
    }

    public void FinishLineReached(FinishLine finish)
    {
        if (reachedAllCheckpoints == true)
        {
            reachedAllCheckpoints = false;
            this.laps = laps + 1;
        }
        }

    public void WallCollided(Wall wall)
    {
        //Debug.Log("Penalty for hitting Wall");
        float reward = -0.01f;
        carAgent.AddReward(reward);
        carAgent._carController.theRB.velocity = carAgent._carController.theRB.velocity * 0.2f;

    }

    private void SetNextCheckpoint()
    {
        if (Checkpoints.Count > 0)
        {
            //TimeLeft = MaxTimeToReachNextCheckpoint;
            nextCheckPointToReach = Checkpoints[CurrentCheckpointIndex];
            
        }
    }
}

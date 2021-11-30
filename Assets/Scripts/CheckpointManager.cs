using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public float MaxTimeToReachNextCheckpoint = 1000f;
    public float TimeLeft = 10000f;
    
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
        TimeLeft = MaxTimeToReachNextCheckpoint;
        
        SetNextCheckpoint();
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;

        if (TimeLeft < 0f)
        {
            carAgent.AddReward(-1f);
            carAgent.EndEpisode();
        }
    }

    public void CheckPointReached(Checkpoint checkpoint)
    {
        if (nextCheckPointToReach != checkpoint) return;


        lastCheckpoint = Checkpoints[CurrentCheckpointIndex];
        reachedCheckpoint?.Invoke(checkpoint);
        CurrentCheckpointIndex++;

        if (CurrentCheckpointIndex >= Checkpoints.Count)
        {
            reachedAllCheckpoints = true;
            carAgent.AddReward(0.5f);
            carAgent.EndEpisode();
        }
        else
        {
            carAgent.AddReward((0.5f) / Checkpoints.Count);
            SetNextCheckpoint();
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

        private void SetNextCheckpoint()
    {
        if (Checkpoints.Count > 0)
        {
            TimeLeft = MaxTimeToReachNextCheckpoint;
            nextCheckPointToReach = Checkpoints[CurrentCheckpointIndex];
            
        }
    }
}

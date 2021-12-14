using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class CarAgent : Agent
{
    public CheckpointManager _checkpointManager;
    public CarController_simple _carController;
    public float total_speed_reward;
    //called once at the start
    public override void Initialize()
    {
        _carController = GetComponent<CarController_simple>();
        total_speed_reward = 0;
    }

    //Called each time it has timed-out or has reached the goal
    public override void OnEpisodeBegin()
    {
        total_speed_reward = 0;
        _checkpointManager.ResetCheckpoints();
        _carController.Respawn();
    }

    #region Edit this region!

    //Collecting extra Information that isn't picked up by the RaycastSensors
    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 diff = _checkpointManager.nextCheckPointToReach.transform.position - transform.position;
        sensor.AddObservation(diff / 20f);
        float r = _carController.theRB.velocity.magnitude*0.01f;

        total_speed_reward = total_speed_reward + r;
        
        AddReward(r);
        //UnityEngine.Debug.Log("total_speed_reward = : " + total_speed_reward);
        //UnityEngine.Debug.Log("total_speed_penaly = : " + total_speed_penalty);
    }

    //Processing the actions received
    public override void OnActionReceived(ActionBuffers actions)
    {
        var input = actions.ContinuousActions;
        this._carController.steer(input[0]);
        this._carController.accelerate(input[1]);
    }

    //For manual testing with human input, the actionsOut defined here will be sent to OnActionRecieved
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.ContinuousActions;
        action.Clear();

        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
    }

    #endregion
}
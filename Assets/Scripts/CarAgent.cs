using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class CarAgent : Agent
{
    public CheckpointManager _checkpointManager;
    private CarController_simple _carController;

    //called once at the start
    public override void Initialize()
    {
        _carController = GetComponent<CarController_simple>();
    }

    //Called each time it has timed-out or has reached the goal
    public override void OnEpisodeBegin()
    {
        _checkpointManager.ResetCheckpoints();
        _carController.Respawn();
    }

    #region Edit this region!

    //Collecting extra Information that isn't picked up by the RaycastSensors
    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 diff = _checkpointManager.nextCheckPointToReach.transform.position - transform.position;
        sensor.AddObservation(diff / 20f);
        r = _carController.theRB.velocity.magnitude*0.01;
        AddReward(r);
        debug.log("Added reward for magnitude: " + r);
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
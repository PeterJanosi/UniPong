using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PaddleAgent : Agent
{
    [SerializeField] private Transform ballTransform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float hitBallReward = 1f;          // Increased reward for hitting the ball
    [SerializeField] private float preventGoalReward = 2f;      // Increased reward for preventing goals
    [SerializeField] private float penaltyForNoAction = -0.01f; // Penalty for not taking actions

    public enum Event
    {
    RightPaddleGoal,
    LeftPaddleGoal
    }

    public override void OnEpisodeBegin()
    {
        // Reset the agent's position and the ball's position at the start of each episode
        transform.localPosition = new Vector3(transform.localPosition.x, Random.Range(-3.7f, 2.5f), 0);
        ballTransform.localPosition = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add the agent's position and the ball's position as observations
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(ballTransform.localPosition);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Allow manual control of the agent's movement for testing
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Vertical");
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Move the agent based on the received action
        float moveY = actions.ContinuousActions[0];
        float movementMagnitude = Mathf.Abs(moveY);

        transform.localPosition += new Vector3(0, moveY, 0) * Time.deltaTime * moveSpeed;

        
        // Penalize the agent for not taking actions
        AddReward(penaltyForNoAction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Reward the agent for hitting the ball
            AddReward(hitBallReward);
        }
    }
    // I used this script for the left paddle too, only changed the CompareTag to "LeftGoal"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the left goal
        if (collision.gameObject.CompareTag("RightGoal"))
        {
            // Penalize for letting the ball hit the left goal
            AddReward(-preventGoalReward);
        }
    }
}

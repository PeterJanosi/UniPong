using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PongEnvController : MonoBehaviour
{
    [SerializeField] private PaddleAgent rightPaddleAgent;
    [SerializeField] private Ball ball;
    private int leftScore;
    private int rightScore;
    public Text rightScoreText;
    public Text leftScoreText;
    private int resetTimer;

   
    

    public enum Event
    {
        RightPaddleGoal = 0,
        LeftPaddleGoal = 1
    }

     public void ResolveEvent(Event triggerEvent)
    {
        switch (triggerEvent)
        {
            case Event.RightPaddleGoal:
                leftScore++; // Increase left score if the ball hits the right goal
                this.leftScoreText.text= leftScore.ToString();
                break;
            case Event.LeftPaddleGoal:
                rightScore++; // Increase right score if the ball hits the left goal
                this.rightScoreText.text= rightScore.ToString();
                break;
            default:
                break;
        }

        // Reset the scene if the ball scores a goal
        ResetScene();
    }

    

    private void ResetScene()
    {
        resetTimer = 60;
        ball.Launch();
    }

   
}

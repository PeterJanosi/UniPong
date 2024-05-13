using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private PongEnvController envController;

    private Rigidbody2D rb;
   
        
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Launch();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RightGoal"))
        {
            envController.ResolveEvent(PongEnvController.Event.RightPaddleGoal);
        }
        else if (other.gameObject.CompareTag("LeftGoal"))
        {
            envController.ResolveEvent(PongEnvController.Event.LeftPaddleGoal);
        }
    }

    public void Launch()
    {
        transform.localPosition = Vector3.zero;
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }
     public enum Event
    {
        LeftPaddleGoal,
        RightPaddleGoal
    }
}

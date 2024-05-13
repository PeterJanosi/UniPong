using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private string inputAxis = "Vertical";

    private void Update()
    {
        // Get input from the vertical axis
        float moveInput = Input.GetAxisRaw(inputAxis);

        // Move the paddle based on the input
        MovePaddle(moveInput);
    }

    private void MovePaddle(float moveInput)
    {
        // Calculate the movement direction
        Vector3 movement = Vector3.up * moveInput;

        // Move the paddle
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}

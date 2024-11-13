using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 7f;
    private float leftEdge;

    private void Start()
    {
        // Calculate the left edge of the screen to destroy the obstacle when it moves off-screen
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        // Move the obstacle to the left
        transform.position += speed * Time.deltaTime * Vector3.left;

        // Destroy the obstacle if it goes off-screen
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}

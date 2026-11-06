using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;

    private void OnTriggerEnter(Collider other)
    {
        var ball = other.attachedRigidbody.GetComponent<Ball>();
        if (ball)
        {
            _scoreCounter.AddScore();
            Debug.Log($"Your score: {_scoreCounter.Score}");
            Destroy(ball.gameObject);
            
        }
    }
}

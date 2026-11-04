using UnityEngine;

public class Gates : MonoBehaviour
{
    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            score++;
            Debug.Log("Текущий счет: " + score);
            Destroy(other.gameObject);
        }
    }
}
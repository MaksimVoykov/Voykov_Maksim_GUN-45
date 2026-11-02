using UnityEngine;

public class Gates : MonoBehaviour
{
    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            score++;
            Debug.Log("Текущий игровой счет: " + score);
            Destroy(other.gameObject);
        }
    }
}
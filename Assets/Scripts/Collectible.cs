using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int pointsValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        ScoreManager.Instance.AddPoints(pointsValue);

        gameObject.SetActive(false);
    }
}
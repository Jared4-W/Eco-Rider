using UnityEngine;

public class EnvironmentFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos.z = player.position.z;

        transform.position = pos;
    }
}

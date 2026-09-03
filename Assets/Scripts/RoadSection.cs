using UnityEngine;

public class RoadSection : MonoBehaviour
{
    private MoveForward[] vehicles;

    private Collectible[] collectibles;

    void Awake()
    {
        // Se guardan TODOS los vehículos una sola vez (optimizado)
        vehicles = GetComponentsInChildren<MoveForward>(true);

        // Monedas
        collectibles = GetComponentsInChildren<Collectible>(true);
    }

    public void ResetVehicles()
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].ResetVehicle();
        }
    }

    public void ResetCollectibles()
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            collectibles[i].gameObject.SetActive(true);
        }
    }

    public void ResetSection()
    {
        ResetVehicles();
        ResetCollectibles();
    }

}

using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //VARIABLE PARA INDICAR QUE OBJETO USAR DESDE INSPECTOR
    public GameObject player;

    //VARIABLE PRIVADA PARA SEGUIR VEHICULO DE MANERA OPTIMIZADA
    private Vector3 offset = new Vector3(0, 5, -7);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()  //VISTA SUAVE Y OPTIMIZADA
    {
        //INDICAR CAMARA TOME POSICION DEL VEHICULO
        transform.position = player.transform.position + offset;
    }
}

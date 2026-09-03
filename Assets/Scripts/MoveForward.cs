using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Awake()
    {
        // Guardamos la posición LOCAL dentro del prefab
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // Movimiento hacia adelante
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        // Cuando sale del tramo de la sección, se desactiva
        if (transform.localPosition.z > 200f) // Ajusta este valor a tu tamaño real
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetVehicle()
    {
        // Regresa a su posición original dentro del prefab
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;

        // Reactiva el vehículo
        gameObject.SetActive(true);
    }

}



//using UnityEngine;

//public class MoveForward : MonoBehaviour
//{
//    public int speed;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        transform.Translate(Vector3.forward * speed * Time.deltaTime);
//    }
//}

using UnityEngine;

public class playerController1 : MonoBehaviour
{
    //VARIABLES DE MANEJO
    public float speed = 5.0f;
    public float turnSpeed;
    public float horizontalInput = 0.0f;

    //VARIABLES DE CAMARA
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;

    public Transform wheelFLPivot;
    public Transform wheelFRPivot;

    //LAS 4 RUEDAS GIRAN
    public Transform wheelFL;
    public Transform wheelFR;
    public Transform wheelRL;
    public Transform wheelRR;

    //ROTACION VISUAL DE RUEDAS - 
    //Determina cuántos grados giran las ruedas visualmente, 
    //Es una animación visual. No tiene relación con la velocidad real del coche.
    public float wheelRotationSpeed = 500f;

    //Máximo giro permitido de las ruedas delanteras.
    public float maxSteerAngle = 25f;

    //SUAVIZADO DEL VOLANTE - Controla qué tan suave giran.
    //Controla la rapidez con que las ruedas alcanzan el ángulo objetivo.
    public float steeringSmoothness = 5f;

    //Almacena el ángulo actual.
    private float currentSteerAngle = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ASIGNACION DE CONFIGURACIONES TECLADO
        horizontalInput = Input.GetAxis("Horizontal");

        //MOVER EL VEHICULO HACIA ADELANTE Y VELOCIDAD AJUSTADA
        transform.Translate( Vector3.forward * Time.deltaTime * speed);
        //MODIFICAR GIRO DEL VEHICULO
        transform.Rotate( Vector3.up, turnSpeed * horizontalInput * Time.deltaTime );

        //Llama al método que controla la apariencia visual de las ruedas.
        AnimateWheels();

        //CAMBIO ENTRE CAMARAS 
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }

    void AnimateWheels()
    {
        //GIRO VISUAL DE LAS 4 RUEDAS
        float rotationAmount = wheelRotationSpeed * Time.deltaTime;

        wheelFL.Rotate(Vector3.right * rotationAmount);
        wheelFR.Rotate(Vector3.right * rotationAmount);
        wheelRL.Rotate(Vector3.right * rotationAmount);
        wheelRR.Rotate(Vector3.right * rotationAmount);

        //CALCULAR ANGULO OBJETIVO
        float targetSteerAngle = horizontalInput * maxSteerAngle;

        //SUAVIZAR MOVIMIENTO
        currentSteerAngle = Mathf.Lerp(
            currentSteerAngle,
            targetSteerAngle,
            steeringSmoothness * Time.deltaTime
        );

        //APLICAR SOLO A RUEDAS DELANTERAS
        wheelFLPivot.localRotation =
    Quaternion.Euler(
        0,
        currentSteerAngle,
        0
    );

        wheelFRPivot.localRotation =
    Quaternion.Euler(
        0,
        currentSteerAngle,
        0
    );
        /*wheelFL.localRotation =
            Quaternion.Euler(
                wheelFL.localEulerAngles.x,
                currentSteerAngle,
                0
            );

        wheelFR.localRotation =
            Quaternion.Euler(
                wheelFR.localEulerAngles.x,
                currentSteerAngle,
                0
            );*/
    }
}

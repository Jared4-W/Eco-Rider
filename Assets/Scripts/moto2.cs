//using UnityEngine;

//[DisallowMultipleComponent]
//public sealed class moto2 : MonoBehaviour
//{
//    [Header("Movimiento")]
//    [SerializeField, Min(0f)]
//    private float velocidad = 12f;

//    [Header("Dirección")]
//    [SerializeField]
//    private float turnSpeed = 45f;

//    [SerializeField]
//    private float steeringSmoothness = 5f;

//    [Header("Manubrio")]
//    [SerializeField]
//    private Transform manubrio;

//    [SerializeField]
//    private float maxSteerAngle = 25f;

//    [Header("Inclinación")]
//    [SerializeField]
//    private Transform modeloMoto;

//    [SerializeField]
//    private float maxLeanAngle = 12f;

//    [SerializeField]
//    private float leanSmoothness = 6f;

//    [Header("Rueda Delantera")]
//    [SerializeField]
//    private Transform frontWheelPivot;

//    [SerializeField]
//    private Transform frontWheel;

//    [Header("Rueda Trasera")]
//    [SerializeField]
//    private Transform rearWheel;

//    [Header("Animación de Ruedas")]
//    [SerializeField]
//    private float wheelRotationSpeed = 500f;

//    private float horizontalInput;
//    //private float currentSteering;
//    private float currentSteerAngle;
//    private float currentLeanAngle;

//    private Quaternion modeloRotacionInicial;
//    private Quaternion manubrioRotacionInicial;
//    private Quaternion frontWheelPivotRotacionInicial;

//    private void Awake()
//    {
//        if (modeloMoto != null)
//            modeloRotacionInicial = modeloMoto.localRotation;

//        if (manubrio != null)
//            manubrioRotacionInicial = manubrio.localRotation;

//        if (frontWheelPivot != null)
//            frontWheelPivotRotacionInicial =
//                frontWheelPivot.localRotation;
//    }

//    private void Update()
//    {
//        LeerEntrada();

//        MoverMoto();

//        AnimarManubrio();

//        AnimarDireccionRuedaDelantera();

//        InclinarMoto();

//        RotarRuedas();
//    }

//    private void LeerEntrada()
//    {
//        horizontalInput =
//            Input.GetAxisRaw("Horizontal");
//    }

//    private void MoverMoto()
//    {
//        transform.Translate(
//            Vector3.forward *
//            velocidad *
//            Time.deltaTime,
//            Space.Self);

//        transform.Rotate(
//            Vector3.up,
//            horizontalInput *
//            turnSpeed *
//            Time.deltaTime,
//            Space.Self);
//    }

//    private void AnimarManubrio()
//    {
//        if (manubrio == null)
//            return;

//        float targetAngle =
//            horizontalInput *
//            maxSteerAngle;

//        currentSteerAngle = Mathf.Lerp(
//            currentSteerAngle,
//            targetAngle,
//            steeringSmoothness *
//            Time.deltaTime);

//        manubrio.localRotation =
//            manubrioRotacionInicial *
//            Quaternion.Euler(
//                0f,
//                currentSteerAngle,
//                0f);
//    }

//    private void AnimarDireccionRuedaDelantera()
//    {
//        if (frontWheelPivot == null)
//            return;

//        frontWheelPivot.localRotation =
//            frontWheelPivotRotacionInicial *
//            Quaternion.Euler(
//                0f,
//                currentSteerAngle,
//                0f);
//    }

//    private void InclinarMoto()
//    {
//        if (modeloMoto == null)
//            return;

//        float targetLean =
//            -horizontalInput *
//            maxLeanAngle;

//        currentLeanAngle = Mathf.Lerp(
//            currentLeanAngle,
//            targetLean,
//            leanSmoothness *
//            Time.deltaTime);

//        modeloMoto.localRotation =
//            modeloRotacionInicial *
//            Quaternion.Euler(
//                0f,
//                0f,
//                currentLeanAngle);
//    }

//    private void RotarRuedas()
//    {
//        float rotationAmount =
//            wheelRotationSpeed *
//            Time.deltaTime;

//        if (frontWheel != null)
//        {
//            frontWheel.Rotate(
//                Vector3.right *
//                rotationAmount,
//                Space.Self);
//        }

//        if (rearWheel != null)
//        {
//            rearWheel.Rotate(
//                Vector3.right *
//                rotationAmount,
//                Space.Self);
//        }
//    }
//}

using UnityEngine;

[DisallowMultipleComponent]
public sealed class moto2 : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField, Min(0f)]
    private float velocidad = 12f;

    [Header("Dirección")]
    [SerializeField]
    private float turnSpeed = 45f;

    [SerializeField]
    private float steeringSmoothness = 5f;

    [Header("Manubrio")]
    [SerializeField]
    private Transform manubrio;

    [SerializeField]
    private float maxSteerAngle = 25f;

    [Header("Inclinación")]
    [SerializeField]
    private Transform modeloMoto;

    [SerializeField]
    private float maxLeanAngle = 12f;

    [SerializeField]
    private float leanSmoothness = 6f;

    [Header("Rueda Delantera")]
    [SerializeField]
    private Transform frontWheelPivot;

    [SerializeField]
    private Transform frontWheel;

    [Header("Rueda Trasera")]
    [SerializeField]
    private Transform rearWheel;

    [Header("Animación de Ruedas")]
    [SerializeField]
    private float wheelRotationSpeed = 500f;

    private Transform _transform;

    private float horizontalInput;
    private float currentSteerAngle;
    private float currentLeanAngle;

    private Quaternion modeloRotacionInicial;
    private Quaternion manubrioRotacionInicial;
    private Quaternion frontWheelPivotRotacionInicial;

    private void Awake()
    {
        _transform = transform;

        if (modeloMoto != null)
            modeloRotacionInicial = modeloMoto.localRotation;

        if (manubrio != null)
            manubrioRotacionInicial = manubrio.localRotation;

        if (frontWheelPivot != null)
            frontWheelPivotRotacionInicial = frontWheelPivot.localRotation;
    }

    private void FixedUpdate() //mejor para Rigidbody
    {
        float dt = Time.deltaTime;

        LeerEntrada();
        MoverMoto(dt);
        AnimarManubrio(dt);
        AnimarDireccionRuedaDelantera();
        InclinarMoto(dt);
        RotarRuedas(dt);
    }

    private void LeerEntrada()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void MoverMoto(float dt)
    {
        _transform.Translate(Vector3.forward * velocidad * dt, Space.Self);
        _transform.Rotate(Vector3.up, horizontalInput * turnSpeed * dt, Space.Self);
    }

    private void AnimarManubrio(float dt)
    {
        if (manubrio == null) return;

        float targetAngle = horizontalInput * maxSteerAngle;

        currentSteerAngle = Mathf.Lerp(
            currentSteerAngle,
            targetAngle,
            steeringSmoothness * dt);

        manubrio.localRotation =
            manubrioRotacionInicial *
            Quaternion.Euler(0f, currentSteerAngle, 0f);
    }

    private void AnimarDireccionRuedaDelantera()
    {
        if (frontWheelPivot == null) return;

        frontWheelPivot.localRotation =
            frontWheelPivotRotacionInicial *
            Quaternion.Euler(0f, currentSteerAngle, 0f);
    }

    private void InclinarMoto(float dt)
    {
        if (modeloMoto == null) return;

        float targetLean = -horizontalInput * maxLeanAngle;

        currentLeanAngle = Mathf.Lerp(
            currentLeanAngle,
            targetLean,
            leanSmoothness * dt);

        modeloMoto.localRotation =
            modeloRotacionInicial *
            Quaternion.Euler(0f, 0f, currentLeanAngle);
    }

    private void RotarRuedas(float dt)
    {
        float rotationAmount = wheelRotationSpeed * dt;

        if (frontWheel != null)
            frontWheel.Rotate(Vector3.right * rotationAmount, Space.Self);

        if (rearWheel != null)
            rearWheel.Rotate(Vector3.right * rotationAmount, Space.Self);
    }
}
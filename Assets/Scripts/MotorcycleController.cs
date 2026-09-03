//using UnityEngine;

//public class MotorcycleController : MonoBehaviour
//{
//    //==================================================
//    // MOVIMIENTO
//    //==================================================

//    [Header("Movimiento")]

//    [SerializeField]
//    private float speed = 18f;

//    [SerializeField]
//    private float turnSpeed = 70f;

//    private float horizontalInput;

//    //==================================================
//    // CAMARAS
//    //==================================================

//    [Header("Cámaras")]

//    [SerializeField]
//    private Camera mainCamera;

//    [SerializeField]
//    private Camera hoodCamera;

//    [SerializeField]
//    private KeyCode switchKey = KeyCode.C;

//    //==================================================
//    // RUEDAS
//    //==================================================

//    [Header("Ruedas")]

//    [SerializeField]
//    private Transform frontWheelPivot;

//    [SerializeField]
//    private Transform frontWheel;

//    [SerializeField]
//    private Transform rearWheel;

//    [SerializeField]
//    private float wheelRotationSpeed = 900f;

//    [SerializeField]
//    private float maxSteerAngle = 22f;

//    [SerializeField]
//    private float steerSmoothness = 10f;

//    private float currentSteer;

//    //==================================================
//    // INCLINACION
//    //==================================================

//    [Header("Inclinación")]

//    [SerializeField]
//    private Transform bikeBody;

//    [SerializeField]
//    private float maxLeanAngle = 20f;

//    [SerializeField]
//    private float leanSmoothness = 8f;

//    private float currentLean;

//    //==================================================

//    private void Update()
//    {
//        ReadInput();

//        Move();

//        AnimateFrontWheel();

//        AnimateRearWheel();

//        LeanBike();

//        ChangeCamera();
//    }

//    //==================================================

//    private void ReadInput()
//    {
//        horizontalInput = Input.GetAxisRaw("Horizontal");
//    }

//    //==================================================

//    private void Move()
//    {
//        transform.Translate(
//            Vector3.forward * speed * Time.deltaTime,
//            Space.Self
//        );

//        transform.Rotate(
//            0f,
//            horizontalInput * turnSpeed * Time.deltaTime,
//            0f,
//            Space.Self
//        );
//    }

//    //==================================================

//    private void AnimateFrontWheel()
//    {
//        float rotationAmount =
//            speed *
//            wheelRotationSpeed *
//            Time.deltaTime;

//        frontWheel.Rotate(
//            rotationAmount,
//            0f,
//            0f,
//            Space.Self
//        );

//        float targetAngle =
//            horizontalInput *
//            maxSteerAngle;

//        currentSteer =
//            Mathf.Lerp(
//                currentSteer,
//                targetAngle,
//                steerSmoothness * Time.deltaTime
//            );

//        frontWheelPivot.localRotation =
//            Quaternion.Euler(
//                0f,
//                currentSteer,
//                0f
//            );
//    }

//    //==================================================

//    private void AnimateRearWheel()
//    {
//        float rotationAmount =
//            speed *
//            wheelRotationSpeed *
//            Time.deltaTime;

//        rearWheel.Rotate(
//            rotationAmount,
//            0f,
//            0f,
//            Space.Self
//        );
//    }

//    //==================================================

//    private void LeanBike()
//    {
//        if (bikeBody == null)
//            return;

//        float targetLean =
//            -horizontalInput *
//            maxLeanAngle;

//        currentLean =
//            Mathf.Lerp(
//                currentLean,
//                targetLean,
//                leanSmoothness * Time.deltaTime
//            );

//        bikeBody.localRotation =
//            Quaternion.Euler(
//                0f,
//                0f,
//                currentLean
//            );
//    }

//    //==================================================

//    private void ChangeCamera()
//    {
//        if (!Input.GetKeyDown(switchKey))
//            return;

//        bool enableMain =
//            !mainCamera.enabled;

//        mainCamera.enabled = enableMain;

//        hoodCamera.enabled = !enableMain;
//    }
//}

//using UnityEngine;

//public sealed class MotoMovimiento : MonoBehaviour
//{
//    [Header("Movimiento")]
//    [SerializeField, Min(0f)]
//    private float velocidad = 12f;

//    private Transform cachedTransform;

//    private void Awake()
//    {
//        cachedTransform = transform;
//    }

//    private void Update()
//    {
//        Mover();
//    }

//    private void Mover()
//    {
//        cachedTransform.Translate(Vector3.forward * velocidad * Time.deltaTime, Space.Self);
//    }
//}

//using UnityEngine;

//[DisallowMultipleComponent]
//[RequireComponent(typeof(Rigidbody))]
//public sealed class MotoMovimiento : MonoBehaviour
//{
//    [Header("Movimiento")]
//    [SerializeField, Min(0f)]
//    private float velocidad = 12f;

//    private Rigidbody rb;

//    private Vector3 direccionMovimiento;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody>();

//        rb.interpolation = RigidbodyInterpolation.Interpolate;
//        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
//        rb.constraints = RigidbodyConstraints.FreezeRotationX |
//                         RigidbodyConstraints.FreezeRotationZ;
//    }

//    private void Start()
//    {
//        direccionMovimiento = Vector3.forward;
//    }

//    private void FixedUpdate()
//    {
//        Mover();
//    }

//    private void Mover()
//    {
//        Vector3 nuevaPosicion =
//            rb.position +
//            transform.TransformDirection(direccionMovimiento) *
//            velocidad *
//            Time.fixedDeltaTime;

//        rb.MovePosition(nuevaPosicion);
//    }
//}

//using UnityEngine;

//[DisallowMultipleComponent]
//[RequireComponent(typeof(Rigidbody))]
//public sealed class MotoMovimiento : MonoBehaviour
//{
//    [Header("Movimiento")]
//    [SerializeField, Min(0f)]
//    private float velocidad = 12f;

//    [Header("Dirección")]
//    [SerializeField]
//    private float turnSpeed = 45f;

//    [SerializeField]
//    private float steeringSmoothness = 5f;

//    private Rigidbody rb;

//    private float horizontalInput;
//    private float currentSteering;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody>();

//        rb.interpolation = RigidbodyInterpolation.Interpolate;
//        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

//        rb.constraints =
//            RigidbodyConstraints.FreezeRotationX |
//            RigidbodyConstraints.FreezeRotationZ;
//    }

//    private void Update()
//    {
//        horizontalInput = Input.GetAxisRaw("Horizontal");

//        currentSteering = Mathf.Lerp(
//            currentSteering,
//            horizontalInput,
//            steeringSmoothness * Time.deltaTime);
//    }

//    private void FixedUpdate()
//    {
//        Mover();
//        Girar();
//    }

//    private void Mover()
//    {
//        rb.MovePosition(
//            rb.position +
//            transform.forward *
//            velocidad *
//            Time.fixedDeltaTime);
//    }

//    private void Girar()
//    {
//        if (Mathf.Abs(currentSteering) < 0.001f)
//            return;

//        Quaternion deltaRotation =
//            Quaternion.Euler(
//                0f,
//                currentSteering * turnSpeed * Time.fixedDeltaTime,
//                0f);

//        rb.MoveRotation(rb.rotation * deltaRotation);
//    }
//}

//using UnityEngine;

//[DisallowMultipleComponent]
//[RequireComponent(typeof(Rigidbody))]
//public sealed class MotoMovimiento : MonoBehaviour
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

//    private Rigidbody rb;
//    private Transform cachedTransform;

//    private float horizontalInput;
//    private float currentSteering;
//    private float currentSteerAngle;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody>();
//        cachedTransform = transform;

//        rb.interpolation = RigidbodyInterpolation.Interpolate;
//        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

//        rb.constraints =
//            RigidbodyConstraints.FreezeRotationX |
//            RigidbodyConstraints.FreezeRotationZ;
//    }

//    private void Update()
//    {
//        LeerEntrada();
//        AnimarManubrio();
//    }

//    private void FixedUpdate()
//    {
//        Mover();
//        Girar();
//    }

//    private void LeerEntrada()
//    {
//        horizontalInput = Input.GetAxisRaw("Horizontal");

//        currentSteering = Mathf.Lerp(
//            currentSteering,
//            horizontalInput,
//            steeringSmoothness * Time.deltaTime);
//    }

//    private void Mover()
//    {
//        rb.MovePosition(
//            rb.position +
//            cachedTransform.forward *
//            velocidad *
//            Time.fixedDeltaTime);
//    }

//    private void Girar()
//    {
//        if (Mathf.Abs(currentSteering) < 0.001f)
//            return;

//        Quaternion deltaRotation =
//            Quaternion.Euler(
//                0f,
//                currentSteering * turnSpeed * Time.fixedDeltaTime,
//                0f);

//        rb.MoveRotation(rb.rotation * deltaRotation);
//    }

//    private void AnimarManubrio()
//    {
//        if (manubrio == null)
//            return;

//        float targetAngle = horizontalInput * maxSteerAngle;

//        currentSteerAngle = Mathf.Lerp(
//            currentSteerAngle,
//            targetAngle,
//            steeringSmoothness * Time.deltaTime);

//        manubrio.localRotation = Quaternion.Euler(
//            0f,
//            currentSteerAngle,
//            0f);
//    }
//}

//using UnityEngine;

//[DisallowMultipleComponent]
//[RequireComponent(typeof(Rigidbody))]
//public sealed class MotoMovimiento : MonoBehaviour
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

//    private Rigidbody rb;
//    private Transform cachedTransform;

//    private float horizontalInput;
//    private float currentSteering;
//    private float currentSteerAngle;
//    private float currentLeanAngle;

//    private Quaternion modeloRotacionInicial;

//    private Quaternion manubrioRotacionInicial;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody>();
//        cachedTransform = transform;

//        rb.interpolation = RigidbodyInterpolation.Interpolate;
//        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

//        rb.constraints =
//            RigidbodyConstraints.FreezeRotationX |
//            RigidbodyConstraints.FreezeRotationZ;

//        if (modeloMoto != null)
//            modeloRotacionInicial = modeloMoto.localRotation;

//        if (manubrio != null)
//        {
//            manubrioRotacionInicial =
//                manubrio.localRotation;
//        }
//    }

//    private void Update()
//    {
//        LeerEntrada();
//        AnimarManubrio();
//        InclinarMoto();
//    }

//    private void FixedUpdate()
//    {
//        Mover();
//        Girar();
//    }

//    private void LeerEntrada()
//    {
//        horizontalInput = Input.GetAxisRaw("Horizontal");

//        currentSteering = Mathf.Lerp(
//            currentSteering,
//            horizontalInput,
//            steeringSmoothness * Time.deltaTime);
//    }

//    private void Mover()
//    {
//        rb.MovePosition(
//            rb.position +
//            cachedTransform.forward *
//            velocidad *
//            Time.fixedDeltaTime);
//    }

//    private void Girar()
//    {
//        if (Mathf.Abs(currentSteering) < 0.001f)
//            return;

//        Quaternion deltaRotation =
//            Quaternion.Euler(
//                0f,
//                currentSteering * turnSpeed * Time.fixedDeltaTime,
//                0f);

//        rb.MoveRotation(rb.rotation * deltaRotation);
//    }

//    private void AnimarManubrio()
//    {
//        if (manubrio == null)
//            return;

//        float targetAngle = horizontalInput * maxSteerAngle;

//        currentSteerAngle = Mathf.Lerp(
//            currentSteerAngle,
//            targetAngle,
//            steeringSmoothness * Time.deltaTime);

//        manubrio.localRotation =
//            manubrioRotacionInicial *
//            Quaternion.Euler(
//                0f,
//                currentSteerAngle,
//                0f);
//    }

//    private void InclinarMoto()
//    {
//        if (modeloMoto == null)
//            return;

//        float targetLean = -horizontalInput * maxLeanAngle;

//        currentLeanAngle = Mathf.Lerp(
//            currentLeanAngle,
//            targetLean,
//            leanSmoothness * Time.deltaTime);

//        modeloMoto.localRotation =
//            modeloRotacionInicial *
//            Quaternion.Euler(
//                0f,
//                0f,
//                currentLeanAngle);
//    }
//}

//using UnityEngine;

//[DisallowMultipleComponent]
//[RequireComponent(typeof(Rigidbody))]
//public sealed class MotoMovimiento : MonoBehaviour
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

//    [Header("Animación Ruedas")]
//    [SerializeField]
//    private float wheelRotationSpeed = 500f;

//    private Rigidbody rb;
//    private Transform cachedTransform;

//    private float horizontalInput;
//    private float currentSteering;
//    private float currentSteerAngle;
//    private float currentLeanAngle;

//    private Quaternion modeloRotacionInicial;
//    private Quaternion manubrioRotacionInicial;
//    private Quaternion frontWheelPivotRotacionInicial;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody>();
//        cachedTransform = transform;

//        rb.interpolation = RigidbodyInterpolation.Interpolate;
//        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

//        rb.constraints =
//            RigidbodyConstraints.FreezeRotationX |
//            RigidbodyConstraints.FreezeRotationZ;

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

//        AnimarManubrio();

//        AnimarDireccionRuedaDelantera();

//        InclinarMoto();

//        RotarRuedas();
//    }

//    private void FixedUpdate()
//    {
//        Mover();

//        Girar();
//    }

//    private void LeerEntrada()
//    {
//        horizontalInput = Input.GetAxisRaw("Horizontal");

//        currentSteering = Mathf.Lerp(
//            currentSteering,
//            horizontalInput,
//            steeringSmoothness * Time.deltaTime);
//    }

//    private void Mover()
//    {
//        rb.MovePosition(
//            rb.position +
//            cachedTransform.forward *
//            velocidad *
//            Time.fixedDeltaTime);
//    }

//    private void Girar()
//    {
//        if (Mathf.Abs(currentSteering) < 0.001f)
//            return;

//        Quaternion deltaRotation =
//            Quaternion.Euler(
//                0f,
//                currentSteering * turnSpeed * Time.fixedDeltaTime,
//                0f);

//        rb.MoveRotation(
//            rb.rotation * deltaRotation);
//    }

//    private void AnimarManubrio()
//    {
//        if (manubrio == null)
//            return;

//        float targetAngle =
//            horizontalInput * maxSteerAngle;

//        currentSteerAngle = Mathf.Lerp(
//            currentSteerAngle,
//            targetAngle,
//            steeringSmoothness * Time.deltaTime);

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
//            -horizontalInput * maxLeanAngle;

//        currentLeanAngle = Mathf.Lerp(
//            currentLeanAngle,
//            targetLean,
//            leanSmoothness * Time.deltaTime);

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
//            wheelRotationSpeed * Time.deltaTime;

//        if (frontWheel != null)
//        {
//            frontWheel.Rotate(
//                Vector3.right * rotationAmount);
//        }

//        if (rearWheel != null)
//        {
//            rearWheel.Rotate(
//                Vector3.right * rotationAmount);
//        }
//    }
//}

//using UnityEngine;

//[DisallowMultipleComponent]
//public sealed class MotoMovimiento : MonoBehaviour
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
public sealed class MotoMovimiento : MonoBehaviour
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

    private float horizontalInput;
    private float currentSteerAngle;

    private Quaternion manubrioRotacionInicial;
    private Quaternion frontWheelPivotRotacionInicial;

    private void Awake()
    {
        if (manubrio != null)
            manubrioRotacionInicial = manubrio.localRotation;

        if (frontWheelPivot != null)
            frontWheelPivotRotacionInicial =
                frontWheelPivot.localRotation;
    }

    private void Update()
    {
        LeerEntrada();

        MoverMoto();

        AnimarManubrio();

        AnimarDireccionRuedaDelantera();

        RotarRuedas();
    }

    private void LeerEntrada()
    {
        horizontalInput =
            Input.GetAxisRaw("Horizontal");
    }

    private void MoverMoto()
    {
        transform.Translate(
            Vector3.forward *
            velocidad *
            Time.deltaTime,
            Space.Self);

        transform.Rotate(
            Vector3.up,
            horizontalInput *
            turnSpeed *
            Time.deltaTime,
            Space.Self);
    }

    private void AnimarManubrio()
    {
        if (manubrio == null)
            return;

        float targetAngle =
            horizontalInput *
            maxSteerAngle;

        currentSteerAngle = Mathf.Lerp(
            currentSteerAngle,
            targetAngle,
            steeringSmoothness *
            Time.deltaTime);

        manubrio.localRotation =
            manubrioRotacionInicial *
            Quaternion.Euler(
                0f,
                currentSteerAngle,
                0f);
    }

    private void AnimarDireccionRuedaDelantera()
    {
        if (frontWheelPivot == null)
            return;

        frontWheelPivot.localRotation =
            frontWheelPivotRotacionInicial *
            Quaternion.Euler(
                0f,
                currentSteerAngle,
                0f);
    }

    private void RotarRuedas()
    {
        float rotationAmount =
            wheelRotationSpeed *
            Time.deltaTime;

        if (frontWheel != null)
        {
            frontWheel.Rotate(
                Vector3.right *
                rotationAmount,
                Space.Self);
        }

        if (rearWheel != null)
        {
            rearWheel.Rotate(
                Vector3.right *
                rotationAmount,
                Space.Self);
        }
    }
}
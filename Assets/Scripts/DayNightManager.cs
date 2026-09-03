using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [Header("Sun")]
    [SerializeField]
    private Light sun;

    [Header("Time")]

    [SerializeField]
    private float currentTime = 12f;

    [SerializeField]
    private float dayDuration = 300f;

    [Header("Sun Intensity")]
    [SerializeField]
    private AnimationCurve sunIntensityCurve;

    [Header("Stars")]
    [SerializeField]
    private GameObject starDome;

    [SerializeField]
    private Renderer starRenderer;

    [SerializeField]
    private float starRotationSpeed = 1f;

    private Material starMaterial;

    [SerializeField]
    private AnimationCurve starsVisibilityCurve;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        starMaterial = starRenderer.material;
    }

    // Update is called once per frame
    private void Update()
    {
        currentTime += Time.deltaTime * (24f / dayDuration);

        if (currentTime >= 24f)
        {
            currentTime = 0f;
        }
        UpdateSunRotation();
        UpdateSunIntensity();
        UpdateStars();
    }

    private void UpdateSunRotation()
    {
        float sunAngle = currentTime * 15f;

        sun.transform.rotation =
            Quaternion.Euler(
                sunAngle - 90f,
                170f,
                0f);
    }

    private float GetNormalizedTime()
    {
        return currentTime / 24f;
    }

    private void UpdateSunIntensity()
    {
        float normalizedTime =
        GetNormalizedTime();

        float intensity =
            sunIntensityCurve.Evaluate(normalizedTime);

        sun.intensity = intensity;
    }

    private void UpdateStars()
    {
        starDome.transform.Rotate(
        Vector3.up,
        starRotationSpeed * Time.deltaTime,
        Space.Self);

        float alpha =
            starsVisibilityCurve.Evaluate(
                GetNormalizedTime());

        Color color =
            starMaterial.color;

        color.a = alpha;

        starMaterial.color = color;
    }
}

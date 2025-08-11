using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon; // Vector3

    [Header("SUN")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("MOON")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntencsityMultiplier;
    public AnimationCurve reflectionintensityMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntencsityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionintensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightingSource, Gradient gradient, AnimationCurve intensityCurve) 
    { 
    
        float intensity = intensityCurve.Evaluate(time);
        lightingSource.transform.eulerAngles = (time - (lightingSource == sun ? 0.25f : 0.75f)) * noon * 4f;
        lightingSource.color = gradient.Evaluate(time);
        lightingSource.intensity = intensity;

        GameObject go = lightingSource.gameObject;
        if (lightingSource.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        }

        else if (lightingSource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}

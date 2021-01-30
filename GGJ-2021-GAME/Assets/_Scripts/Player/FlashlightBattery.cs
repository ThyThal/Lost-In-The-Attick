using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashlightBattery : MonoBehaviour
{
    [SerializeField] private float maxBattery = 0f;
    [SerializeField] private float decreaseBatteryModifier = 1f;
    [SerializeField] private float decreaseLightIntensityModifier = 1f;

    private float currentBattery = 0f;
    private bool flashlightState = true;

    private Light2D light2D = null;

    private void Awake()
    {
        light2D = GetComponentInChildren<Light2D>();
    }

    private void Start()
    {
        InitializeBattery();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused())
        {
            if (currentBattery > (maxBattery / 4))
            {
                currentBattery -= Time.deltaTime * decreaseBatteryModifier;
            }
            else if (currentBattery < (maxBattery / 4) && currentBattery > 0)
            {
                currentBattery -= Time.deltaTime * decreaseBatteryModifier;

                if (light2D.intensity > 0)
                {
                    light2D.intensity -= Time.deltaTime * decreaseLightIntensityModifier;
                    //print(currentBattery);
                }
            }
            else if (currentBattery < 0)
            {
                currentBattery = 0;
            }
            else if(currentBattery == 0 && flashlightState)
            {
                light2D.intensity = 0f;
                flashlightState = false;
            }
        }
    }

    private void InitializeBattery()
    {
        currentBattery = maxBattery;
        flashlightState = true;
    }
}

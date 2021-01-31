using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class FlashlightBattery : MonoBehaviour
{
    [SerializeField] private float maxBattery = 0f;
    [SerializeField] private float decreaseBatteryModifier = 1f;
    [SerializeField] private float decreaseLightIntensityModifier = 1f;
    [SerializeField] private float batteryPercentaje;
    [SerializeField] private float minBattery;
    [SerializeField] private Image batteryImage;

    public float intensityModifier;

    public bool testMode = true;

    public float MaxBattery
    {
        get
        {
            return maxBattery;
        }
    }

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
        if (!testMode)
        {
            if (!GameManager.Instance.IsPaused())
            {
                if (currentBattery > 0)
                {
                    ModifyBattery(-Time.deltaTime * decreaseBatteryModifier);
                }




                batteryImage.fillAmount = batteryPercentaje;
            }
        }
    }

    public float CurrentBatteryAmount()
    {
        return currentBattery;
    }


    public void ModifyBattery(float batteryModify)
    {
        currentBattery += batteryModify;
        CurrentBattery();
        light2D.intensity = batteryPercentaje * intensityModifier;
        if (currentBattery > maxBattery) currentBattery = maxBattery;
        else if (currentBattery < 0) currentBattery = 0;
    }


    private void InitializeBattery()
    {
        currentBattery = maxBattery;
    }
    private void CurrentBattery()
    {
        float maximumOffset = maxBattery - minBattery;
        batteryPercentaje = currentBattery / maximumOffset;
        if (batteryPercentaje < 0) batteryPercentaje = 0;
    }
}

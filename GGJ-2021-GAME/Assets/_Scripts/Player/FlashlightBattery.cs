using UnityEngine;

public class FlashlightBattery : MonoBehaviour
{
    [SerializeField] private float maxBattery = 0f;
    [SerializeField] private float decreaseBatteryModifier = 1f;

    private float currentBattery = 0f;

    private void Start()
    {
        InitializeBattery();
    }

    private void Update()
    {
        if (currentBattery > 0)
        {
            currentBattery += Time.deltaTime * decreaseBatteryModifier;
        }
    }

    private void InitializeBattery()
    {
        currentBattery = maxBattery;
    }
}

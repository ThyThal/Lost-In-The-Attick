using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashlightSkill : MonoBehaviour
{
    [SerializeField] private float skillDuration = 0f;
    [SerializeField] private float skillRadiusMultiplier = 1f;
    private FlashlightBattery flashBatt;

    private Light2D light2D = null;
    private float originalRadius = 0f;
    private float timer = 0f;
    private bool canCount = false;
    private bool alreadyFired;

    [SerializeField] private GameObject flashlightSound = null;

    private void Start()
    {
        flashlightSound.gameObject.SetActive(false);
        flashBatt = GetComponent<FlashlightBattery>();
    }

    private void Awake()
    {
        light2D = GetComponentInChildren<Light2D>();
        originalRadius = light2D.pointLightOuterRadius;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPaused())
        {
            if (Input.GetButtonDown("Fire1") && !alreadyFired)
            {
                alreadyFired = true;
                flashlightSound.gameObject.SetActive(true);
                
                light2D.pointLightOuterRadius = originalRadius * skillRadiusMultiplier;
                timer = Time.time + skillDuration;
                canCount = true;
                flashBatt.ModifyBattery(-1);
            }

            if (canCount && timer < Time.time)
            {
                flashlightSound.gameObject.SetActive(false);
                canCount = false;
                alreadyFired = false;
                light2D.pointLightOuterRadius = originalRadius;
            }
        }
    }
}

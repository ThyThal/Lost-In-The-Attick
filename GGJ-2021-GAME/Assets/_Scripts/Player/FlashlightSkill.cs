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
    [SerializeField] private float fireIntensity;


    [SerializeField] private GameObject flashlightSound = null;

    private PolygonCollider2D polygonCollider = null;

    private void Start()
    {
        flashlightSound.gameObject.SetActive(false);
        flashBatt = GetComponent<FlashlightBattery>();
    }

    private void Awake()
    {
        light2D = GetComponentInChildren<Light2D>();
        originalRadius = light2D.pointLightOuterRadius;

        polygonCollider = GetComponent<PolygonCollider2D>();
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
                flashBatt.intensityModifier = fireIntensity;
                timer = Time.time + skillDuration;
                canCount = true;
                flashBatt.ModifyBattery(-1);
                polygonCollider.enabled = true;
            }

            if (canCount && timer < Time.time)
            {
                flashlightSound.gameObject.SetActive(false);
                canCount = false;
                alreadyFired = false;
                flashBatt.intensityModifier = 1;
                light2D.pointLightOuterRadius = originalRadius;
                polygonCollider.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            enemyController.FleeTrigger();
        }
    }
}

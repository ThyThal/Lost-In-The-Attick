using UnityEngine;


public class FlashlightSkill : MonoBehaviour
{
    [SerializeField] private int attackCost = 1;
    [SerializeField] private float skillDuration = 0f;
    [SerializeField] private float skillRadiusMultiplier = 1f;
    [SerializeField] private float _lerpSpeed = 3f;
    private FlashlightBattery flashlightBattery;

    private UnityEngine.Rendering.Universal.Light2D light2D = null;
    private float originalOuterRadius = 0f;
    private float attackCooldown = 0f;
    private bool hasFired = false;
    [SerializeField] private float attackIntensity = 2.5f;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AudioSource flashlightSound;

    private PolygonCollider2D attackCollider = null;

    private void Start()
    {     
        flashlightBattery = GetComponent<FlashlightBattery>();
    }

    private void Awake()
    {
        light2D = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        originalOuterRadius = light2D.pointLightOuterRadius;

        attackCollider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPaused())
        {
            if (Input.GetButtonDown("Fire1") && !hasFired && flashlightBattery.CurrentBatteryAmount()>0)
            {
                Attack();   
            }

            if (hasFired && attackCooldown < Time.time)
            {
                ResetAttack();
            }
            
            if (!hasFired)
            {
                UpdateLerp();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && hasFired)
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            enemyController.FleeTrigger();
        }
    }

    private void Attack()
    {
        flashlightSound.Play(); // Play Attack Sound.
        playerMovement.canMove = false; // Stop player movement.
        hasFired = true;

        light2D.pointLightOuterRadius = originalOuterRadius * skillRadiusMultiplier; // Sets the new radius.
        flashlightBattery.intensityModifier = attackIntensity; // Sets new light intensity.
        flashlightBattery.ModifyBattery(-attackCost); // Reduces battery energy.

        attackCooldown = Time.time + skillDuration; // Creates cooldown timer.
        attackCollider.enabled = true; // Activate collider for scaring enemies.
    }

    private void ResetAttack()
    {
        playerMovement.canMove = true; // Resume player movement.
        hasFired = false; // Reset fired trigger.
        attackCollider.enabled = false; // Disables attack collider.

        //flashlightBattery.intensityModifier = 1; // Resets light intensity to 1.
        //light2D.pointLightOuterRadius = originalOuterRadius; // Resets outer radius.
        //Mathf.Lerp(light2D.pointLightOuterRadius, originalRadius, Time.deltaTime * _lerpSpeed)
    }

    private void UpdateLerp()
    {
        flashlightBattery.intensityModifier = Mathf.Lerp(flashlightBattery.intensityModifier, 1, Time.deltaTime * _lerpSpeed); // Resets light intensity to 1.
        light2D.pointLightOuterRadius = Mathf.Lerp(light2D.pointLightOuterRadius, originalOuterRadius, Time.deltaTime * _lerpSpeed); // Resets outer radius.
    }
}

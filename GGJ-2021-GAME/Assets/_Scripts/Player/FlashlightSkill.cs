﻿using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashlightSkill : MonoBehaviour
{
    [SerializeField] private float skillDuration = 0f;
    [SerializeField] private float skillRadiusMultiplier = 1f;

    private Light2D light2D = null;
    private float originalRadius = 0f;
    private float timer = 0f;
    private bool canCount = false;

    private void Awake()
    {
        light2D = GetComponentInChildren<Light2D>();
        originalRadius = light2D.pointLightOuterRadius;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPaused())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                light2D.pointLightOuterRadius = originalRadius * skillRadiusMultiplier;
                timer = Time.time + skillDuration;
                canCount = true;
            }

            if (canCount && timer < Time.time)
            {
                canCount = false;
                light2D.pointLightOuterRadius = originalRadius;
            }
        }
    }
}
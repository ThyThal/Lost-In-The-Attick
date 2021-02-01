using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBattery : MonoBehaviour
{
    [SerializeField] private AudioClip batteryPickupSFX;
    [SerializeField] private float volume;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FlashlightBattery batteryScript = collision.GetComponentInChildren<FlashlightBattery>();

        if (batteryScript != null)
        {
            batteryScript.ModifyBattery(batteryScript.MaxBattery);
            GameManager.Instance.PlaySFX(batteryPickupSFX, volume);
            Destroy(gameObject);
        }
    }










}

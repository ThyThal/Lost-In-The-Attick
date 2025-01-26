using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TriggerShine : MonoBehaviour
{
    [SerializeField] private Light2D lightShine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            lightShine.enabled = true;
        }
    }
}

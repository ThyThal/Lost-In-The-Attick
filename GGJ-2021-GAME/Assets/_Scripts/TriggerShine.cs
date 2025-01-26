using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerShine : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D lightShine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            lightShine.enabled = true;
        }
    }
}

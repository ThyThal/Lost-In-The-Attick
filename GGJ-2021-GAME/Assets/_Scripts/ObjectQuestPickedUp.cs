using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectQuestPickedUp : MonoBehaviour
{
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private float volume;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PlaySFX(pickUpSound, volume);
            this.gameObject.SetActive(false);
        }
    }
}

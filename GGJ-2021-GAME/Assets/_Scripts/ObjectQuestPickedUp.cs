using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectQuestPickedUp : MonoBehaviour
{
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private float volume;
    [SerializeField] private int itemId;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>(); 
        if (player != null)
        {
            GameManager.Instance.PlaySFX(pickUpSound, volume); 
            player.PickedObject();
            this.gameObject.SetActive(false);
        }
    }
}

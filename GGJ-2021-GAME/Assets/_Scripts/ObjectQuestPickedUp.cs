using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectQuestPickedUp : MonoBehaviour
{
    [SerializeField] 
    private GameObject pickUpSound = null;

    private void Start()
    {
        pickUpSound.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUpSound.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}

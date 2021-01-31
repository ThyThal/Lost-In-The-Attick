using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPit : MonoBehaviour
{
    [SerializeField] private GameObject dialogueFather;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
           
            if (player.questObjectPickedUp)
            {
                GameManager.Instance.YouWin();
                return;
            }
            dialogueFather.SetActive(true);
            dialogueFather.GetComponent<FlavorFullOldMan>().TriggerDialogue();
           
        }
    }
}

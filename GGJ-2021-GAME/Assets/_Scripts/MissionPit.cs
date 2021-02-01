using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPit : MonoBehaviour
{
    [SerializeField] private GameObject dialogueFather;
    private float missionTimer = 1.5f;
    private bool hasMission = false;

    private void Start()
    {
        GiveMission();
    }

    private void Update()
    {
        if (missionTimer > 0) { missionTimer -= Time.deltaTime; }
        if (missionTimer <= 0 && !hasMission)
        {
            GiveMission();
        }
    }

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
        }
    }

    void GiveMission()
    {
        hasMission = true;
        dialogueFather.SetActive(true);
        dialogueFather.GetComponent<FlavorFullOldMan>().TriggerDialogue();
    }
}

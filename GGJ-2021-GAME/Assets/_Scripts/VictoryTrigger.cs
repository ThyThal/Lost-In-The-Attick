using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VictoryTrigger : MonoBehaviour
{
    public UnityEvent OnVictory;

    private void Start()
    {
        GameManager.Instance.victoryTrigger = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.GetQuestObject() != null)
        {
            OnVictory.Invoke();
        }
    }
}

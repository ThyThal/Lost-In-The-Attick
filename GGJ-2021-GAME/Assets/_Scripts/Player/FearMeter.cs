using UnityEngine;
using UnityEngine.Events;

public class FearMeter : MonoBehaviour
{
    [SerializeField] private float maxFear = 0f;
    [SerializeField] private float fearDecreaseModifier = 0;
    [SerializeField] private float fearIncreaseModifier = 0;

    private float fearAmount = 0f;
    private int nearEnemys = 0;

    public UnityEvent OnFrightened;

    private void Start()
    {
        fearAmount = 0f;
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused())
        {
            if (nearEnemys > 0)
            {
                fearAmount += nearEnemys * fearIncreaseModifier * Time.deltaTime;
            }
            else if (nearEnemys == 0 && fearAmount > 0)
            {
                fearAmount -= fearDecreaseModifier * Time.deltaTime;
            }
            else
            {
                fearAmount = 0f;
            }

            if (fearAmount >= maxFear)
            {
                OnFrightened.Invoke();
            }
        }
    }

    public void AddNearEnemy()
    {
        nearEnemys++;
    }

    public void RemoveNearEnemy()
    {
        nearEnemys--;
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class FearMeter : MonoBehaviour
{
    [SerializeField] private float maxFear = 10f;
    [SerializeField] private float fearDecreaseModifier = 0;
    [SerializeField] private float fearIncreaseModifier = 0;
    private float _minimun;
    [SerializeField] private Image _fearMask;
    [SerializeField] private Color _color;

    [SerializeField] private float fearAmount = 0f;
    [SerializeField] private int nearEnemys = 0;
    [SerializeField] private float darknessFear = 1.5f;


    [SerializeField] private AudioClip fearClip;
    [SerializeField] private FlashlightBattery flashlightBattery;
    private AudioSource audioSrc;

    private void Start()
    {
        fearAmount = 0f;

        audioSrc = GetComponent<AudioSource>();

    }


    private void Update()
    {

        //TODO: Incrementar el multiplicador de miedo por tiempo

        if (!GameManager.Instance.IsPaused())
        {
            if (nearEnemys > 0)
            {
                fearAmount += nearEnemys * fearIncreaseModifier * Time.deltaTime;
            }

            if (flashlightBattery.currentBattery <= 0)
            {
                fearAmount += darknessFear * fearIncreaseModifier * Time.deltaTime;
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

                audioSrc.PlayOneShot(fearClip);

                GameManager.Instance.GameOver();
            }

            GetCurrentFill();
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
    private void GetCurrentFill()
    {
        float currentOffset = fearAmount - _minimun;
        float maximumOffset = maxFear - _minimun;
        float fillAmount = currentOffset / maximumOffset;
        _fearMask.fillAmount = fillAmount;
        _fearMask.color = _color;
    }
}

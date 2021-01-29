using UnityEngine;

public class Spooker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var fearMeter = collision.GetComponent<FearMeter>();

            fearMeter.AddNearEnemy();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var fearMeter = collision.GetComponent<FearMeter>();

            fearMeter.RemoveNearEnemy();
        }
    }
}

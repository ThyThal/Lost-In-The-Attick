using UnityEngine;

public class Spooker : MonoBehaviour
{

    FearMeter fearMeter;

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        fearMeter = collision.GetComponent<FearMeter>();

        if (fearMeter != null)
        {
            fearMeter.AddNearEnemy();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        fearMeter = collision.GetComponent<FearMeter>();

        if (fearMeter != null) fearMeter.RemoveNearEnemy();
    }
}

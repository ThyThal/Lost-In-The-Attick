using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocity = 0f;
    private Rigidbody2D rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var movX = Input.GetAxis("Horizontal");
        var movY = Input.GetAxis("Vertical");

        var movNormaliced = new Vector2(movX, movY).normalized * velocity;

        rb.velocity = movNormaliced;
    }
}

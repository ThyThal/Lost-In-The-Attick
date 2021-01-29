using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocity = 10f;
    private Rigidbody2D _rigidbody = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {        
        var movX = Input.GetAxis("Horizontal");
        var movY = Input.GetAxis("Vertical");

        var moveDirection = new Vector2(movX, movY).normalized;
        Move(moveDirection);
    }

    private void Move(Vector2 direction)
    {
        var moveVelocity = direction * velocity;
        _rigidbody.velocity = moveVelocity;
    }
}

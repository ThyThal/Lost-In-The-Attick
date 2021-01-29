using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocity = 10f;
    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        var movX = Input.GetAxis("Horizontal");
        var movY = Input.GetAxis("Vertical");

        var moveDirection = new Vector2(movX, movY).normalized;
        Move(moveDirection);
        _animator.SetFloat("MovementX", movX);
        _animator.SetFloat("MovementY", movY);
    }

    private void Move(Vector2 direction)
    {
        var moveVelocity = direction * velocity;
        _rigidbody.velocity = moveVelocity;
    }
}

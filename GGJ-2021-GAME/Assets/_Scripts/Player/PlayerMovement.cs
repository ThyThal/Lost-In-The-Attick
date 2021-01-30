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
        if (!GameManager.Instance.IsPaused())
        {
            // Movement
            _rigidbody.velocity = Vector2.zero;
            var movX = Input.GetAxisRaw("Horizontal");
            var movY = Input.GetAxisRaw("Vertical");

            var moveDirection = new Vector2(movX, movY).normalized;
            Move(moveDirection);


            // Animator
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var positionDifference = Vector3.zero;

            positionDifference.x = mousePosition.x - transform.position.x;
            positionDifference.y = mousePosition.y - transform.position.y;

            positionDifference.Normalize();

            _animator.SetFloat("LookMouseX", positionDifference.x);
            _animator.SetFloat("LookMouseY", positionDifference.y);

            if (moveDirection.SqrMagnitude() != 0) _animator.SetBool("IsMoving", true);
            else _animator.SetBool("IsMoving", false);
        }
    }

    private void Move(Vector2 direction)
    {
        var moveVelocity = direction * velocity;
        if (direction != Vector2.zero) { _rigidbody.velocity = moveVelocity; }
        else { _rigidbody.velocity = Vector2.zero; }
    }
}

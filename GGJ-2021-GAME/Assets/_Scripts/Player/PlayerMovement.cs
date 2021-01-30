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

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var positionDifference = Vector3.zero;

        positionDifference.x = mousePosition.x - transform.position.x;
        positionDifference.y = mousePosition.y - transform.position.y;

        positionDifference.Normalize();

        print(positionDifference);

        _animator.SetFloat("LookMouseX", positionDifference.x);
        _animator.SetFloat("LookMouseY", positionDifference.y);

        if (moveDirection.SqrMagnitude() != 0) _animator.SetBool("IsMoving", true);
        else _animator.SetBool("IsMoving", false);
    }

    private void Move(Vector2 direction)
    {
        var moveVelocity = direction * velocity;
        _rigidbody.velocity = moveVelocity;
    }
}

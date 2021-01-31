using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocity = 10f;
    private Rigidbody2D _rigidbody = null;
    private Animator _animator = null;

    [SerializeField] private AudioClip step1;
    [SerializeField] private AudioClip step2;
    [SerializeField] private AudioClip step3;

    private AudioSource audioSrc;

    private float timeToStep = 0.0f;
    [SerializeField] private float timeToStepStart;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();

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

            if (moveDirection.SqrMagnitude() != 0)
            {
                timeToStep -= Time.deltaTime;

                if (timeToStep <= 0)
                {
                    PlayStepSound();
                }



                _animator.SetBool("IsMoving", true);
            }
            else _animator.SetBool("IsMoving", false);
        }
    }

    private void Move(Vector2 direction)
    {
        var moveVelocity = direction * velocity;
        if (direction != Vector2.zero) { _rigidbody.velocity = moveVelocity; }
        else { _rigidbody.velocity = Vector2.zero; }
    }

    private void PlayStepSound()
    {
        timeToStep = timeToStepStart;

        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0: audioSrc.clip = step1;
                break;

            case 1: audioSrc.clip = step2;
                break;

            default: audioSrc.clip = step3;
                break;
        }

        audioSrc.Play();
    }
}

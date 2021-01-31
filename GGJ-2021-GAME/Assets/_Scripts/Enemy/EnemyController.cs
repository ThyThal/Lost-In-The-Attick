using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Seeker _seeker;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 200f;
    [SerializeField] private float _nextWaypointDistance = 3f;
    [SerializeField] private float fleeTimer = 3.5f;
    [SerializeField] private Vector3 _spawnPosition;

    private bool fleed;


    [SerializeField] private AudioClip[] clips;
    private AudioSource audioSrc;
    [SerializeField] private float minSoundTriggerTime;
    [SerializeField] private float maxSoundTriggerTime;
    private float currentSoundTriggerTime;


    [SerializeField] private bool isFlee;
    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndPath;
    private bool enemyTriggered = false;
    private Animator animator = null;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        _spawnPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator.SetBool("IsMoving", false);
    }

    private void Update()
    {
        if (isFlee) 
        {
            fleeTimer -= Time.deltaTime; 
            FleeDeathTimer(); 
        }

        if (enemyTriggered)
        {
            if (_path == null) { return; }

            if (_currentWaypoint >= _path.vectorPath.Count)
            {
                _reachedEndPath = true;
                return;
            }

            else
            {
                _reachedEndPath = false;
            }

            animator.SetBool("IsMoving", true);

            Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rigidbody.position).normalized;
            Vector2 force = direction * _speed * Time.deltaTime;

            //La shata te mira
            if (force.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (force.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            _rigidbody.AddForce(force);

            float distance = Vector2.Distance(_rigidbody.position, _path.vectorPath[_currentWaypoint]);
            if (distance < _nextWaypointDistance) { _currentWaypoint++; }
        }

        currentSoundTriggerTime -= Time.deltaTime;
        if (currentSoundTriggerTime <= 0)
        {
            PlaySFX();   
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            PlaySFX();
            FollowTrigger();
            
        }
    } // Triggers follow Player.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            FleeTrigger();
        }
    } // Triggers flee from Player.

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

    private void UpdatePathPlayer()
    {
        if (_seeker.IsDone()) { _seeker.StartPath(_rigidbody.position, _target.position, OnPathComplete); }
    }

    private void UpdatePathFlee()
    {
        if (_seeker.IsDone()) { _seeker.StartPath(_rigidbody.position, _spawnPosition, OnPathComplete); }
    }

    private void FollowTrigger()
    {
        enemyTriggered = true;
        InvokeRepeating("UpdatePathPlayer", 0f, 0.5f);
    }

    public void FleeTrigger()
    {
       if (fleed) return;

        CancelInvoke();
        isFlee = true;
        InvokeRepeating("UpdatePathFlee", 0f, 0.5f);
        _speed *= 5;
        minSoundTriggerTime /= 7;
        maxSoundTriggerTime /= 7;
        PlaySFX();
        fleed = true;
    }

    private void FleeDeathTimer()
    {
        if (fleeTimer <= 0) { Destroy(this.gameObject); }
    }


    private void GenerateNewTimeToPlaySound()
    {
        currentSoundTriggerTime = Random.Range(minSoundTriggerTime, maxSoundTriggerTime);
    }

    private void PlaySFX()
    {
        int randomClip = Random.Range(0, clips.Length - 1);

        audioSrc.clip = clips[randomClip];

        audioSrc.Play();

        GenerateNewTimeToPlaySound();
    }
}

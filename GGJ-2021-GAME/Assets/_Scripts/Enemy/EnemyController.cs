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
    [SerializeField] private float fleeTimer = 2f;
    [SerializeField] private Vector3 _spawnPosition;


    [SerializeField] private bool isFlee;
    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndPath;
    private bool enemyTriggered = false;
    private Animator animator = null;

    private void Awake()
    {
        _spawnPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
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
        else
        {
            animator.SetBool("IsMoving", false);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            FollowTrigger();
        }
    } // Triggers follow Player.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            Debug.Log("LA CONCHA DE TU VIEJA NO FUNCIONA");
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
        CancelInvoke();
        isFlee = true;
        InvokeRepeating("UpdatePathFlee", 0f, 0.5f);
    }

    private void FleeDeathTimer()
    {
        if (fleeTimer <= 0) { Destroy(this.gameObject); }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 200f;
    [SerializeField] private float _nextWaypointDistance = 3f;

    [SerializeField] private Seeker _seeker;
    [SerializeField] private Rigidbody2D _rigidbody;
    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndPath;

    private bool followPlayer = false;

    private Animator animator = null;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        if (followPlayer)
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
            _rigidbody.AddForce(force);

            float distance = Vector2.Distance(_rigidbody.position, _path.vectorPath[_currentWaypoint]);
            if (distance < _nextWaypointDistance) { _currentWaypoint++; }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone()) { _seeker.StartPath(_rigidbody.position, _target.position, OnPathComplete); }
    }

    public void EnemyFindPath()
    {
        followPlayer = true;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void DestroyEnemy()
    {

    }
}

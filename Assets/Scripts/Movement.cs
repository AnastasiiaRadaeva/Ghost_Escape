using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script will work only if there is rigidbody2D component
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 8f;
    public float speedMultiplier = 1f;
    // public Vector2 initialDirection;
    public LayerMask obstacleLayer;
    
    private Rigidbody2D _rigidBody;
    private Vector2 _nextDirection;
    private Vector3 _startPosition;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }
    
    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        // direction = initialDirection;
        _nextDirection = Vector2.zero;
        transform.position = _startPosition;
        _rigidBody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        // Try to move in the next direction while it's queued to make movements
        // more responsive
        if (_nextDirection != Vector2.zero) {
            SetDirection(_nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidBody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        _rigidBody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 curDirection, bool forced = false)
    {
        // Only set the direction if the tile in that direction is available
        // otherwise we set it as the next direction so it'll automatically be
        // set when it does become available
        if (forced || !Occupied(curDirection))
        {
            direction = curDirection;
            _nextDirection = Vector2.zero;
        }
        else
        {
            _nextDirection = curDirection;
        }
    }
    
    private bool Occupied(Vector2 direction)
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one, 0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }
}

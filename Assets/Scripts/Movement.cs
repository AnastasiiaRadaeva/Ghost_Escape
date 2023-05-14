using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script will work only if there is rigidbody2D component
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private LayerMask obstacleLayer;
    
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
        _nextDirection = Vector2.zero;
        transform.position = _startPosition;
        _rigidBody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        SetDirection(direction);
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidBody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        _rigidBody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 curDirection, bool pushOff = true)
    {
        if (!Occupied(curDirection))
            direction = curDirection;
        else if (pushOff)
            direction = curDirection * -1f;
    }
    
    private bool Occupied(Vector2 checkDirection)
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, 
            new Vector2(0.3f, 0.7f), 0f, checkDirection, 1f, obstacleLayer);
        return hit.collider != null;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    // public SpriteRenderer spriteRenderer;
    // public new Collider2D collider;
    public Movement movement;

    private void Awake()
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        // collider = GetComponent<Collider2D>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        // Set the new direction based on the current input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            movement.SetDirection(Vector2.right);
        }
    }
}

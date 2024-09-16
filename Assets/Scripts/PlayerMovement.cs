//Program Started by: Ishmael Kwayisi

//This program is for the player to 
//move in the game world in all directions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movSpeed;
    private Vector2 movement;
    Rigidbody2D rb;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        movement.Set(InputManager.movement.x, InputManager.movement.y);

        rb.velocity = movement * movSpeed;
    }
}

//Program Started by: Ishmael Kwayisi

//This program is for the player to 
//move in the game world in all directions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movSpeed = 10f;
    private Vector2 movement;
    private Animator animator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    public VectorValue startingPosition;

    GameManager manager;

    Rigidbody2D rb;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
    }

    private void Start()
    {
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        //this freezes player while in dialogue mode - jon
        if (Dialoguemanger.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        movement.Set(InputManager.movement.x, InputManager.movement.y);

        rb.velocity = movement * movSpeed;
        
        animator.SetFloat(horizontal, rb.velocity.x);
        animator.SetFloat (vertical, rb.velocity.y);

        if(movement != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, movement.x);
            animator.SetFloat(lastVertical, movement.y);
        }
    }

}

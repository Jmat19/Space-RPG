using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{  
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Values values;
    Vector2 movement;
    void Awake()
    {
        values = GameObject.Find("Godsend").GetComponent<Values>();
    }

    void Start()
    {
        transform.position = new Vector2(values.heldposX, values.heldposY);
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
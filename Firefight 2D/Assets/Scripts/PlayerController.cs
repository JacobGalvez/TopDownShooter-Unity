using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Weapon weapon;

    private Vector2 moveDirection;

    private Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate() 
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            weapon.StartFiring();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.StopFiring();
        }
        

        moveDirection = new Vector2(moveX, moveY).normalized; // TODO come back
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        // Rotate player to follow mouse
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 0f;
        rb.rotation = aimAngle;
    }
}

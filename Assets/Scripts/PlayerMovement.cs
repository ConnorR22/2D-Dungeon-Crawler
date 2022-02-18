using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta; // diff between current position and distance to next movement
    private RaycastHit2D hit;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        


        // Returns -1 for left, 0 for none, 1 for right movement
        float x = Input.GetAxisRaw("Horizontal");

        float y = Input.GetAxisRaw("Vertical");

        // Reset Move Delta, so that new frame returns to 0 (player hasnt moved)
        moveDelta = new Vector3(x,y,0);

        // Swap sprite direction, whetehr youre going right or left
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;

        } else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Check if the direction player is moving is colliding with a wall or space unable to be moved to, if null allow movement
        // Transform.postion gets the current position of the player
        // boxCollider.size gets the area of collision from the player
        // new Vector2 is the new position were moving our player to
        // Mathf is calculating the the distance to the space were to occupy
        // LayerMask is the saying the Player is being checked if its colliding with anything with the Actor or blocking Layers
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            // Movement in y axis
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        // Check if the direction player is moving is colliding with a wall or space unable to be moved to, if null allow movement
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            // Movement in y axis
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }



    }
}

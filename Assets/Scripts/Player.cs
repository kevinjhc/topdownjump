using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    public float moveSpeed = 5f;

    // Jump
    public float jumpTime = 2.0f;
    public float currentTimeInJump = 0.0f;
    public float jumpDecay = 0.1f;
    public float jumpForce = 300;
    public float gravity = 7.0f;
    float axisY;
    bool isJumping;

    private float z;

    void Start()
    {
      z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
      movement.x = Input.GetAxisRaw("Horizontal");
      movement.y = Input.GetAxisRaw("Vertical");

      movement = movement.normalized;

      animator.SetFloat("Horizontal", movement.x);
      animator.SetFloat("Vertical", movement.y);
      animator.SetFloat("Magnitude", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
      rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

      Jump();
    }



    void Jump()
    {
      Debug.Log("IsJumping: " + isJumping + " | zPos: " + z + " | currentTimeInJump " + currentTimeInJump);

      if (Input.GetButtonDown("Jump") && !isJumping)
      {
        // Start Jump
        isJumping = true;
        z = 50.0f;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
        currentTimeInJump = jumpTime;
        return;
      }

      if (isJumping)
      {
        // In jump - allowed to ignore stuff
        currentTimeInJump -= jumpDecay;
      }

      if (currentTimeInJump <= 0)
      {
        // landing -- can't ignore stuff anymore
        isJumping = false;
        z = 0.0f;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
        currentTimeInJump = 0.0f;
        return;
      }
    }
/*
    void Jump() {

      if (transform.position.y <= axisY)
        OnLanding();

      if (Input.GetButtonDown("Jump") && !isJumping) {
        axisY = transform.position.y;
        isJumping = true;
        animator.SetBool("isJumping", isJumping);

        // rb.gravityScale = 2.0f;
        // rb.AddForce(new Vector2(transform.position.x + gravity, jumpForce));


        Physics2D.IgnoreCollision(GameObject.Find("Wall Tilemap").GetComponent<Collider2D>(), GetComponent<Collider2D>());
      }
    }

    void OnLanding() {
      isJumping = false;
      rb.gravityScale = 0;
      axisY = transform.position.y;
      animator.SetBool("isJumping", false);

      Physics2D.IgnoreCollision(GameObject.Find("Wall Tilemap").GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }
*/
    void OnCollisionEnter2D(Collision2D collision) {
      // if (collision.gameObject.name == "Wall Tilemap" && isJumping) {
      //   Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
      //   Debug.Log(collision.gameObject.name);
      // }

      if (isJumping && collision.gameObject.transform.position.z < transform.position.z)
      {
        Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
      }
    }
}

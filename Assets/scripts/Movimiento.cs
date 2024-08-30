using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float jump = 8f;
    public Animator animator;

    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal"); // "Horizontal" para teclas A/D o flechas
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Animaciones de movimiento
        animator.SetFloat("movimiento", Mathf.Abs(move));

        if (move > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (move < 0 && isFacingRight)
        {
            Flip();
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && PlayerInGround.inGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            animator.SetBool("saltando", true);
        }

        // Actualización de animaciones de salto y caída
        if (PlayerInGround.inGround)
        {
            animator.SetBool("saltando", false);
            animator.SetBool("cayendo", false);
        }
        else if (rb.velocity.y < 0)
        {
            animator.SetBool("cayendo", true);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("cajita"))
        {
            // Destruye la caja inmediatamente al tocarla
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("checkpoint"))
        {
            Debug.Log("Checkpoint");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("checkpoint"))
        {
            Debug.Log("Zona segura");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("checkpoint"))
        {
            Debug.Log("Saliendo de zona segura");
        }
    }
}

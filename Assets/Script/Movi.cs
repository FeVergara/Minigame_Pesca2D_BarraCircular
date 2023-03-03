using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movi : MonoBehaviour
{
    public float moveSpeed;
    Animator anim;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        controlAnim();
    }

    private void FixedUpdate()
    {
        if(FindObjectOfType<Pesca>().podePescar)
        {
            Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
            rb.velocity = moveDir;
        }

    }

    void controlAnim()
    {
        if (rb.velocity.x <= -0.01)
        {
            GetComponent<Transform>().transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(rb.velocity.x >= 0.01)
        {
            GetComponent<Transform>().transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (rb.velocity.x != 0)
            anim.SetBool("walk", true);
        else
            anim.SetBool("walk", false);
    }
}
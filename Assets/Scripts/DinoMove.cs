using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 5);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        if (nextMove == 0)
            anim.SetBool("isMove", false);
        else
            anim.SetBool("isMove", true);

        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        Invoke("Think", 5);
    }
}
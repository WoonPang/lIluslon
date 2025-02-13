using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    public float Speed;
    public float jump;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Collider2D collider;
    Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // ����
        if (Input.GetKeyDown("c") && !anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }

        // ���� �ӵ�
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        // ���� ��ȯ
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isMove", false);
        else
            anim.SetBool("isMove", true);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // ������ �̵� �ִ� �ӵ�
        if (rigid.velocity.x > Speed)
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);
        // ���� �̵� �ִ� �ӵ�
        else if (rigid.velocity.x < Speed*(-1))
            rigid.velocity = new Vector2(Speed*(-1), rigid.velocity.y);

        // ���� ���� ������ �����Ǿ� ���� but ���� 1ȸ�� ���� �ٵ� �� �𸣰ڽ��ϴ�.
        Vector2 rayOrigin = new Vector2(collider.bounds.center.x, collider.bounds.min.y);
        Debug.DrawRay(rayOrigin, Vector2.down * 1f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rayOrigin, Vector2.down, 1, LayerMask.GetMask("Ground", "Block"));
        if (rayHit.collider != null)
        {
            if (rayHit.distance < 0.5f)
                anim.SetBool("isJump", false);
        }
    }
}
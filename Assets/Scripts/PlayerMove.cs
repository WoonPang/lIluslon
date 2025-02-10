using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed;
    public float jump;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 점프
        if (Input.GetKeyDown("c") && !anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }

        // 정지 속도
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        // 방향 전환
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

        // 오른쪽 이동 최대 속도
        if (rigid.velocity.x > Speed)
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);
        // 왼쪽 이동 최대 속도
        else if (rigid.velocity.x < Speed*(-1))
            rigid.velocity = new Vector2(Speed*(-1), rigid.velocity.y);

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    anim.SetBool("isJump", false);
            }
        }
    }
}

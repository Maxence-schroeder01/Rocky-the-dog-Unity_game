using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using TMPro;
using UnityEngine;

public class ennemy : MonoBehaviour
{
   

    public float speed = 1;

    private Animator m_Anim;
    Rigidbody2D body;
    private Collider2D collider2d;
    Transform trans;
    public LayerMask enemyMask;
    float width;

     void Start()
    {
        trans = this.transform;
        body = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        m_Anim = GetComponent<Animator>();
        width = GetComponent<Collider2D>().bounds.extents.x;
    }

    void FixedUpdate()
    {
        Vector2 lineCastPos = trans.position - trans.right * width;

        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

        if (!isGrounded)
        {
            Vector3 currRot = trans.eulerAngles;
            currRot.y += 180;
            trans.eulerAngles = currRot;
        }

        Vector2 myVel = body.velocity;
        myVel.x = -trans.right.x * speed;
        body.velocity = myVel;
    }

    public void died()
    {
        m_Anim.SetBool("Dead", true);
        Destroy(this);
        StartCoroutine(delay());
        Destroy(this.gameObject);
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
    }



}
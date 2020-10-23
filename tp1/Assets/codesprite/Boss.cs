using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
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
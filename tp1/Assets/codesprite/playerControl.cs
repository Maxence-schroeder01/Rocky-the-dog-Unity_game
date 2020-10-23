using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerControl : MonoBehaviour
{
    private Rigidbody2D m_Rgb;
    private Animator m_Anim;
    private bool m_Isflipped = true;
    private bool isGrouded;
    private float m_HorizontalMove;
    public float MaxSpeed = 3f;
    public float Jumpforce = 1;
    public Transform m_GroudCast;
    public float checkRaduis;
    public LayerMask m_GroudLayers;
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHeart;
    public int extraJumps;
    public int extraJumpValue;
    /*internal new*/
    public Collider2D collider2d;
    bool invincibleTime = false;
    public float timer;
    Material mWhite;
    Material mDefaut;
    SpriteRenderer sRend;
    private void Start()
    {
        extraJumps = extraJumpValue;
        m_Rgb = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        sRend = GetComponent<SpriteRenderer>();
        mDefaut = sRend.material;
        mWhite = Resources.Load("mWhite", typeof(Material)) as Material;
    }
    private void Update()
    {
        win();
        if (win() == true)
            SceneManager.LoadScene("Victory");
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            invincibleTime = false;
        }

        if(isGrouded == true)
        {
            extraJumps = extraJumpValue;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            m_Rgb.velocity = Vector2.up * Jumpforce;
            m_Anim.SetTrigger("Jump");
            extraJumps--;
        } else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrouded == true)
        {
            m_Rgb.velocity = Vector2.up * Jumpforce;
        }

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHearts;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        if (playerManger.boneCount == 100)
        {
            if (health == 4)
            {
                health++;
            }
        }

    }
    void invicibleperiod()
    {
        timer = 1;
        if (timer > 0 )
        {
            invincibleTime = true;
        }
    }
    void endinvicibleperiod()
    {
        invincibleTime = false;
    }
    IEnumerator Flash()
    {
        for (int i = 0; i < 3; i++)
        {
            invicibleperiod();
            yield return new WaitForSeconds(0.25f);
            sRend.material = mWhite;
            Invoke("ResetMaterial", 0.15f);
            endinvicibleperiod();
        }
    }
    IEnumerator jump()
    {
        yield return new WaitForSeconds(25f);
    }
    void ResetMaterial()
    {
        sRend.material = mDefaut;
    }
    public void TakeDamage(int a_Damage)
    {

        health -= a_Damage;
        UnityEngine.Debug.Log("degats");
        hurt();
        if (health == 0)
        {
            dead();
            UnityEngine.Debug.Log("zadazdazdzadazaz" + "azdazazazdazd");
        }
    }
    private void FixedUpdate()
    {
        isGrouded = Physics2D.OverlapCircle(m_GroudCast.position, checkRaduis, m_GroudLayers);
        m_HorizontalMove = Input.GetAxis("Horizontal");
        m_Rgb.velocity = new Vector2(m_HorizontalMove * MaxSpeed, m_Rgb.velocity.y);
        float t_AbsVelocityX = Mathf.Abs(m_Rgb.velocity.x);
        float t_AnimHorizontalSpeed = Mathf.Lerp(0, 1, t_AbsVelocityX / MaxSpeed);
        m_Anim.SetFloat("speed", t_AnimHorizontalSpeed);
        if (m_Isflipped == false && m_HorizontalMove > 0)
        {
            Flip();
        }
        else if (m_Isflipped == true && m_HorizontalMove < 0)
        {
            Flip();
        }
    }
    private void Flip()
    {
        m_Isflipped = !m_Isflipped;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    public void hurt()
    {
        m_Anim.SetTrigger("Hurt");
        StartCoroutine("Flash");
    }
    public void dead()
    {
        m_Anim.SetBool("Dead", true);
        playerManger.boneCount = 0;
        SceneManager.LoadScene("GameOver");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<ennemy>();
        var fire = collision.gameObject.GetComponent<fire>();
        var deadzone = collision.gameObject.GetComponent<deadzone>();
        var boss = collision.gameObject.GetComponent<Boss>();
        if (player != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                    Vector2 velocity = m_Rgb.velocity;
                    velocity.y = Jumpforce;
                    m_Rgb.velocity = velocity;
                    player.gameObject.GetComponent<ennemy>().died();
                }
                else
                {
                    TakeDamage(1);
                }
            }
        }
        if (fire)
        {
            TakeDamage(1);
        }
        if (deadzone)
        {
            dead();
        }
 
        if (boss != null)
        {
            float nonosf = playerManger.boneCount;
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                    Vector2 velocity = m_Rgb.velocity;
                    velocity.y = Jumpforce;
                    m_Rgb.velocity = velocity;
                    boss.gameObject.GetComponent<Boss>().died();

                    if (nonosf >= 1)
                    {
                        SceneManager.LoadScene("Victory");
                    }
                }
                else
                {
                    TakeDamage(4);
                }
            }
        }
        var panneau = collision.gameObject.GetComponent<Panneau>();
        float nonos = playerManger.boneCount;
        if (panneau)
        {
            if (nonos >= 1)
            {
                SceneManager.LoadScene("Scene2");
            }
        }

        var boulet = collision.gameObject.GetComponent<Chainemere>();

        if (boulet)
        {
            TakeDamage(1);
        }
    }

    private bool win()
    {
        float nonosw = playerManger.boneCount;

        if (nonosw == 37)
        {
            return true;
        }
        else
            return false;
    }
}


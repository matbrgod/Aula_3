using System.Linq.Expressions;
using UnityEngine;

public class AiFly : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public SpriteRenderer spriteRenderer;
    public Vector3 res;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void FixedUpdate()
    {
        if (player == null) return;

        res = player.transform.position - transform.position;
        res.Normalize();
        rb.linearVelocity = res * speed * Time.fixedDeltaTime;

        //if ternário
        //spriteRenderer.flipX = res.x > 0? true : false;
        if(res.x >= 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(res.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player = null;
            rb.linearVelocity = Vector2.zero;

        }
    }
}

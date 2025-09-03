using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f; // Speed of player movement
    Vector2 movement; // Movement vector
    public float jumpForce = 300f; // Force applied when jumping
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public float knockbackForce = 300f;

    public bool canMove;

    bool invincible = false;

    bool canJump = true; // Flag to check if the player can jump

    float distanceToGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), 0);
        if (movement.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        if (movement.x > 0)
        {
            spriteRenderer.flipX = true;
        }


        // Allow jumping only when the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(new Vector2(0, jumpForce)); // Apply jump force
            canJump = false; // Set canJump to false to prevent double jumping
        }
        animator.SetBool("Pulando", !canJump);


    }
    // FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        // rb.AddForce(movement * speed);
        Vector3 newMovement = new Vector3(movement.x * speed, rb.linearVelocity.y, 0);
        rb.linearVelocity = newMovement;
        animator.SetFloat("Velocidade.x", Mathf.Abs(movement.x));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 5f);
        if(hit.collider != null)
        {
            distanceToGround = hit.distance;
            animator.SetFloat("DistaciaChao", distanceToGround);
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }

    }
    public void DamageTaken(Vector3 relativeImpact)
    {
        animator.SetTrigger("Dano");
        canMove = false;
        StartCoroutine(KnockBack(relativeImpact));
        
    }

    IEnumerator KnockBack(Vector3 relativeImpact)
    {
        yield return new WaitForFixedUpdate(); // Wait for the next physics update

        rb.AddForce(relativeImpact.normalized * knockbackForce, ForceMode2D.Impulse); // Apply knockback force

        yield return new WaitForSeconds(0.2f); // Wait for the knockback effect to finish

        rb.linearVelocity = new Vector2(0, 0);

        canMove = true; // Re-enable movement
        spriteRenderer.color = Color.white;
        StartCoroutine(Invincible(2f));
    }

    IEnumerator Invincible(float time)
    {
        float elapsed = 0f;
        invincible = true;
        while(true)
        {
            elapsed += Time.deltaTime;
            //spriteRenderer = new Color();
            if(elapsed >= time)
            {
                invincible = false;
                spriteRenderer.color = Color.white;
                yield break;
            }
            yield return new WaitForFixedUpdate();

        }
    }
   
    void OnCollisionEnter2D(Collision2D collision)
    {   
        
        // Check if the player is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true; // Allow jumping again when the player is on the ground
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {


    }
}
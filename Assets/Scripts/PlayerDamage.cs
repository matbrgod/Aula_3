using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int maxHealth;
    public int health;
    PlayerMove playerMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (health <= 0) Destroy(gameObject);

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (playerMove.DamageTaken(transform.position - collision.transform.position))
            {
                health -= 10;
                Debug.Log("Player Health: " + health);
            }
        }
    }
}

using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Salud del enemigo")]
    public int MaxHealth = 3;
    public int currentHealth;

    [Header("Ajustes de Retroceso (Knockback)")]
    public float knockbackForceX = 4f;
    public float knockbackForceY = 2f;
    public float stunDuration = 0.35f;

    private Rigidbody2D rb;
    private EnemyPatrol enemyPatrol;

    void Start()
    {
        currentHealth = MaxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    public void TakeDamage(int damageAmount, Vector3 attackerPosition)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(KnockbackRoutine(attackerPosition));
        }
    }

    IEnumerator KnockbackRoutine(Vector3 attackerPosition)
    {
        // 1. Pausa el movimiento de patrulla
        if (enemyPatrol != null)
        {
            enemyPatrol.isStunned = true;
        }

        // 2. Frena la inercia previa antes de empujar
        if (rb != null)
        {
            rb.velocity = Vector2.zero;

            // Determina la dirección: empuja hacia la derecha si el jugador ataca desde la izquierda y viceversa
            float knockbackDir = (transform.position.x < attackerPosition.x) ? -1f : 1f;
            rb.AddForce(new Vector2(knockbackDir * knockbackForceX, knockbackForceY), ForceMode2D.Impulse);
        }

        // 3. Tiempo que el enemigo queda aturdido / quieto
        yield return new WaitForSeconds(stunDuration);

        // 4. Reanuda la patrulla normal
        if (enemyPatrol != null)
        {
            enemyPatrol.isStunned = false;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
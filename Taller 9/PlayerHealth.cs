using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Sistema de Vidas")]
    public int maxLives = 3;
    private int currentLives;

    [Header("Daño y Empujón")]
    public float invulnerabilityTime = 1f;
    public float knockbackForceX = 5f;
    public float knockbackForceY = 5f;
    private bool isInvulnerable = false;

    private Animator animator;
    private Rigidbody2D rb;
    private PlayerController playerController;

    void Start()
    {
        currentLives = maxLives;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Daño por colisión directa con enemigos
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            TakeDamage(collision.transform.position);
        }
    }

    // Sobrecarga principal: recibe la posición del impacto
    public void TakeDamage(Vector3 damageSourcePosition)
    {   
        if (isInvulnerable) return;

        currentLives--;

        if(CameraShake.Instance != null){

            CameraShake.Instance.Sacudir(0.2f, 0.2f);
        }

        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        if (currentLives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(HurtRoutine(damageSourcePosition));
        }
    }

    // Sobrecarga secundaria: recibe directamente el Transform (útil para trampas)
    public void TakeDamage(Transform damageSource)
    {
        
        if (damageSource != null)
        {
            TakeDamage(damageSource.position);
            
            if(CameraShake.Instance != null){
                CameraShake.Instance.Sacudir(0.2f, 0.2f);
            }

        }
    }

    IEnumerator HurtRoutine(Vector3 damageSourcePosition)
    {
        isInvulnerable = true;
        
        // Bloquea el input en PlayerController para no sobreescribir la física
        if (playerController != null)
        {
            playerController.canMove = false;
        }

        // Detiene la velocidad previa antes del impacto
        rb.velocity = Vector2.zero;

        // Aplica el impulso en dirección opuesta a la fuente del daño
        float knockbackDirection = (transform.position.x < damageSourcePosition.x) ? -1f : 1f;
        rb.AddForce(new Vector2(knockbackForceX * knockbackDirection, knockbackForceY), ForceMode2D.Impulse);

        // Espera el tiempo de inmunidad
        yield return new WaitForSeconds(invulnerabilityTime);

        // Devuelve el control al jugador
        if (playerController != null)
        {
            playerController.canMove = true;
        }
        isInvulnerable = false;
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }
}
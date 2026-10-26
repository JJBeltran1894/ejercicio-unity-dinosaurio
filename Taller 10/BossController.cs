using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Salud y UI")]
    public int maxHealth = 10;
    private int currentHealth;
    public Slider barraVidaUI;
    public GameObject panelVictoriaUI; // Variable para el panel de victoria

    [Header("Movimiento")]
    public float speed = 2.5f;
    public Transform player;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isEnraged = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        currentHealth = maxHealth;
        if (barraVidaUI != null)
        {
            barraVidaUI.maxValue = maxHealth;
            barraVidaUI.value = maxHealth;
        }
    }

    void Update()
    {
        if (player != null && rb != null)
        {
            Vector2 targetX = new Vector2(player.position.x, rb.position.y);
            Vector2 nextPosition = Vector2.MoveTowards(rb.position, targetX, speed * Time.deltaTime);

            rb.MovePosition(new Vector2(nextPosition.x, rb.position.y));

            // Orientación del sprite según la posición del jugador
            if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (barraVidaUI != null)
        {
            barraVidaUI.value = currentHealth;
        }

        // Fase de Furia al 50% de vida o menos
        if (!isEnraged && currentHealth <= maxHealth / 2)
        {
            EnterEnragePhase();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void EnterEnragePhase()
    {
        isEnraged = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }

        speed *= 1.8f;
    }

    void Die()
    {
        // 1. Activa el panel de victoria en pantalla
        if (panelVictoriaUI != null)
        {
            panelVictoriaUI.SetActive(true);
        }

        // 2. Detiene el paso del tiempo del juego
        Time.timeScale = 0f;

        // 3. Destruye al Boss
        Destroy(gameObject);
    }
}
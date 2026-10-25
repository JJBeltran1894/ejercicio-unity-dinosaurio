using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float speed = 3f;
    private bool movinRigth = true;

    [Header("Detectores")]
    public Transform groundCheck;
    public float distanceToGround = 1.5f;
    public float distanceToWall = 1.5f;
    public LayerMask groundLayer;

    [Header("Estado")]
    public bool isStunned = false;

    private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned) return;
        rb.velocity = new Vector2(movinRigth ? speed: -speed, rb.velocity.y);
        Vector2 direction = movinRigth ? Vector2.right : Vector2.left;

        RaycastHit2D isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, distanceToGround, groundLayer);
        RaycastHit2D isWallAhead = Physics2D.Raycast(transform.position, direction, distanceToWall, groundLayer);

        if(isGroundAhead.collider == null || isWallAhead.collider != null){
            Flip();
        }

        
    }
    void Flip(){
            movinRigth = !movinRigth;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
    }

    private void OnDrawGizmos(){
        if(groundCheck != null){
            Gizmos.DrawRay(groundCheck.position, Vector2.down * distanceToGround);
        }
        Vector2 direction = movinRigth ? Vector2.right : Vector2.left;
        Gizmos.DrawRay(transform.position, direction* distanceToWall);
    }
}
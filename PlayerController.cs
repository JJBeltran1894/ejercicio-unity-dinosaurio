using UnityEngine;

public class PlayerController : MonoBehaviour



{
    private Rigidbody2D rd;   
    public float moveSpeed = 4f;
    public float jumpForce = 9f;
    
    private bool isGrounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        rd.velocity = new Vector2(move * moveSpeed, rd.velocity.y);

        //Salto

        if (Input.GetButtonDown("Jump") && isGrounded){
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        void OnCollisionEnter2D(Collision2D collision){
            
        }

    }
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }
}

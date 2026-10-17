using UnityEngine;

public class PlayerController : MonoBehaviour



{
    private Rigidbody2D rd;   
    public float moveSpeed = 4f;
    public float jumpForce = 9f;
    private Animator animator;
    private bool isGrounded;
    private bool facinRigth = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        float speedAnimation = Mathf.Abs(move);
        animator.SetFloat("Speed", speedAnimation);

        rd.velocity = new Vector2(move * moveSpeed, rd.velocity.y);

        // Orientacion personaje
        if(move>0 && !facinRigth){
            Flip();
        }else if(move < 0 && facinRigth){
            Flip();
        }

        //Salto

        if (Input.GetButtonDown("Jump") && isGrounded){
            rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("IsJump", true);
        }


    }
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJump", false);
        }
    }
    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
            animator.SetBool("IsJump", true);
        }
    }
        void Flip(){
        facinRigth = !facinRigth;
        Vector3 scale  = transform.localScale;
        scale.x *=-1;
        transform.localScale = scale;
    }
}

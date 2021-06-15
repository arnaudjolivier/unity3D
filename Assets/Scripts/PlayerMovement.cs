using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public Rigidbody2D rb;
    public float JumpForce;
    public Transform CheckGround;
    public float groundCheckRadius;
    public LayerMask collisionLayer;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private bool IsJumping;
    private bool isGrounded;
    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de PlayerMovement dans la scéne");
            return;
        }
        instance = this;
    }
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.fixedDeltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            IsJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x); // valeur absolue de la vitesse
        animator.SetFloat("Speed", characterVelocity);
    }

    void FixedUpdate() // notions de physique donc fixed update au lieu de update
    {
        isGrounded = Physics2D.OverlapCircle(CheckGround.position, groundCheckRadius , collisionLayer); // check si ca touche le sol
        MovePlayer(horizontalMovement); //deplacement du joueur
    }
    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity,targetVelocity, ref velocity, .05f);

        if (IsJumping == true)
        {
            rb.AddForce(new Vector2(0f, JumpForce));
            IsJumping = false;
        }
    }
    void Flip(float _velocity) //permet de regarder a droite ou gauche
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
    private void OnDrawGizmos() // affiche le gizmos
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CheckGround.position, groundCheckRadius); 
    }
}

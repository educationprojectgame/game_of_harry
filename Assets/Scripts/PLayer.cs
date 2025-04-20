using UnityEngine;

public class PLayer : MonoBehaviour
{
    [SerializeField] float speed;
    public Vector2 moveVelocity;
    public Animator animator;
    public Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        Move();
        animator.SetFloat("Horizontal", moveVelocity.x);
        animator.SetFloat("Vertical", moveVelocity.y);
        animator.SetFloat("Speed", moveVelocity.sqrMagnitude);
        

    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

}

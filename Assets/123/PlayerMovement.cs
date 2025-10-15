using UnityEngine;
using UnityEngine.InputSystem; // ✅ 新輸入系統命名空間

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 5f;

    private bool isGrounded = false;
    private bool hasJumped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- 使用新 Input System ---
        float moveInput = 0f;

        if (Keyboard.current.rightArrowKey.isPressed)
            moveInput = 1f;
        else if (Keyboard.current.leftArrowKey.isPressed)
            moveInput = -1f;

        // 移動
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // 跳躍 — 只能在地面且尚未跳過時觸發
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded && !hasJumped)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            hasJumped = true;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            hasJumped = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

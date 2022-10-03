using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{ 
    [SerializeField] float speed = 5f;
    [SerializeField] float runMultiplier = 1.5f;
    Vector2 moveVector;

    Rigidbody2D rb;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        float z = rb.gameObject.transform.position.z;
        float x = CrossSceneData.mainScenePlayerX;
        float y = CrossSceneData.mainScenePlayerY;
        rb.gameObject.transform.position = new Vector3(x, y, z);
    }

    private void FixedUpdate() => rb.velocity = moveVector * speed;

    private void Update()
    {
        GetInput();
        SetAnimations();
    }

    // ========== !!! Change this method to either use the Input Manager or the Input System !!! ==========
    private void GetInput()
    {
        float moveY = Input.GetAxisRaw("VerticalMovement");
        float moveX = Input.GetAxisRaw("HorizontalMovement");
        moveVector = new Vector2(moveX, moveY).normalized;
        if (Input.GetButton("Dash"))
        {
            moveVector *= runMultiplier;
        }
    } 

    private void SetAnimations()
    { 
        // If the player is moving
        if (moveVector != Vector2.zero)
        {
            // Trigger transition to moving state
            anim.SetBool("IsMoving", true);

            // Set X and Y values for Blend Tree
            anim.SetFloat("MoveX", moveVector.x);
            anim.SetFloat("MoveY", moveVector.y);
        }
        else
            anim.SetBool("IsMoving", false);
    }
}
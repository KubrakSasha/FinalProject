using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour{
    
    Rigidbody2D rb;
    Vector2 moveDirection;
    Vector2 mousePosition;
    public float speedMovement = 150;
    float angle;
    Camera cam;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    private void Update()
    {
        HandleMovement();  
        HandleRotation();
    }
    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speedMovement;       
        rb.rotation = angle;
    }
    void HandleMovement() 
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");        
        moveDirection = new Vector2(moveX, moveY).normalized;
        
    }
    void HandleRotation() 
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - rb.position;
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
    }
    

}

using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour{
    
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    private Vector2 _mousePosition;
    private float _angle;
    private Camera _cam;  


    private void Start()
    {        
        _rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
    }
    private void Update()
    {       
        HandleMovement();  
        HandleRotation();
    }
    private void FixedUpdate()
    {
        _rb.velocity = _moveDirection * PlayerMain.Instance.Stats.SpeedMovement;
        _rb.rotation = _angle;
    }
    private void HandleMovement() 
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        _moveDirection = new Vector2(moveX, moveY).normalized;        
    }
    private void HandleRotation() 
    {
        _mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = _mousePosition - _rb.position;
        _angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
    }
}
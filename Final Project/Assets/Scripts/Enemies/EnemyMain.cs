using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float movementSpeed = 3;
    HealthSystem healthSystem;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovementHandler>().transform;
        healthSystem = new HealthSystem(100);
    }

    // Update is called once per frame
    void Update()
    {        
        FollowForPlayer();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
            TakeDamage();
            //healthSystem.ApplyDamgage(40);
    }
    void TakeDamage() 
    {
        Destroy(this.gameObject);
    }
    void FollowForPlayer() 
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
    }
}

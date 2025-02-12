using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("")]
    public float speed = 4f;
    public Animator animator;
    public Rigidbody2D rb;
    public Transform view;
    public MonoFSM fsm = new MonoFSM();
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        fsm.InitDefaultState(0);
    }
    private void Update()
    {
        fsm.Update();
    }
    private void FixedUpdate()
    {
       fsm.FixedUpdate();
    }
    public void  HorizontalMove()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Flip(horizontalInput);
        rb.velocity = new Vector3(horizontalInput * speed, rb.velocity.y, 0);
        
    }
    private void Flip(float input)
    {
        int dir = (int)view.transform.localScale.x;
        if (input > 0f) dir = -1;
        else  if(input <0f) dir = 1;
        view.transform.localScale = new Vector3(dir, 1, 1);
    }
    public void Death()
    {
        fsm.SwitchState((int)ZombieStateName.Death);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Killer"))
        {
            Death();
        }
    }
    
}
public enum ZombieStateName
{
    Normal,
    JUMP,
    Death,
    FALL,
}
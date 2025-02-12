using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState_Jump : MonoBaseState
{
    public float JumpForce = 10;
    public float ExtraGravity = 1f;
    public PlayerController controller;

    private bool isSecondJump;
    private float enterTimer;
    public bool isGround;
    public ParticleSystem jumpParticle;
    public AudioClip[] FartSounds;
    public AudioClip jumpSound;
    private void Awake()
    {
         
    }
  
    public override void OnEnter()
    {
        controller.animator.CrossFade("Jump", 0.1f);    
        controller.rb.velocity = new Vector2(controller.rb.velocity.x, JumpForce);
        isSecondJump = false;
        enterTimer = 0.2f;
        SoundManager.Instance.PlaySound(jumpSound,0.5f);
    }

    public override void OnExit(MonoBaseState nextState)
    {
        controller.animator.CrossFade("FallBuffer", 0.1f);
    }

    public override void OnFixedUpdate()
    {
        controller.HorizontalMove();
        if (controller.rb.velocity.y >0f && !Input.GetKey(KeyCode.Space))
        {
            controller.rb.AddForce(Vector2.down * ExtraGravity);
        }
    }

    public override void OnUpdate()
    {
        enterTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && enterTimer<0f && isSecondJump == false)
        {
            isSecondJump = true;
            SoundManager.Instance.PlayRandomSound(FartSounds,0.3f);
            jumpParticle.Emit(5);
            controller.animator.CrossFade("Jump", 0.1f);
            controller.rb.velocity = new Vector2(controller.rb.velocity.x, JumpForce);
        }



       if (controller.rb.velocity.y < 0f)
        {
            controller.animator.SetBool("IsFall",true);
        }
       else
        {
            controller.animator.SetBool("IsFall", false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGround = true;
            if(controller.fsm.GetCurrentState() == this)
            controller.fsm.SwitchState(0);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGround = false;
        }
    }
}

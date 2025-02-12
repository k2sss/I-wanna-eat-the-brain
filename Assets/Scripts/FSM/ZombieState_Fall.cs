using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState_Fall : MonoBaseState
{
    public PlayerController controller;
    private float enterTimer;
    public float ExtraGravity = 1f;
    public float CoyoteTime;
    public BoxCollider2D boxCollider;
    public LayerMask targetLayer;

    public override void OnEnter()
    {
        controller.animator.CrossFade("Fall", 0.1f);
        enterTimer = 0f;
    }

    public override void OnExit(MonoBaseState nextState)
    {
    }

    public override void OnFixedUpdate()
    {
        controller.HorizontalMove();
        if (controller.rb.velocity.y > 0f)
        {
            controller.rb.AddForce(Vector2.down * ExtraGravity);
        }

        if (IsOverlappingTargetLayer())
        {
            if (controller.fsm.GetCurrentState() == this)
            {
                controller.fsm.SwitchState(0);
                controller.animator.CrossFade("FallBuffer", 0.1f);
            }
        }
    }

    public override void OnUpdate()
    {
        enterTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && enterTimer <= CoyoteTime)
        {
            controller.fsm.SwitchState((int)ZombieStateName.JUMP);
        }

        if (enterTimer > CoyoteTime)
        {
            controller.fsm.SwitchState((int)ZombieStateName.JUMP);
        }

        if (controller.rb.velocity.y < 0f)
        {
            controller.animator.SetBool("IsFall", true);
        }
        else
        {
            controller.animator.SetBool("IsFall", false);
        }


    }

    bool IsOverlappingTargetLayer()
    {
        return Physics2D.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.size, 0, targetLayer) != null;
    }
}

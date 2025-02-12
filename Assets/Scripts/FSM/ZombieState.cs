using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState : MonoBaseState
{
    public PlayerController controller;
    public BoxCollider2D boxCollider;
    public LayerMask targetLayer;

    public override void OnEnter()
    {
       // controller.animator.CrossFade("IDLE", 0.1f);
    }

    public override void OnExit(MonoBaseState nextState)
    {
     
    }

    public override void OnFixedUpdate()
    {
        controller.HorizontalMove();

        if (!IsOverlappingTargetLayer())
        {
            if (controller.fsm.GetCurrentState() == this)
                controller.fsm.SwitchState((int)ZombieStateName.FALL);
        }
    }

    public override void OnUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0f)
        {
            controller.animator.SetBool("IsRun", true);
        }
        else
        {
            controller.animator.SetBool("IsRun", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.fsm.SwitchState((int)ZombieStateName.JUMP);
        }

    }
    bool IsOverlappingTargetLayer()
    {
        return Physics2D.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.size, 0, targetLayer) != null;
    }
}


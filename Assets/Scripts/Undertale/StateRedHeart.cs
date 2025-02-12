using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Undertale
{
    public class StateRedHeart : MonoBaseState
    {
        public HeartController controller;
        public Sprite sprite;
        public float moveSpeed = 10;
        public Transform StartPos;
        public override void OnEnter()
        {
           controller.ChangeHeartSprite(sprite);
            controller.ChangeGravity(0);
        }

        public override void OnExit(MonoBaseState nextState)
        {
            if(nextState is StateBlueHeart)
            controller._animator.Play("red2blue");
        }

        public override void OnFixedUpdate()
        {
            RedHeartMove();
        }

        public override void OnUpdate()
        {
            
        }
        public void RedHeartMove()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            controller.rb.MovePosition(transform.position + new Vector3(horizontal, vertical, 0) * Time.deltaTime * moveSpeed);

        }
    }

}

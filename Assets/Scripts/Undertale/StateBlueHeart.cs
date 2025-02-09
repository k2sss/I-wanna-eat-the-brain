using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
namespace Undertale
{
    public class StateBlueHeart : MonoBaseState
    {
        public HeartController controller;
        public Sprite sprite;
        public float gravityScale = 3f;
        public float moveSpeed = 10f;
        public float jumpVelocity;
        public bool isGround;
        private float jumpTimer;
        public float extraGravity = 1f;
        
        public override void OnEnter()
        {
            controller.ChangeHeartSprite(sprite);
            controller.ChangeGravity(gravityScale);
        }

        public override void OnExit()
        {
            controller._animator.Play("blue2red");
        }

        public override void OnFixedUpdate()
        {   float horizontal = Input.GetAxisRaw("Horizontal");
            float Vertical = Input.GetAxisRaw("Vertical");
            // controller.rb.MovePosition(transform.position + new Vector3(horizontal, 0, 0) * Time.deltaTime * moveSpeed);
            controller.rb.velocity = new Vector2(horizontal * moveSpeed, controller.rb.velocity.y);
            if (!(Input.GetKey(KeyCode.Space)|| Vertical > 0f) && controller.rb.velocity.y>0f)
            {
                controller.rb.AddForce(Vector2.down * extraGravity);
            }
        }

        public override void OnUpdate()
        {
            jumpTimer-=Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                controller.fsm.SwitchState(0);
            }
            //jump
            float Vertical = Input.GetAxisRaw("Vertical");
            if (isGround &&  (Input.GetKeyDown(KeyCode.Space)|| Vertical > 0f) && jumpTimer <0f)
            {
                jumpTimer = 0.2f;
                controller.rb.velocity = new Vector2(controller.rb.velocity.x, jumpVelocity);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 3)
            {
                isGround = true;
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

}

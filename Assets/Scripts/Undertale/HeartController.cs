using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Undertale
{
    public class HeartController : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Rigidbody2D rb;
        public float moveSpeed = 3;
        public MonoFSM fsm;
        public Animator _animator;
        
        private void Start()
        {
            
            fsm.InitDefaultState(2);

            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            fsm.Update();
            //Test
            if (Input.GetKeyUp(KeyCode.R))
            {
                fsm.SwitchState(3);
            }
        }
        private void FixedUpdate()
        {
            fsm.FixedUpdate();
        }

        public void PlaySound(AudioClip clip)
        {
            SoundManager.Instance.PlaySound(clip);
        }
        public void ChangeHeartSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }
        public void ChangeGravity(float value)
        {
            rb.gravityScale = value;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Killer"))
            {
                fsm.SwitchState(3);
            }
        }
    }

}

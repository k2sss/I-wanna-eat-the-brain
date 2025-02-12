using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Undertale
{
    public class StatePurpleHeart : MonoBaseState
    {
        public HeartController controller;
        public Sprite sprite;
        public float moveSpeed = 10;
        public Transform StartPos;
        private int CurrentYOffset;
        public GameObject Line;
        private List<GameObject> lines = new();
        public float OriginY;
        public float YSpace;
        public override void OnEnter()
        {
            controller.ChangeHeartSprite(sprite);
            controller.ChangeGravity(0);
            InitLine();


        }

        public override void OnExit(MonoBaseState nextState)
        {
            DestroyLine();
        }

        public override void OnFixedUpdate()
        {
            PurpleHeartMove();
        }

        public override void OnUpdate()
        {
            int offset = VerticalInput();
            CurrentYOffset = Mathf.Clamp(CurrentYOffset + offset, -1, 1);


        }
        public int VerticalInput()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                return 1;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        public void PurpleHeartMove()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            
            Vector3 newPos = new Vector3(transform.position.x + horizontal * Time.deltaTime * moveSpeed, OriginY + YSpace * CurrentYOffset, 0);
            controller.rb.MovePosition(newPos);
        }
        public void InitLine()
        {
            for (int i = -1; i <= 1; i++)
            {
                GameObject go = Instantiate(Line);
                go.transform.position = new Vector3(go.transform.position.x, OriginY + YSpace * i, 0);
                lines.Add(go);
            }
        }
        public void DestroyLine()
        {
            foreach (GameObject go in lines)
            {
                Destroy(go);
            }
            lines.Clear();
        }
       
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Undertale;
using System;
using Unity.VisualScripting;
using System.Net.Sockets;
namespace Undertale
{
    public class StateMenu : MonoBaseState
    {
        private HeartController controller;
        private UTGameManager gameManager;
        
       
        private void Start()
        {
            controller = UTGameManager.Instance.GetPlayer();
            gameManager = UTGameManager.Instance;
        }
        public override void OnEnter()
        {
            controller.rb.isKinematic = true;
            controller.rb.velocity = Vector3.zero;
            gameManager.OnBreakTimeStart();
        }

        public override void OnExit(MonoBaseState nextState)
        {
            controller.rb.isKinematic = false;
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnUpdate()
        {
            
          
            if (Input.GetKeyDown(KeyCode.Z))
            {
                gameManager.Confirm();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                gameManager.Back();
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameManager.SwitchButton(1);
                gameManager.SwitchItem(1, false);
            }
            else
             if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameManager.SwitchButton(-1);
                gameManager.SwitchItem(-1, false);

            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                
                gameManager.SwitchItem(-1, true);

            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                gameManager.SwitchItem(1, true);

            }
        }
        
    }
    public class SelectionInfo
    {
        public string targetName;
        public Action OnSelect;


    }
}


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
        public UndertaleButton[] buttons;
        private int currentButtonIndex;
        public TextDisplay menuText;
        public UTselecitonMgr seletionMgr;
        public bool isSelectMode;
        
        private string currentMenuContent;
        public FightUI fightUI;
        private bool isWaiting;
        private void Start()
        {
            controller = UTGameManager.Instance.GetPlayer();
           
        }
        public override void OnEnter()
        {
            isWaiting = false;
            isSelectMode = false;
            currentButtonIndex = 0;
            buttons[currentButtonIndex].Enable();
            controller.rb.isKinematic = true;
            controller.rb.velocity = Vector3.zero;
            ShowText("我想吃掉大脑...");
        }

        public override void OnExit()
        {
            controller.rb.isKinematic = false;
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnUpdate()
        {
            if (isWaiting == true) return;

            SwitchButton();
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (isSelectMode)
                {
                    seletionMgr.InvokeCurrentSelect();
                }
                else
                {
                    HandleButton();
                }
            }

            if (Input.GetKeyDown(KeyCode.X) && isSelectMode)
            {
                seletionMgr.QuitSelect();
                isSelectMode = false;
                ShowText(currentMenuContent);
                buttons[currentButtonIndex].Enable();
            }
        }
        public void SwitchButton()
        {
            

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (isSelectMode)
                {
                    seletionMgr.SelectHorizontal(1);
                }
                else
                {
                    buttons[currentButtonIndex].Disable();
                    currentButtonIndex = (currentButtonIndex + 1) % buttons.Length;
                    buttons[currentButtonIndex].Enable();
                }

            }
            else
             if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (isSelectMode)
                {
                    seletionMgr.SelectHorizontal(-1);
                }
                else
                {
                    buttons[currentButtonIndex].Disable();
                    if (currentButtonIndex - 1 < 0) currentButtonIndex = buttons.Length - 1;
                    else
                        currentButtonIndex = (currentButtonIndex - 1);
                    buttons[currentButtonIndex].Enable();
                }

            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (isSelectMode)
                {
                    seletionMgr.SelectVertical(-1);
                }

            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (isSelectMode)
                {
                    seletionMgr.SelectVertical(1);
                }

            }

        }
        public void HandleButton()
        {
            buttons[currentButtonIndex].Handle();
        }
        public void ShowText(string text)
        {
            if (isSelectMode)
            {
                seletionMgr.QuitSelect();
            }

            currentMenuContent = text;
            menuText.Display($"* {text}", 0.06f);
        }
        public void EnterSelectionMode(List<SelectionInfo> infos)
        {

            isSelectMode = true;
            menuText.DisplayAll("");
            seletionMgr.Set(infos);
            seletionMgr.Select(0);
        }
        public void EnterFight()
        {
            //ShowText("这是战斗界面");
            EnterSelectionMode(new List<SelectionInfo>()
            {
                new SelectionInfo(){targetName = "豌豆射手",OnSelect = ()=>{

                    fightUI.Init();
                    seletionMgr.QuitSelect();
                    isWaiting = true;
                } }
            });
        }
        public void EnterAct()
        {
            List<SelectionInfo> subSelects = new List<SelectionInfo>()
            {
                new SelectionInfo(){targetName = "肘击",OnSelect = ()=>{
                } },
                new SelectionInfo(){targetName = "求饶",OnSelect = ()=>{
                } }
            };

            EnterSelectionMode(new List<SelectionInfo>()
            {
                new SelectionInfo(){targetName = "豌豆射手",OnSelect = ()=>{
                    EnterSelectionMode(subSelects);
                } }
            });
        }
        public void EnterItem()
        {
            EnterSelectionMode(new List<SelectionInfo>()
            {
                new SelectionInfo(){targetName = "物品1",OnSelect = ()=>{ } },
                 new SelectionInfo(){targetName = "物品2",OnSelect = ()=>{ } },
                  new SelectionInfo(){targetName = "物品3",OnSelect = ()=>{ } },
            });
        }
        public void EnterMercy()
        {
            EnterSelectionMode(new List<SelectionInfo>()
            {
                new SelectionInfo(){targetName = "豌豆射手",OnSelect = ()=>{ } }
            });
        }


    }
    public class SelectionInfo
    {
        public string targetName;
        public Action OnSelect;


    }
}


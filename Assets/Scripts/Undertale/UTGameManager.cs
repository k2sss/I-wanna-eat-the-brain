using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.Rendering;
using UnityEngine;
namespace Undertale
{
    public class UTGameManager : BaseMonoManager<UTGameManager>
    {

        public bool isSelectMode { get; set; }//��ǰ����ѡ����󣨰�������Ŀ�����Ʒ��
        public bool isWaiting { get; set; }
        [Header("��ť�߼�")]
        public UndertaleButton[] buttons;
        private int currentButtonIndex;



        [Header("�غ��߼�")]
        public List<Round> rounds = new();
        private int CurrentRoundIndex;
        private string currentMenuContent;
        public bool isWin;
        [Header("��������")]
        public HeartController player;
        public Transform heartStartPos;
        public UTselecitonMgr seletionMgr;
        public TextDisplay menuText;
        public UTPeashooter peaShooter;
        public Trap HpTrap;
        public FightUI fightUI;

        
        public HeartController GetPlayer()
        {
            if (player == null) throw new System.Exception("��ȡ����Player");
            return player;
        }
        protected override void Awake()
        {
            base.Awake();
            InitUTButton();
        }
        public void InitUTButton()
        {
            buttons[0].OnHandle += EnterFight;
            buttons[1].OnHandle += EnterAct;
            buttons[2].OnHandle += EnterItem;
            buttons[3].OnHandle += ()=> { HpTrap.MoveY("0.5,-2"); };
        }
        public void OnBreakTimeStart()
        {
            isWaiting = false;
            isSelectMode = false;
            currentButtonIndex = 0;
            buttons[currentButtonIndex].Enable();
            ShowText("�㶹���ֵ�ס����ĵ�·");

        }
        public void EnterSelectionMode(List<SelectionInfo> infos)
        {

            isSelectMode = true;
            menuText.DisplayAll("");
            seletionMgr.Set(infos);
            seletionMgr.Select(0);
        }
        public void GoToNextRound()
        {
            menuText.DisplayAll("");
            if (CurrentRoundIndex >= rounds.Count) CurrentRoundIndex = 0;
            Round currentRound = rounds[CurrentRoundIndex];
            currentRound.Init();
            //�л����λ��
            player.fsm.SwitchState(0);
            player.transform.position = heartStartPos.position;

            //��ʼ�Ի����Ի���ɺ�ʼ����
            peaShooter.DisplayNext(currentRound.SpeakContent, 0, () =>
            {
                currentRound.StartAttack();
            });
            //���������������жԻ���Ȼ���л����»غ�
            currentRound.OnAttackFinish += () =>
            {
                if (player.fsm.GetCurrentState() is StateDeath) return;

                peaShooter.DisplayNext(currentRound.EndContent, 0, () =>
                {
                    player.fsm.SwitchState(2);
                    ShowText(currentRound.nextBreakTimeStr);
                });
            };



            CurrentRoundIndex++;

        }
        public void Confirm()
        {
            if (isWaiting) return;
            if (isSelectMode)
            {
                seletionMgr.InvokeCurrentSelect();
            }
            else
            {
                buttons[currentButtonIndex].Handle();
            }
        }
        public void Back()
        {
            if (isWaiting) return;
            if (isSelectMode)
            {
                seletionMgr.QuitSelect();
                isSelectMode = false;
                ShowText(currentMenuContent);
                buttons[currentButtonIndex].Enable();
            }
        }
        public void SwitchButton(int offset)
        {
            if (isWaiting) return;
            if (isSelectMode) return;

            buttons[currentButtonIndex].Disable();
            int nextIndex = currentButtonIndex + offset;
            if (nextIndex < 0)
            {
                nextIndex = 0;
            }
            else
            {
                nextIndex %= buttons.Length;
            }
            currentButtonIndex = nextIndex;
            buttons[currentButtonIndex].Enable();

        }
        public void SwitchItem(int offset, bool isVertical)
        {
            if (isWaiting) return;
            if (isSelectMode)
            {
                if (!isVertical)
                    seletionMgr.SelectHorizontal(offset);
                else
                    seletionMgr.SelectVertical(offset);
            }
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
        public void EnterFight()
        {

            EnterSelectionMode(new List<SelectionInfo>()
            {
                new SelectionInfo(){targetName = "�㶹����",OnSelect = ()=>{
                    fightUI.Init();
                    fightUI.AddFinishListener(()=>
                    {
                        if(!isWin)
                        GoToNextRound();
                    }
                    );
                    seletionMgr.QuitSelect();
                    isWaiting = true;
                } }
            });
        }
        public void EnterAct()
        {
            List<SelectionInfo> subSelects = new List<SelectionInfo>()
            {
               
                new SelectionInfo(){targetName = "����",OnSelect = ()=>{
                    ShowText("û��Ч��");
                    GameManager.Instance.Delay(1, GoToNextRound);
                } }
            };

            EnterSelectionMode(new List<SelectionInfo>()
            {
                new SelectionInfo(){targetName = "�㶹����",OnSelect = ()=>{
                    EnterSelectionMode(subSelects);
                } }
            });
        }
        public void EnterItem()
        {
            EnterSelectionMode(new List<SelectionInfo>()
            {
                new SelectionInfo(){targetName = "��ҩ",OnSelect = ()=>{

                  ShowText("��Ե��˶�ҩ������֪���Ƕ�ҩȴҪ����ȥ������һ�");
                  GameManager.Instance.Delay(3,()=>player.fsm.SwitchState(3));
                } },
                
            });
        }
      
    }

}

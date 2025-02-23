using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
namespace Undertale
{
    public class Round : MonoBehaviour
    {
        [Multiline]
        public List<string> SpeakContent = new();//�غϿ�ʼʱ˵�Ļ�
        [Multiline]
        public List<string> EndContent = new();//�غϽ���ʱ˵�Ļ�

        public string nextBreakTimeStr;

        public List<Attack> attackList = new();
        private int currentAttackIndex;
        public Action OnAttackFinish;
        private float timer;
        private bool isable;
        public float DelayTime;

        public virtual void Init()
        {
            timer = 0;
            currentAttackIndex = 0;
            isable = false;
            OnAttackFinish = null;

        }
        public virtual void StartAttack()
        {
            isable = true;
            attackList.Sort((a, b) => a.Time.CompareTo(b.Time));

        }
        private void Update()
        {
            if (isable)
            {
                if (attackList.Count == 0 || currentAttackIndex == attackList.Count)
                {
                    isable = false;
                    GameManager.Instance.Delay(2+ DelayTime, () => EndTheAttack());

                    return;
                }

                timer += Time.deltaTime;
                Attack a = attackList[currentAttackIndex];
                if (timer > a.Time)
                {
                    GameObject go = ObjectPool.Instance.GetObject(a.AttackPrefab);
                    go.transform.SetParent(transform, true);
                    go.transform.position = a.GeneratePos + transform.position;
                    if (a.direction != Vector3.zero)
                        go.transform.right = a.direction;
                    if (a is BulletAttack bulletA)
                    {
                        a.AttackPrefab.GetComponent<Bullet>().Set(bulletA.Speed,bulletA.bulletDir,bulletA.DisappearTime);
                    }


                    currentAttackIndex++;
                }
            }
        }
        public virtual void EndTheAttack()
        {
            CleanAttack();
            isable = false;
            OnAttackFinish?.Invoke();
        }
        public void CleanAttack()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject obj = transform.GetChild(i).gameObject;
                if (obj.activeSelf == true)
                {
                    ObjectPool.Instance.PushObject(obj);
                }
            }
        }

    }
    [System.Serializable]
    public class Attack
    {

        public float Time;//����ʱ��
        public Vector3 GeneratePos;
        public Vector3 direction;
        public GameObject AttackPrefab;
    }
    public class BulletAttack : Attack
    {
        public float Speed;
        public Vector2 bulletDir;
        public float DisappearTime;
    }
}

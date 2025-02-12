using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using TMPro.EditorUtilities;
namespace Undertale
{
    public class UTPeashooter : MonoBehaviour
    {
        public Blconsm blconsm;
        public AudioClip talkClip;
        public Image hp;
        public GameObject HpBar;
        public int Health;
        public int MaxHealth = 5;
        private bool isDead;
        private void Start()
        {
            Health = MaxHealth;
            blconsm.SetAudioClip(talkClip);
        }
        public void DisplayNext(List<string> contents, int index, Action OnSpeakOver)
        {
            if (index == contents.Count)
            {
                blconsm.Close();
                OnSpeakOver?.Invoke();
                return;
            }

            blconsm.Display(contents[index],
               () =>
               {
                 GameManager.Instance.Delay(1,()=>DisplayNext(contents, index + 1, OnSpeakOver));
               });
        }

        public void Hurt()
        {
            Health -= 1;
            hp.fillAmount = (float)Health / MaxHealth;
            HpBar.SetActive(true);
            GameManager.Instance.Delay(1, () => HpBar.SetActive(false));
            if (Health <= 0 &&!isDead)
            {
                Dead();
            }

            Vector2 originPos = transform.position;
            transform.DOShakePosition(1, 1).OnComplete(() => transform.position = originPos);
            SoundManager.Instance.PlaySound("Sounds/UT/Damage");
        }
        public void Dead()
        {
           
            gameObject.GetComponent<Animator>().Play("Dead");
            isDead = true;
            UTGameManager.Instance.isWin = true;
            GameManager.Instance.Delay(2,()=>GameManager.Instance.LoadSceneAsync("Level5After"));
            
        }
        public void PlaySound(AudioClip clip)
        {
            SoundManager.Instance.PlaySound(clip);
        }
        
    }

}

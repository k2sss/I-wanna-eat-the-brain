using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Undertale
{
    public class UTselection : MonoBehaviour
    {
        public Transform heartPos;
        public Text text;
       
        public Action OnSelect;
        private HeartController heartController;
        private Vector3 originPos;
        private AudioClip selectAudioclip;
        public void Init(string name)
        {
            heartController = UTGameManager.Instance.GetPlayer();
            text.text = name;
            selectAudioclip = Resources.Load<AudioClip>("Sounds/UT/select");
        }
        public void EnterSelect()
        {
            originPos = heartController.transform.position;
            heartController.transform.position = heartPos.transform.position;
        }
        public void QuitSelect()
        {
            if (heartController == null) return;
            heartController.transform.position = originPos;
            SoundManager.Instance.PlaySound(selectAudioclip);
        }
        public void Handle()
        {
            SoundManager.Instance.PlaySound(selectAudioclip);
            OnSelect?.Invoke();
        }

    }

}

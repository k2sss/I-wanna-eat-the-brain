using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
namespace Undertale
{
    public class UndertaleButton : MonoBehaviour
    {
        public Sprite deafultSprite;
        public Sprite selectedSprite;
        private SpriteRenderer spriteRenderer;
        public Transform heartPos;
        private HeartController heartController;
        private AudioClip selectAudioclip;
        public UnityEvent OnHandle;
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            heartController = UTGameManager.Instance.GetPlayer();
            selectAudioclip = Resources.Load<AudioClip>("Sounds/UT/select");
        }
        [ContextMenu("Enable")]
        public void Enable()
        {
           
            heartController.transform.position = heartPos.position;
            spriteRenderer.sprite = selectedSprite;
            SoundManager.Instance.PlaySound(selectAudioclip);
        }
        public void Disable()
        {

            spriteRenderer.sprite = deafultSprite;
        }
        public void Handle()
        {
            SoundManager.Instance.PlaySound(selectAudioclip);
            OnHandle?.Invoke();
        }
    }
}


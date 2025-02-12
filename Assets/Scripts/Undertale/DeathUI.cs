using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Undertale
{
    public class DeathUI : MonoBehaviour
    {
        public TextDisplay display;
        
        public void PlayMusic(AudioClip clip)
        {
            SoundManager.Instance.PlayMusic(clip);
        }
        public void StrtPrint()
        {
            display.Display("��ǰ�����ô��ô�˰�", 0.2f);
        }
       
    }

}

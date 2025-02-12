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
            display.Display("猪鼻吧这怎么这么菜啊", 0.2f);
        }
       
    }

}

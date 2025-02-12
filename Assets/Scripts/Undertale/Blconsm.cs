using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Undertale
{
    public class Blconsm : MonoBehaviour
    {
        public TextDisplay display;

        public void SetAudioClip(AudioClip clip)
        {
            display.clip = clip;
        }
        public void Close()
        {
            display.DisplayAll("");
            gameObject.SetActive(false);
        }
        public void Display(string content,Action OnDisplayOver)
        {
            gameObject.SetActive(true);
           
            display.Display(content, OnDisplayOver,0.06f);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7 : MonoBehaviour
{
    public PlayerController controller;
    public GameObject diangun;
    public void EnterSky()
    {
        SoundManager.Instance.musicAudioSource.mute = true;
        GameManager.Instance.Delay(5, () =>
        {
            //SoundManager.Instance.musicAudioSource.mute =false;
            controller.fsm.SwitchState(2);
        });
    }
    public void Diangun()
    {
        GameManager.Instance.Delay(6, () => {
            
            GameObject.Instantiate(diangun);
        });
      
    }
}

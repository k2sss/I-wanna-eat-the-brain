using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7 : MonoBehaviour
{
    public PlayerController controller;

    public void EnterSky()
    {
        SoundManager.Instance.musicAudioSource.mute = true;
        GameManager.Instance.Delay(5, () =>
        {
            //SoundManager.Instance.musicAudioSource.mute =false;
            controller.fsm.SwitchState(2);
        });
    }
}

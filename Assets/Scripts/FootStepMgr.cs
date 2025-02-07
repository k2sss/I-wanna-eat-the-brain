using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepMgr : MonoBehaviour
{
    public AudioClip[] footsteps;
    public float volume;
    public void PlayFootStep()
    {
        SoundManager.Instance.PlayRandomSound(footsteps, volume);
    }
}

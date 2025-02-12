using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UTEntry : MonoBehaviour
{
    private AsyncOperation operation;
    public void Enter()
    {
        gameObject.SetActive(true);
       operation = SceneManager.LoadSceneAsync("UndertaleScene");
        operation.allowSceneActivation = false;

    }
    public void AllowSceneActivation()
    {
        operation.allowSceneActivation = true;
    }
    public void PlaySound(AudioClip clip)
    {
        SoundManager.Instance.PlaySound(clip);
    }
}

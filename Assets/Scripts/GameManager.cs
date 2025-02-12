using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class GameManager : BaseMonoManager<GameManager>
{
    public UIManager UImgr;
    public bool isFadeIn = true;
    
    protected override void Awake()
    {
        base.Awake();
        UImgr.InitUI();

    }

    protected virtual void Start()
    {
        if(isFadeIn)
         UImgr.blackScreen.FadeOut(1);
    }
    public void LoadSceneAsync(string sceneName)
    {
        // �첽���س���������������
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false; // Ĭ������£�����������ɺ���Զ�����
        UImgr.blackScreen.FadeIn(1, ()=>asyncLoad.allowSceneActivation = true);

    }



    public void Delay(float time, Action action)
    {
        StartCoroutine(DelayCourtine(time, action));
    }
    private IEnumerator DelayCourtine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}

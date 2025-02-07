using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;
using static UnityEngine.Rendering.HDROutputUtils;

public class GameManager : BaseMonoManager<GameManager>
{
    public UIManager UImgr;
    protected override void Awake()
    {
        base.Awake();
        UImgr.InitUI();

    }

    private void Start()
    {
        UImgr.blackScreen.FadeOut(1);
    }
    public void LoadSceneAsync(string sceneName)
    {
        // 异步加载场景，不立即激活
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false; // 默认情况下，场景加载完成后会自动激活
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

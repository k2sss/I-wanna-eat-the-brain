using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
public class BlackScreen : MonoBehaviour
{
    public Image black;
    public Text text;
    public void FadeIn(float duration,Action OnCompleted)
    {
        black.color = new Color(0, 0, 0, 0);
        black.DOFade(1, duration).OnComplete(()=>OnCompleted?.Invoke());
        text.text = "";
    }
    public void FadeOut(float duration)
    {
        black.color = new Color(0, 0, 0, 1);
        black.DOFade(0, duration);
        text.color = new Color(1, 1, 1, 0);
        text.DOFade(1, duration);
        GameManager.Instance.Delay(duration + 0.5f, () => { text.DOFade(0, 0.4f); });
        text.text = SceneManager.GetActiveScene().name;
    }
}

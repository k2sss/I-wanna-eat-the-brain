using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    // public
    public Text text;
    public AudioClip clip;
    public float TimeInterval = 0.2f;

    /// <summary>
    /// 文字显示完成时触发的回调
    /// </summary>
    public System.Action OnDisplayComplete;
    public void Display(string str,Action OnDisplayComplete,float speed = 0.03f)
    {
        this.OnDisplayComplete = null;
        this.OnDisplayComplete += OnDisplayComplete;
       Display(str,speed);
    }
    /// <summary>
    /// 以打字机形式显示文字
    /// </summary>
    public void Display(string str, float speed = 0.03f)
    {
        nowDisplayCount = 0;
        nowDisplay = str;
        enabled = true;
        if (speed <= 0)
        {
            speed = 0;
        }
        this.speed = speed;
        timmer = 0;
        lastAudioTime = -TimeInterval;
        text.text = "";
        isDisplaying = true;
        richTag.Clear();
    }

    /// <summary>
    /// 直接显示所有文字
    /// </summary>
    public void DisplayAll(string str)
    {
        enabled = false;
        text.text = str;
        isDisplaying = false;
        
    }

    public bool isDisplaying { get; private set; } = false;

    // private
    private string nowDisplay;
    private int nowDisplayCount;
    private float speed;
    private float timmer;
    private float lastAudioTime;

    public List<(string start, string end)> richTag = new();

    private void Update()
    {
        timmer += Time.deltaTime;
        if (speed <= 0)
        {
            DisplayAll(nowDisplay);
            return;
        }
        while (timmer >= speed)
        {
            timmer -= speed;
            DisplayNext();
        }
    }

    private void DisplayNext()
    {
        nowDisplayCount += 1;
        while (nowDisplayCount <= nowDisplay.Length && nowDisplay[nowDisplayCount - 1] == '<') // 跳过富文本
        {
            nowDisplayCount = GetRichTag(nowDisplayCount - 1) + 1;
        }
        if (nowDisplayCount > nowDisplay.Length)
        {
            DisplayAll(nowDisplay);
            return;
        }
        string end = "";
        for (int i = richTag.Count - 1; i >= 0; i--)
        {
            end += richTag[i].end;
        }
        text.text = nowDisplay.Substring(0, nowDisplayCount) + end;

        // 播放打字机音效
        if (Time.time - lastAudioTime >= TimeInterval && clip != null)
        {
            SoundManager.Instance.PlaySound(clip, 1.0f);
            lastAudioTime = Time.time;
        }

        // 文字完全显示后触发回调
        if (nowDisplayCount >= nowDisplay.Length)
        {
            isDisplaying = false;
            OnDisplayComplete?.Invoke();
        }
    }

    private int GetRichTag(int count)
    {
        if (count + 1 < nowDisplay.Length && nowDisplay[count + 1] == '/')
        {
            while (count < nowDisplay.Length && nowDisplay[count] != '>')
            {
                count++;
            }
            if (count >= nowDisplay.Length)
            {
                Debug.LogError("Rich text error!");
            }
            if (richTag.Count > 0)
            {
                richTag.RemoveAt(richTag.Count - 1);
            }
            return count;
        }

        int start = count;
        int len = 1;
        while (count < nowDisplay.Length && nowDisplay[count] != '>')
        {
            len++;
            count++;
        }
        if (count >= nowDisplay.Length)
        {
            Debug.LogError("Rich text error!");
            return count;
        }
        else
        {
            string tag = nowDisplay.Substring(start, len);
            string tagEnd = $"</{tag.Substring(1)}";
            if (tag.Contains("="))
            {
                tagEnd = $"{tagEnd.Substring(0, tagEnd.IndexOf('='))}>";
            }
            richTag.Add(new(tag, tagEnd));
            return count + 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoCanvas : MonoBehaviour
{
    public Text text;
    public void Set(string target)
    {
        string content = $"�ؿ�:{SceneManager.GetActiveScene().name}\nĿ��:{target}";
        text.text = content;
    }
}

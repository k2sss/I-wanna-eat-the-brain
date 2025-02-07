using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public BlackScreen blackScreen;
    private InfoCanvas infoCanvas;
    public void InitUI()
    {
        GameObject infoUI = Instantiate(Resources.Load<GameObject>("Prefab/UI/InfoCanvas"));
        infoCanvas = infoUI.GetComponent<InfoCanvas>();
        infoCanvas.Set("³ÔµôÄÔ×Ó");
        GameObject blackScreenObj = Instantiate(Resources.Load<GameObject>("Prefab/UI/BlackScreen"));
        blackScreen = blackScreenObj.GetComponent<BlackScreen>();
    }
    
    
}

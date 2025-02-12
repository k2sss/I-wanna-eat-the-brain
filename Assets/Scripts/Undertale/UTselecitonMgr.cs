using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
namespace Undertale
{
    public class UTselecitonMgr : MonoBehaviour
    {

        public GameObject prefab;
        private List<UTselection> selections = new();
        private int currentSelect;

        public void Set(List<SelectionInfo> infos)
        {
            Clean();
            foreach (SelectionInfo info in infos)
            {
                GameObject go = Instantiate(prefab, transform);
                UTselection s = go.GetComponent<UTselection>();
                s.Init(info.targetName);
                s.OnSelect += info.OnSelect;
                selections.Add(s);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }
        public void Clean()
        {

            currentSelect = 0;
            selections.Clear();
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }

            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    DestroyImmediate(transform.GetChild(i).gameObject);
            //}
        }
        public void QuitSelect()
        {
            if (selections.Count == 0) return;
            selections[currentSelect].QuitSelect();
            Clean();
        }
        public void SelectVertical(int dir)
        {
            int next = currentSelect + 2 * dir;

            if (next < 0) next = 0;
            if (next >= selections.Count) next = selections.Count - 1;

            Select(next);
        }
        public void SelectHorizontal(int dir)
        {
            int next = currentSelect + 1 * dir;

            if (next < 0) next = 0;
            next %= selections.Count;

            Select(next);
        }
        public void Select(int index)
        {
            if (index < 0 || index >= selections.Count) return;
            selections[currentSelect].QuitSelect();
            currentSelect = index;
            selections[index].EnterSelect();
        }
        public void InvokeCurrentSelect()
        {
            if (currentSelect >= selections.Count) return;
            selections[currentSelect].Handle();
        }
    }

}

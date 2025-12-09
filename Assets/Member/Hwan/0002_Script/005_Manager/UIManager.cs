using System.Collections.Generic;
using UnityEngine;
using YGPacks;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<UIType, IUI> uiDictionary;
    public Dictionary<UIType, IUI> UIDictionary
    {
        get
        {
            if (uiDictionary== null)
            {
                uiDictionary = new();

                foreach (IUI ui in GetComponentsInChildren<IUI>(true))
                {
                    uiDictionary.Add(ui.UIType, ui);
                }
            }

            return uiDictionary;
        }
    }

    public void CloseAllUI()
    {
        foreach (IUI ui in uiDictionary.Values)
        {
            ui.Close();
        }
    }
}

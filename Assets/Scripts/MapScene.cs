using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScene : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;

    void Start()
    {
        MenuPanel.SetActive(false);
    }
    public void onClickMenu()
    {
        MenuPanel.SetActive(true);
        UIEffectsManager spriteEM = MenuPanel.GetComponent<UIEffectsManager>();
        spriteEM.Run("popUp");
    }

    public void onCloseMenu()
    {
        UIEffectsManager spriteEM = MenuPanel.GetComponent<UIEffectsManager>();
        spriteEM.Run("popDown");
    }

    public void setPanelMenu()
    {
        MenuPanel.SetActive(false);
    }
}
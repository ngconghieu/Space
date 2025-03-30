using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BtnAbstract : GameMonoBehaviour
{
    protected Button button;

    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
    }

    private void LoadButton()
    {
        if (button != null) return;
        button = GetComponent<Button>();
        //Debug.Log("LoadButton", gameObject);
    }

    protected abstract void OnClick();
}
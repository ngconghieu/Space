using System;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private Camera _cam;
    public Camera Camera => _cam;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCamera();
    }

    private void LoadCamera()
    {
        if (_cam != null) return;
        _cam = GetComponentInChildren<Camera>();
        Debug.Log("LoadCamera", gameObject);
    }
}

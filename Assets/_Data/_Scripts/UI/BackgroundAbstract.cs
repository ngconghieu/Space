using UnityEngine;

public abstract class BackgroundAbstract : GameMonoBehaviour
{
    [SerializeField] BackgroundManager _backgroundManager;
    public BackgroundManager BackgroundManager => _backgroundManager;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBackgroundManager();
    }

    private void LoadBackgroundManager()
    {
        if (_backgroundManager != null) return;
        _backgroundManager = GetComponentInParent<BackgroundManager>();
        Debug.Log("LoadBackgroundManager", gameObject);
    }
}
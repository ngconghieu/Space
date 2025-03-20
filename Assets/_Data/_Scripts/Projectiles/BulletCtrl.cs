using UnityEngine;
using UnityEngine.Events;

public class BulletCtrl : GameMonoBehaviour
{
    [SerializeField] private DespawnBullet _despawnBullet;
    public DespawnBullet DespawnBullet => _despawnBullet;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDespawnBullet();
        EventManager.EmitEvent("Bullet", this);
    }

    private void LoadDespawnBullet()
    {
        if (_despawnBullet != null) return;
        _despawnBullet = GetComponentInChildren<DespawnBullet>();
        Debug.Log("LoadDespawnBullet", gameObject);
    }
}

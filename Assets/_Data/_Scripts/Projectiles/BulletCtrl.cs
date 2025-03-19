using UnityEngine;

public class BulletCtrl : GameMonoBehaviour
{
    [SerializeField] private DespawnBullet _despawnBullet;

    public DespawnBullet DespawnBullet => _despawnBullet;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDespawnBullet();
    }

    private void LoadDespawnBullet()
    {
        if (_despawnBullet != null) return;
        _despawnBullet = GetComponentInChildren<DespawnBullet>();
        Debug.Log("LoadDespawnBullet", gameObject);
    }

}

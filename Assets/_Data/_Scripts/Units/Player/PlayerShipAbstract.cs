using UnityEngine;

public abstract class PlayerShipAbstract : GameMonoBehaviour
{
    [SerializeField] private PlayerShipCtrl _playerShipCtrl;
    public PlayerShipCtrl PlayerShipCtrl => _playerShipCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerShipCtrl();
    }

    private void LoadPlayerShipCtrl()
    {
        if (_playerShipCtrl != null) return;
        _playerShipCtrl = GetComponentInParent<PlayerShipCtrl>();
        Debug.Log("LoadPlayerShipCtrl", gameObject);
    }
}

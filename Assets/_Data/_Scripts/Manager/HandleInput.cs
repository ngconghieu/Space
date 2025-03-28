using UnityEngine.InputSystem;
using UnityEngine;

public class HandleInput : GameMonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        InputManager.Instance.OnInventoryToggle += SwitchActionMap;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerInput();
    }

    private void LoadPlayerInput()
    {
        if (_playerInput != null) return;
        _playerInput = GetComponent<PlayerInput>();
        Debug.Log("LoadPlayerInput", gameObject);
    }
    #endregion

    public void SwitchActionMap()
    {
        if (InputManager.Instance.InventoryToggle)
            _playerInput.SwitchCurrentActionMap(Const.UI.ToString());
        if (!InputManager.Instance.InventoryToggle)
            _playerInput.SwitchCurrentActionMap(Const.Player.ToString());
    }
}
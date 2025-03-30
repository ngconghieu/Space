using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [Header("InputPlayer")]
    [SerializeField] private bool _leftClick;
    [SerializeField] private bool _rightClick;
    [SerializeField] private Vector2 _look = Vector2.zero;
    [SerializeField] private bool _inventoryToggle;
    [SerializeField] private PlayerInput _playerInput;

    private InputActionMap _playerMap;
    private InputActionMap _uiMap;

    public bool LeftClick => _leftClick;
    public bool RightClick => _rightClick;
    public Vector2 Look => _look;
    public bool InventoryToggle => _inventoryToggle;

    public event Action HandleInventoryToggle;

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


    public void OnLeftClick(InputAction.CallbackContext context)
    {
        _leftClick = true;
        if (context.canceled)
            _leftClick = false;
    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        _rightClick = true;
        if (context.canceled)
            _rightClick = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 data = context.ReadValue<Vector2>();
        _look = Camera.main.ScreenToWorldPoint(data);
    }

    public void OnToggleInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_inventoryToggle)
            {
                _inventoryToggle = true;
                Time.timeScale = 0;
                Debug.Log("Open Inventory");
                _playerInput.SwitchCurrentActionMap(Const.UI.ToString());
            }
            else
            {
                _inventoryToggle = false;
                Time.timeScale = 1;
                Debug.Log("Close Inventory");
                _playerInput.SwitchCurrentActionMap(Const.Player.ToString());
            }
            HandleInventoryToggle?.Invoke();
        }
    }

    private void Start()
    {
        _playerMap = _playerInput.actions.FindActionMap(Const.Player.ToString());
        _uiMap = _playerInput.actions.FindActionMap(Const.UI.ToString());
        _playerMap.Enable();
        _uiMap.Disable();
    }
}

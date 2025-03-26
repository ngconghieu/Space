using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private PlayerInput _playerInput;
    
    [Header("InputPlayer")]
    [SerializeField] private bool _leftClick;
    [SerializeField] private bool _rightClick;
    [SerializeField] private Vector2 _look = Vector2.zero;

    public bool LeftClick => _leftClick;
    public bool RightClick => _rightClick;
    public Vector2 Look => _look;

    [Header("InputUI")]
    [SerializeField] private bool _openInventory;

    public bool OpenInventory => _openInventory;

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
        Vector2 test = context.ReadValue<Vector2>();
        _look = Camera.main.ScreenToWorldPoint(test);
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        _look = Vector2.zero;
        _openInventory = true;
        _playerInput.SwitchCurrentActionMap("UI");
    }

    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        _openInventory = false;
        _playerInput.SwitchCurrentActionMap("Player");
    }
}

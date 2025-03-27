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

    public bool LeftClick => _leftClick;
    public bool RightClick => _rightClick;
    public Vector2 Look => _look;
    public bool InventoryToggle => _inventoryToggle;

    public event Action OnInventoryToggle;

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
        _inventoryToggle = true;
        _look = Vector2.zero;
        OnInventoryToggle?.Invoke();
    }

    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        _inventoryToggle = false;
        OnInventoryToggle?.Invoke();
    }
}

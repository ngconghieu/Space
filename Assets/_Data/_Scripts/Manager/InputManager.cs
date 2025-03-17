using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private PlayerInput _playerInput;
    
    [Header("Input")]
    [SerializeField] private bool _leftClick;
    [SerializeField] private bool _rightClick;
    [SerializeField] private Vector2 _look = Vector2.zero;

    public bool isLeftClick => _leftClick;
    public bool isRightClick => _rightClick;
    public Vector2 Look => _look;

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
}

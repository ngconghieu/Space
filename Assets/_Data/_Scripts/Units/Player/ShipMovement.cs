using System;
using UnityEngine;

public class ShipMovement : GameMonoBehaviour
{
    [SerializeField] private float _MovementSpeed = 0.4f;
    [SerializeField] private float _RotationSpeed = 5f;
    [SerializeField] private bool _canMove = true;
    private Vector3 _mousePos;

    private void FixedUpdate()
    {
        SetMousePos();
        HandleShipMovement();
        HandleShipRotation();
    }

    private void SetMousePos()
    {
        _mousePos = InputManager.Instance.Look;
        _mousePos.z = 0;
    }

    private void HandleShipMovement()
    {
        if (!_canMove) return;
        transform.parent.position = Vector2.Lerp(
            transform.parent.position,
            _mousePos,
            _MovementSpeed * Time.fixedDeltaTime
        );
    }

    private void HandleShipRotation()
    {
        Vector3 targetDir = (_mousePos - transform.parent.position).normalized;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.parent.rotation = Quaternion.Lerp(
            transform.parent.rotation,
            rot,
            _RotationSpeed * Time.fixedDeltaTime
        );
    }
}

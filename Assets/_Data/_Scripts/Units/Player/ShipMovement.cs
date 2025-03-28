using System;
using UnityEngine;

public class ShipMovement : GameMonoBehaviour
{
    [SerializeField] private float _MovementSpeed = 0.4f;
    [SerializeField] private float _RotationSpeed = 5f;
    public bool CanMove = true;
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
    }

    private void HandleShipMovement()
    {
        if (!CanMove) return;
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

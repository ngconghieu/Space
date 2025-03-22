using System;
using UnityEngine;

public class RandomDownMovement : GameMonoBehaviour
{
    [SerializeField, Range(1, 5)] private float _maxRandomSpeed;
    [SerializeField, Range(0.1f, 2)] private float _maxRandomRotationSpeed;

    private void Start()
    {
        _maxRandomRotationSpeed = UnityEngine.Random.Range(0.1f, _maxRandomRotationSpeed);
        _maxRandomSpeed = UnityEngine.Random.Range(1, _maxRandomSpeed);
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        transform.parent.Rotate(0, 0, _maxRandomRotationSpeed);
    }

    private void HandleMovement()
    {
        transform.parent.Translate(_maxRandomSpeed * Time.fixedDeltaTime * Vector2.down, Space.World);
    }

    public float GetSpeed() => _maxRandomSpeed;
    public float GetRotation() => _maxRandomRotationSpeed;
}

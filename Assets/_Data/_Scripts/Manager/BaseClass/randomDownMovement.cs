using System;
using UnityEngine;

public class RandomDownMovement : GameMonoBehaviour
{
    [SerializeField, Range(1, 5)] protected float maxRandomSpeed;
    [SerializeField, Range(0.1f, 2)] protected float maxRandomRotationSpeed;

    protected virtual void Start()
    {
        maxRandomRotationSpeed = UnityEngine.Random.Range(0.1f, maxRandomRotationSpeed);
        maxRandomSpeed = UnityEngine.Random.Range(1, maxRandomSpeed);
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        transform.parent.Rotate(0, 0, maxRandomRotationSpeed);
    }

    private void HandleMovement()
    {
        transform.parent.Translate(maxRandomSpeed * Time.fixedDeltaTime * Vector2.down, Space.World);
    }

    public void GetSpeedAndRotation(out float speed, out float rotation)
    {
        speed = maxRandomSpeed;
        rotation = maxRandomRotationSpeed;
    }
}

using System;
using UnityEngine;

public class BulletMovement : GameMonoBehaviour
{
    [SerializeField] private float _speed = 15;

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        transform.parent.Translate(_speed * Time.fixedDeltaTime * Vector2.up);
    }
}
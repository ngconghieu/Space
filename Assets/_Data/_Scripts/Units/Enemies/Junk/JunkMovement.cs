using System;
using UnityEngine;

public class JunkMovement : GameMonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _rotationSpeed = .1f;
    [SerializeField] private float _delayRandomRotate = 6;
    private float timer = 0;
    private float _randomRotation = 0;

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= _delayRandomRotate)
        {
            _randomRotation = UnityEngine.Random.Range(0, 360);
            timer = 0;
        }
        transform.parent.rotation = Quaternion.Lerp(
            transform.parent.rotation,
            Quaternion.Euler(0, 0, _randomRotation),
            _rotationSpeed
        );
    }

    private void HandleMovement()
    {
        transform.parent.Translate(_speed * Time.fixedDeltaTime * Vector2.down);
    }
}

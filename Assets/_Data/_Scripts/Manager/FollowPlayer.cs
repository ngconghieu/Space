using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 2f;

    private void FixedUpdate()
    {
        if (_player == null) return;
        transform.position = Vector3.Lerp(transform.position,
            _player.position,
            Time.fixedDeltaTime * _speed
        );
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundManager : GameMonoBehaviour
{
    [SerializeField] private SpriteRenderer _defaultBackground;
    [SerializeField] private Dictionary<SpriteRenderer, Vector2> _backgrounds = new();
    [SerializeField] private Vector2 _bound = new(25, 14);
    [SerializeField] private Vector2 _position = new(96, 54);
    protected Vector2 _camPos;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDefaultBackgrounds();
        _camPos = CameraManager.Instance.Camera.transform.position;
    }

    private void LoadDefaultBackgrounds()
    {
        if (_defaultBackground != null) return;
        _defaultBackground = GetComponentInChildren<SpriteRenderer>();
        _backgrounds.Add(_defaultBackground, _defaultBackground.transform.position);
    }
    #endregion
    //x26 y15 u54 r96

    private void FixedUpdate()
    {
        CheckAndSpawnBackgrounds();
    }

    private void CheckAndSpawnBackgrounds()
    {
        Vector2 currentBgPos = GetCurrentBackgroundPosition(_camPos);

        // calculate half width and height of the screen
        float halfWidth = _position.x / 2;
        float halfHeight = _position.y / 2;

        // check direction and spawn background if needed
        CheckDirection(currentBgPos, _camPos, new Vector2(_position.x, 0), halfWidth - _bound.x, true); // r
        CheckDirection(currentBgPos, _camPos, new Vector2(-_position.x, 0), halfWidth - _bound.x, true); // l
        CheckDirection(currentBgPos, _camPos, new Vector2(0, _position.y), halfHeight - _bound.y, false); // u
        CheckDirection(currentBgPos, _camPos, new Vector2(0, -_position.y), halfHeight - _bound.y, false); // d
    }

    // determine the current background position
    private Vector2 GetCurrentBackgroundPosition(Vector2 cameraPosition)
    {
        int xGrid = Mathf.FloorToInt((cameraPosition.x + _position.x / 2) / _position.x);
        int yGrid = Mathf.FloorToInt((cameraPosition.y + _position.y / 2) / _position.y);
        return new Vector2(xGrid * _position.x, yGrid * _position.y);
    }

    // check direction and spawn background
    private void CheckDirection(Vector2 currentBgPos, Vector2 cameraPos, Vector2 direction, float offset, bool isHorizontal)
    {
        float cameraDistance = isHorizontal ? Mathf.Abs(cameraPos.x - currentBgPos.x) : Mathf.Abs(cameraPos.y - currentBgPos.y);
        if (cameraDistance > offset)
        {
            Vector2 newBgPos = currentBgPos + direction;
            //if (!_backgrounds.ContainsKey(newBgPos))
            //{
            //    SpawnBackground(newBgPos);
            //}
        }
    }

    // Spawn Background and flip
    private void SpawnBackground(Vector2 position)
    {
        //GameObject newBg = Instantiate(_defaultBackground, position, Quaternion.identity);
        //_backgrounds.Add(position, newBg);

        //// Flip Background
        //int xCount = (int)(position.x / _position.x);
        //if (xCount % 2 != 0)
        //{
        //    SpriteRenderer sr = newBg.GetComponent<SpriteRenderer>();
        //    sr.flipX = true;
        //}
    }
}

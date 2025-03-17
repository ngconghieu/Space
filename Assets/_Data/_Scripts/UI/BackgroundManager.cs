using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundManager : GameMonoBehaviour
{
    [SerializeField] private SpriteRenderer _defaultBackground;
    [SerializeField] private Dictionary<SpriteRenderer, Vector2> _backgrounds = new();
    [SerializeField] private Vector2 _position = new(96, 54);
    private Vector2 _camPos;
    private Vector2 _currentBG;


    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDefaultBackgrounds();
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
        _camPos = CameraManager.Instance.Camera.transform.position;
        HandleSpawnBackground();
    }

    private void HandleSpawnBackground()
    {
        _currentBG = GetCurrentBackground(_currentBG);

        bool isOne = _camPos.x < _currentBG.x && _camPos.y < _currentBG.y;
        bool isTwo = _camPos.x < _currentBG.x && _camPos.y > _currentBG.y;
        bool isThree = _camPos.x > _currentBG.x && _camPos.y > _currentBG.y;
        bool isFour = _camPos.x > _currentBG.x && _camPos.y < _currentBG.y;
        if (isOne)
        {
            SpawnBackground(new Vector2(_currentBG.x - _position.x, _currentBG.y));
            SpawnBackground(new Vector2(_currentBG.x, _currentBG.y - _position.y));
            SpawnBackground(new Vector2(_currentBG.x - _position.x, _currentBG.y - _position.y));
        }

        if (isTwo)
        {
            SpawnBackground(new Vector2(_currentBG.x - _position.x, _currentBG.y));
            SpawnBackground(new Vector2(_currentBG.x, _currentBG.y + _position.y));
            SpawnBackground(new Vector2(_currentBG.x - _position.x, _currentBG.y + _position.y));
        }

        if (isThree)
        {
            SpawnBackground(new Vector2(_currentBG.x + _position.x, _currentBG.y));
            SpawnBackground(new Vector2(_currentBG.x, _currentBG.y + _position.y));
            SpawnBackground(new Vector2(_currentBG.x + _position.x, _currentBG.y + _position.y));
        }

        if (isFour)
        {
            SpawnBackground(new Vector2(_currentBG.x + _position.x, _currentBG.y));
            SpawnBackground(new Vector2(_currentBG.x, _currentBG.y - _position.y));
            SpawnBackground(new Vector2(_currentBG.x + _position.x, _currentBG.y - _position.y));
        }
    }

    private void SpawnBackground(Vector2 currentPos)
    {
        if(_backgrounds.ContainsValue(currentPos)) return;
        SpriteRenderer background = Instantiate(_defaultBackground, currentPos, Quaternion.identity);
        
        background.flipX = Mathf.Abs(currentPos.x) / _position.x % 2 == 1; ;
        background.flipY = Mathf.Abs(currentPos.y) / _position.y % 2 == 1; ;
        background.transform.SetParent(transform);
        _backgrounds.Add(background, currentPos);
    }

    private Vector2 GetCurrentBackground(Vector2 currentBg)
    {
        //int x = Mathf.FloorToInt((_camPos + currentBg));
        int y = Mathf.FloorToInt(Mathf.Abs(_camPos.y) / _position.y);
        return new Vector2(x * _position.x, y * _position.y);
    }
}
/*
    -96, 0 | -127, 15 (isTwo)
    x = -1
    y = 
    halfX = 96/2 = 48
    halfY = 54/2 = 27
    -96 - 127 = -223 / 96 = -2.3
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundManager : GameMonoBehaviour
{
    [SerializeField] private SpriteRenderer _defaultBackground;
    [SerializeField] private Dictionary<SpriteRenderer, Vector2> _backgrounds = new();
    [SerializeField] private Vector2 _spritePosition = new(96, 54);
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

        bool isThirdQuadrant = _camPos.x < _currentBG.x && _camPos.y < _currentBG.y;
        bool isSecondQuadrant = _camPos.x < _currentBG.x && _camPos.y > _currentBG.y;
        bool isFirstQuadrant = _camPos.x > _currentBG.x && _camPos.y > _currentBG.y;
        bool isFourthQuadrant = _camPos.x > _currentBG.x && _camPos.y < _currentBG.y;
        if (isThirdQuadrant)
        {
            SpawnBackground(new Vector2(_currentBG.x - _spritePosition.x, _currentBG.y));
            SpawnBackground(new Vector2(_currentBG.x, _currentBG.y - _spritePosition.y));
            SpawnBackground(new Vector2(_currentBG.x - _spritePosition.x, _currentBG.y - _spritePosition.y));
        }

        if (isSecondQuadrant)
        {
            SpawnBackground(new Vector2(_currentBG.x - _spritePosition.x, _currentBG.y));
            SpawnBackground(new Vector2(_currentBG.x, _currentBG.y + _spritePosition.y));
            SpawnBackground(new Vector2(_currentBG.x - _spritePosition.x, _currentBG.y + _spritePosition.y));
        }

        if (isFirstQuadrant)
        {
            SpawnBackground(new Vector2(_currentBG.x + _spritePosition.x, _currentBG.y));
            SpawnBackground(new Vector2(_currentBG.x, _currentBG.y + _spritePosition.y));
            SpawnBackground(new Vector2(_currentBG.x + _spritePosition.x, _currentBG.y + _spritePosition.y));
        }

        if (isFourthQuadrant)
        {
            SpawnBackground(new Vector2(_currentBG.x + _spritePosition.x, _currentBG.y));
            SpawnBackground(new Vector2(_currentBG.x, _currentBG.y - _spritePosition.y));
            SpawnBackground(new Vector2(_currentBG.x + _spritePosition.x, _currentBG.y - _spritePosition.y));
        }
    }

    private void SpawnBackground(Vector2 currentPos)
    {
        if (_backgrounds.ContainsValue(currentPos)) return;
        SpriteRenderer background = Instantiate(_defaultBackground, currentPos, Quaternion.identity, transform);
        background.flipX = Mathf.Abs(currentPos.x) / _spritePosition.x % 2 == 1;
        background.flipY = Mathf.Abs(currentPos.y) / _spritePosition.y % 2 == 1;
        _backgrounds.Add(background, currentPos);
    }

    private Vector2 GetCurrentBackground(Vector2 currentBg)
    {
        int x = (int)(Mathf.RoundToInt(_camPos.x / _spritePosition.x));
        int y = (int)(Mathf.RoundToInt(_camPos.y / _spritePosition.y));
        return new Vector2(x * _spritePosition.x, y * _spritePosition.y);
    }
}

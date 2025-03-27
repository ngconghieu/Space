using UnityEngine;

public class GameMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
        SetValues();
    }

    protected virtual void LoadComponents()
    {
        
    }

    protected virtual void SetValues()
    {

    }
}

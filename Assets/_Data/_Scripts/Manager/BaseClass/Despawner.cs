using System;
using UnityEngine;

public abstract class Despawner<T> : GameMonoBehaviour where T : GameMonoBehaviour
{
    protected T ctrl;
    public event Action<T> OnDespawn;

    public virtual void Despawn()
    {
        OnDespawn?.Invoke(ctrl);
    }

    public abstract void Initialize(T ctrl);

}
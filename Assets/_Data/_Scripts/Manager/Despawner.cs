using UnityEngine;

public abstract class Despawner<T> : GameMonoBehaviour where T : GameMonoBehaviour
{

    public virtual void Despawn(T prefab)
    {
        Spawner<T>.Instance.Despawn(prefab);
    }

    protected abstract void OnEnable();
}
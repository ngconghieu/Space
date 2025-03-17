using UnityEngine;

public class Despawner<T> : GameMonoBehaviour where T : GameMonoBehaviour
{
    public virtual void Despawn(T prefab)
    {
        Spawner<T>.Instance.Despawn(prefab);
    }
}
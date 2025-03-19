using UnityEngine;

public abstract class Despawner<T> : GameMonoBehaviour where T : GameMonoBehaviour
{

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCtrl();
    }

    protected abstract void LoadCtrl();
    #endregion

    public virtual void Despawn(T prefab)
    {
        Spawner<T>.Instance.Despawn(prefab);
    }

    protected abstract void OnEnable();
}
using System.Collections;
using UnityEngine;

public class DespawnEffect : Despawner<EffectCtrl>
{
    [SerializeField] private float _despawnTime = 2f;

    public override void Initialize(EffectCtrl ctrl)
    {
        this.ctrl = ctrl;
        StartCoroutine(HandleDespawn());
    }

    private IEnumerator HandleDespawn()
    {
        yield return new WaitForSeconds(_despawnTime);
        Despawn();
    }
}

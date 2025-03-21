using UnityEngine;

public class EffectCtrl : GameMonoBehaviour
{
    [SerializeField] private DespawnEffect _despawnEffect;
    public DespawnEffect DespawnEffect => _despawnEffect;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _despawnEffect.Initialize(this);

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDespawnEffect();
    }

    private void LoadDespawnEffect()
    {
        if (_despawnEffect != null) return;
        _despawnEffect = GetComponentInChildren<DespawnEffect>();
        Debug.Log("LoadDespawnEffect", gameObject);
    }
}
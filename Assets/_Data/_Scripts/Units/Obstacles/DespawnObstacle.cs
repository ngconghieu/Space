using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DespawnObstacle : Despawner<ObstacleCtrl>
{
    [SerializeField] private List<ItemName> _itemList;

    public override void Initialize(ObstacleCtrl ctrl)
    {
        this.ctrl = ctrl;
    }

    public override void Despawn()
    {
        DropItem();
        SpawnEffect();
        base.Despawn();
    }

    private void DropItem()
    {
        List<ObstacleDropItem> itemList = ctrl.ObstacleProfiles.DropList.OrderBy(item => item.DropRate).ToList();
        float rand = Random.Range(itemList[0].DropRate, itemList[itemList.Count - 1].DropRate);
        ObstacleDropItem dropItem = itemList.Find(item => item.DropRate > rand);
        ctrl.ItemManager.Spawn(
            ctrl.ItemManager.GetPrefab(dropItem.PrefabName), 
            ctrl.transform.position, 
            Quaternion.identity
        );

    }

    private void SpawnEffect()
    {
        EffectCtrl effectCtrl = ctrl.EffectManager.GetPrefab(PrefabName.Smoke_ObstacleDetroy);
        ctrl.EffectManager.Spawn(effectCtrl, ctrl.transform.position, Quaternion.identity);
    }
}

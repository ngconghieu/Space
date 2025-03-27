using System.Collections.Generic;
using UnityEngine;

public class DespawnObstacle : Despawner<ObstacleCtrl>
{
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
        List<ObstacleDropItem> itemList = ctrl.ObstacleProfiles.DropList;
        float rand = Random.Range(0.01f, 1);
        // binary search
        int index = FinDropItem(itemList, rand);
        ObstacleDropItem dropItem = itemList[index];

        var item = ctrl.ItemManager.Spawn(
            ctrl.ItemManager.GetPrefab(dropItem.PrefabName),
            ctrl.transform.position,
            Quaternion.identity
        );
        ctrl.RandomDownMovement.GetSpeedAndRotation(out float speed, out float rotation);
        item.ItemMovement.SetSpeedAndRotation(speed, rotation);
    }

    private int FinDropItem(List<ObstacleDropItem> itemList, float rand)
    {
        int l = 0, r = itemList.Count;
        int result = -1;
        while (l <= r)
        {
            int m = (l + r) / 2;
            if (itemList[m].DropRate >= rand)
            {
                result = m;
                r = m - 1;
            }
            else
                l = m + 1;
        }
        return result;
    }

    private void SpawnEffect()
    {
        EffectCtrl effectCtrl = ctrl.EffectManager.GetPrefab(Const.Smoke_ObstacleDetroy);
        ctrl.EffectManager.Spawn(effectCtrl, ctrl.transform.position, Quaternion.identity);
    }
}

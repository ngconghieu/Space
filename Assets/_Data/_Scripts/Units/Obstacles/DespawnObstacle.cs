using UnityEngine;

public class DespawnObstacle : Despawner<ObstacleCtrl>
{
    public override void Initialize(ObstacleCtrl ctrl)
    {
        this.ctrl = ctrl;
    }

}

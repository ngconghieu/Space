using System.Collections;
using UnityEngine;

public class ObstacleManager : Spawner<ObstacleCtrl>
{
    [SerializeField] private float deplaySpawn = 1f;
    [SerializeField] private int _maxSpawnObject = 100;
    private WaitForSeconds _wait;

    protected override void SubcribeEvent(ObstacleCtrl prefab)
    {
        prefab.DespawnObstacle.OnDespawn += Despawn;
    }

    private void Start()
    {
        _wait = new WaitForSeconds(deplaySpawn);
        SetMaxSpawnObject(_maxSpawnObject);
    }

    public void SetMaxSpawnObject(int maxSpawnObject)
    {
        _maxSpawnObject = maxSpawnObject;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (_maxSpawnObject-->0)
        {
            Vector3 camPos = CameraManager.Instance.Camera.transform.position;
            float RandomX = Random.Range(camPos.x - 9, camPos.x + 9);
            Vector2 spawnPos = new(RandomX, camPos.y + 13);
            ObstacleCtrl obstacle = GetPrefab("Obstacle_0");
            Spawn(obstacle, spawnPos, Quaternion.identity);
            yield return _wait;
        }
    }
}

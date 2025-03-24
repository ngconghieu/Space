using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 3f;

    private void FixedUpdate()
    {
        if (_player == null) return;
        transform.position = Vector3.Lerp(transform.position,
            _player.position,
            Time.fixedDeltaTime * _speed
        );
    }

    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        //while (true)
        {
            yield return new WaitForSeconds(1f);
            InventoryManager.Instance.AddItem(ItemName.CopperOre, 26);
            InventoryManager.Instance.AddItem(ItemName.CopperOre, 26);
            InventoryManager.Instance.AddItem(ItemName.CopperOre, 26);
            InventoryManager.Instance.AddItem(ItemName.CopperOre, 26);
            InventoryManager.Instance.AddItem(ItemName.CopperOre, 26);
            InventoryManager.Instance.AddItem(ItemName.CopperOre, 26);
            InventoryManager.Instance.AddItem(ItemName.CopperOre, 26);
            InventoryManager.Instance.AddItem(ItemName.CopperOre, 26);
            InventoryManager.Instance.AddItem(ItemName.CopperOre, 26);
        }
    }
}

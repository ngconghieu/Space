using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleProfiles", menuName = "ScriptableObjects/ObstacleProfiles", order = 1)]
public class ObstacleProfiles : ScriptableObject
{
    public List<ObstacleDropItem> DropList = new();
}

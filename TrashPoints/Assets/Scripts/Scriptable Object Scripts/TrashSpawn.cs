using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashSpawn", menuName = "TrashPoints/Create new Trash Spawn")]
public class TrashSpawn : ScriptableObject
{
    public TrashItem trashItem;
    public double x;
    public double y;
    public double rotation;
    public double scale = 1;
    public int layerIndexInScene;
    public string hintText;
}

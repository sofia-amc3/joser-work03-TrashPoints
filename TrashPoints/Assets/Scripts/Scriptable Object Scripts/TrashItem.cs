using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashItem", menuName = "TrashPoints/Create new Trash Item")]
public class TrashItem : ScriptableObject
{
    public Sprite trashIcon;
    public Sprite trashInScene;
    public string trashName;
    public TrashType trashType;
}

public enum TrashType
{
    Organic,
    PaperCardboard,
    PlasticMetal,
    Glass,
    EWaste
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashTooltip : MonoBehaviour
{
    public GameObject tooltip;
    public GameObject iconPrefab;
    private LevelController levelController;


    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    public void TooltipOn()
    {
        List<TrashSpawn> trashSpawns = levelController.GetTrashSpawnsRemaining();
        foreach (var trash in trashSpawns)
        {
            GameObject prefab = Instantiate(iconPrefab, tooltip.transform, false);
            prefab.GetComponent<Image>().sprite = trash.trashItem.trashIcon;
        }
        tooltip.SetActive(true);
    }

    public void TooltipOff()
    {
        tooltip.SetActive(false);
        for (int i = 0; i < tooltip.transform.childCount; i++)
        {
            Destroy(tooltip.transform.GetChild(i).gameObject);
        }
    }
}

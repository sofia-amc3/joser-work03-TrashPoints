using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelButton : MonoBehaviour
{
    public GameObject border;
    private SelectLevelController controller;

    private void Start()
    {
        controller = FindObjectOfType<SelectLevelController>();
    }

    public void SelectLevel(int index)
    {
        border.SetActive(true);
        controller.SetSelectedLevelIndex(index);
    }

    public void DeselectLevel()
    {
        border.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelButton : MonoBehaviour
{
    private SelectLevelController controller;

    private void Start()
    {
        controller = FindObjectOfType<SelectLevelController>();
    }

    public void SelectLevel(int index)
    {
        controller.SetSelectedLevelIndex(index);
    }
}

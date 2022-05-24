using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRulesInLevel : MonoBehaviour
{
    public GameObject rulesMenu;

    public void OpenRules()
    {
        rulesMenu.SetActive(true);
    }

    public void CloseRules()
    {
        rulesMenu.SetActive(false);
    }
}

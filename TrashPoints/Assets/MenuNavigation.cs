using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        menusTravelled.Add(mainMenu);
    }

    private List<GameObject> menusTravelled = new List<GameObject>();

    public GameObject mainMenu;

    public void OpenMenu(GameObject menu)
    {
        GameObject menuToClose = null;
        if (menusTravelled.Count > 0)
        {
            menuToClose = menusTravelled[menusTravelled.Count - 1];
        }
        menusTravelled.Add(menu);
        menusTravelled[menusTravelled.Count - 1].SetActive(true);
        if (menusTravelled.Count > 0)
        {
            menuToClose.SetActive(false);
        }
    }

    public void Back()
    {
        if (menusTravelled.Count > 1)
        {
            GameObject menuToClose = menusTravelled[menusTravelled.Count - 1];
            menusTravelled.RemoveAt(menusTravelled.Count - 1);
            menusTravelled[menusTravelled.Count - 1].SetActive(true);
            menuToClose.SetActive(false);
        }
    }
    
}

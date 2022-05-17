using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupControl : MonoBehaviour
{
    public void OpenPopup(GameObject popup)
    {
        popup.SetActive(true);
    }

    public void ClosePopup(GameObject popup)
    {
        popup.SetActive(false);
    }
}

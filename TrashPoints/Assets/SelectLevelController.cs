using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelController : MonoBehaviour
{
    private int selectedLevelIndex;
    public List<GameObject> highlights;

    public void SetSelectedLevelIndex(int index)
    {
        selectedLevelIndex = index;
        DeselectOthers(index);
    }

    public void PlayLevel()
    {
        switch (selectedLevelIndex)
        {
            case 0:
                SceneManager.LoadScene("Level01");
                break;

            case 1:
                SceneManager.LoadScene("Level02");
                break;

            case 2:
                SceneManager.LoadScene("Level03");
                break;

            case 3:
                break;
        }
    }

    private void DeselectOthers(int indexException)
    {
        for (int i = 0; i < highlights.Count; i++)
        {
            if (i != indexException)
            {
                highlights[i].SetActive(false);
            }
        }
    }
}

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
        int index = selectedLevelIndex + 1;
        if (index < 10)
        {
            SceneManager.LoadScene("Level0" + index, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene("Level" + index, LoadSceneMode.Additive);
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

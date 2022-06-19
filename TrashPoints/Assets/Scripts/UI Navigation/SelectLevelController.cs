using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelController : MonoBehaviour
{
    private int selectedLevelIndex;
    public List<GameObject> highlights;
    public GameObject leftArrow;
    public GameObject rightArrow;
    private int pageIndex;
    private int maxPageIndex;
    private MenuLoadScores menuLoadScores;

    private void Start()
    {
        menuLoadScores = FindObjectOfType<MenuLoadScores>(true);
        maxPageIndex = Mathf.Max(Mathf.FloorToInt((Constants.NUMBER_OF_LEVELS - 1) / 4), 0);
        UpdateButtons();
    }

    public void SetSelectedLevelIndex(int index)
    {
        selectedLevelIndex = index;
        DeselectOthers(index);
    }

    public void GoToPage(int index)
    {
        pageIndex = index;
        UpdateButtons();
    }

    public void NextPage()
    {
        DeselectAll();
        pageIndex++;
        menuLoadScores.DisplayInfo(pageIndex);
        UpdateButtons();
    }

    public void PreviousPage()
    {
        DeselectAll();
        pageIndex--;
        menuLoadScores.DisplayInfo(pageIndex);
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        if (pageIndex == 0)
        {
            leftArrow.SetActive(false);
        }
        else
        {
            leftArrow.SetActive(true);
        }

        if (pageIndex == maxPageIndex)
        {
            rightArrow.SetActive(false);
        }
        else
        {
            rightArrow.SetActive(true);
        }
    }

    public void PlayLevel()
    {
        if (selectedLevelIndex < 4)
        {
            int index = selectedLevelIndex + 1 + pageIndex * 4;
            if (index < 10)
            {
                SceneManager.LoadScene("Level0" + index, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene("Level" + index, LoadSceneMode.Additive);
            }
        }
    }

    private void DeselectAll()
    {
        for (int i = 0; i < highlights.Count; i++)
        {
            highlights[i].SetActive(false);
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
            else
            {
                highlights[i].SetActive(true);
            }
        }
    }
}

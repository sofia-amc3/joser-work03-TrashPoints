using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [Header("Settings for User")]
    public int trashToFind = 0;
    public List<TrashSpawn> trashSpawns = new List<TrashSpawn>();
    public List<GameObject> layers = new List<GameObject>();
    public int bonusScoreTimeLimitSeconds = 0;
    public int levelSuccessScoreThreshold = 0;
    public int levelSaveIndex = 0;
    
    [Header("Fixed Settings")]
    public Text scoreTextPhase1;
    public Text trashCounterTextPhase1;
    public Text hintText;
    public Text timeRemainingText;
    public GameObject trashSpawnPrefab;

    public GameObject phase2;
    public Text scoreTextPhase2;
    public Text trashCounterTextPhase2;
    public Text feedbackText;
    public Text trashNameText;
    public Image trashImage;
    public GameObject trashPanel;
    public List<Button> trashBinButtons = new List<Button>();

    public Text winScoreText;
    public GameObject winScreen;
    public Text loseScoreText;
    public GameObject loseScreen;

    // Private variables
    private int score = 0;
    private int timeRemaining = 0;
    private int bonusTimeScoreObtained = 0;

    private List<TrashSpawn> trashSpawnsHints = new List<TrashSpawn>();

    private SaveScoreEndLevel saver;

    private Color32 correctColor = new Color32(85, 197, 149, 255);
    private Color32 wrongColor = new Color32(197, 85, 85, 255);

    private List<TrashItem> listTrashCollected = new List<TrashItem>();

    //------------------------------------------------------------------------------------------ GENERAL

    // Run when scene is loaded
    private void Start()
    {
        saver = FindObjectOfType<SaveScoreEndLevel>();
        timeRemaining = bonusScoreTimeLimitSeconds;
        trashCounterTextPhase1.text = "Trash Counter: 0/" + trashToFind;
        if (trashToFind < 1 || trashToFind > trashSpawns.Count)
        {
            Debug.LogError("Number of trash to find is invalid.");
        }

        foreach (var layer in layers)
        {
            foreach (var image in layer.GetComponentsInChildren<Image>())
            {
                image.alphaHitTestMinimumThreshold = 0.01f;
            }
        }

        List<TrashSpawn> spawnListCopy = new List<TrashSpawn>(trashSpawns);

        for (int i = 0; i < trashToFind; i++)
        {
            int randomNumber = Random.Range(0, spawnListCopy.Count);

            GenerateTrash(spawnListCopy[randomNumber]);

            spawnListCopy.RemoveAt(randomNumber);
        }

        StartCoroutine(TimeCountdown());
    }

    // Used to update score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreTextPhase1.text = "Score: " + score;
        scoreTextPhase2.text = "Score: " + score;
    }

    // Used to get score
    public int Score()
    {
        return score;
    }

    public List<TrashSpawn> GetTrashSpawnsRemaining()
    {
        return trashSpawnsHints;
    }

    public void LeaveLevel()
    {
        FindObjectOfType<MenuLoadScores>(true).LoadAndFixData();
        int index = levelSaveIndex + 1;
        if (index < 10)
        {
            SceneManager.UnloadSceneAsync("Level0" + index);
        }
        else
        {
            SceneManager.UnloadSceneAsync("Level" + index);
        }
    }

    //------------------------------------------------------------------------------------------ PHASE 1

    // Generates the amount of trash specified
    private void GenerateTrash(TrashSpawn trashSpawn)
    {
        GameObject prefab = Instantiate(trashSpawnPrefab, layers[trashSpawn.layerIndexInScene].transform, false);
        prefab.transform.localPosition = new Vector3((float)trashSpawn.x, (float)trashSpawn.y, 0);
        prefab.transform.Rotate(new Vector3(0, 0, (float)trashSpawn.rotation));
        prefab.transform.localScale = new Vector2((float)trashSpawn.scale, (float)trashSpawn.scale);
        //prefab.transform.position = new Vector3((float)trashSpawn.x, (float)trashSpawn.y, 0);
        //prefab.transform.SetParent(layers[trashSpawn.layerIndexInScene].transform, true);
        prefab.transform.SetAsFirstSibling();

        prefab.GetComponent<TrashInstanceScript>().Initialize(trashSpawn);

        trashSpawnsHints.Add(trashSpawn);
    }

    // Called upon clicking a piece of trash
    public void FoundTrash(TrashItem trashItem)
    {
        listTrashCollected.Add(trashItem);
        trashCounterTextPhase1.text = "Trash Counter: " + listTrashCollected.Count + "/" + trashToFind;
        trashSpawnsHints.RemoveAll(x => x.trashItem == trashItem);

        if (listTrashCollected.Count == trashToFind)
        {
            // Take player to stage 2
            bonusTimeScoreObtained = timeRemaining * Constants.BONUS_SCORE_FOR_SECOND_REMAINING;
            timeRemaining = 0;
            StartCoroutine(GoToPhase2());
        }
    }

    // Called when clicking something that is not trash
    public void WrongClick()
    {
        UpdateScore(Constants.SCORE_FOR_WRONG_CLICK);
    }

    // Counts down the timer
    IEnumerator TimeCountdown()
    {
        string seconds;
        if (timeRemaining % 60 < 10)
        {
            seconds = "0" + timeRemaining % 60;
        }
        else
        {
            seconds = (timeRemaining % 60).ToString();
        }

        timeRemainingText.text = "Bonus Score Time Remaining: " + timeRemaining / 60 + ":" + seconds;
        yield return new WaitForSeconds(1);
        timeRemaining--;

        if (timeRemaining > 0)
        {
            StartCoroutine(TimeCountdown());
        }
    }

    // Called when player clicks the hint button
    public void ShowHint()
    {
        int randomNumber = Random.Range(0, trashSpawnsHints.Count);

        hintText.text = "HINT: " + trashSpawnsHints[randomNumber].hintText;
        hintText.gameObject.SetActive(true);
    }

    //------------------------------------------------------------------------------------------ PHASE 2

    // Loads the second phase
    IEnumerator GoToPhase2()
    {
        yield return new WaitForSeconds(2);
        UpdateScore(bonusTimeScoreObtained);
        DisplayFirstItem();
        phase2.SetActive(true);
    }

    // Displays the first trash piece collected
    private void DisplayFirstItem()
    {
        trashCounterTextPhase2.text = "Trash Counter: " + listTrashCollected.Count + "/" + trashToFind;
        trashNameText.text = listTrashCollected.First().trashName;
        trashImage.sprite = listTrashCollected.First().trashIcon;
    }

    // Displays the next trash piece on the list
    private void DisplayNextItem()
    {
        listTrashCollected.RemoveAt(0);

        trashCounterTextPhase2.text = "Trash Counter: " + listTrashCollected.Count + "/" + trashToFind;

        if (listTrashCollected.Count > 0)
        {
            trashNameText.text = listTrashCollected.First().trashName;
            trashImage.sprite = listTrashCollected.First().trashIcon;
        }
        else // All trash has been recycled
        {
            StartCoroutine(GoToFinalScreen());
        }
    }

    // Called when a trash bin is clicked
    public void EvaluateTrash(int trashTypeIndex)
    {
        if (listTrashCollected.First().trashType == (TrashType) trashTypeIndex)
        {
            UpdateScore(Constants.SCORE_FOR_CORRECT_TRASH_SEPARATION);
            feedbackText.color = correctColor;
            feedbackText.text = "Correct!";
        }
        else
        {
            UpdateScore(Constants.SCORE_FOR_WRONG_TRASH_SEPARATION);
            feedbackText.color = wrongColor;

            switch (listTrashCollected.First().trashType)
            {
                case TrashType.Organic:
                    feedbackText.text = "Wrong! The " + listTrashCollected.First().trashName + " belongs in the Organic bin!";
                    break;

                case TrashType.PaperCardboard:
                    feedbackText.text = "Wrong! The " + listTrashCollected.First().trashName + " belongs in the Paper/Cardboard bin!";
                    break;

                case TrashType.PlasticMetal:
                    feedbackText.text = "Wrong! The " + listTrashCollected.First().trashName + " belongs in the Plastic/Metal bin!";
                    break;

                case TrashType.Glass:
                    feedbackText.text = "Wrong! The " + listTrashCollected.First().trashName + " belongs in the Glass bin!";
                    break;

                case TrashType.EWaste:
                    feedbackText.text = "Wrong! The " + listTrashCollected.First().trashName + " belongs in the E-Waste bin!";
                    break;
            }
        }
        DisplayNextItem();
    }

    // Show Win or Lose Screen
    IEnumerator GoToFinalScreen()
    {
        foreach (var button in trashBinButtons)
        {
            button.enabled = false;
        }
        trashPanel.SetActive(false);

        saver.SaveData();

        yield return new WaitForSeconds(2);

        if (score >= levelSuccessScoreThreshold)
        {
            winScoreText.text = "Score: " + score;
            winScreen.SetActive(true);
        }
        else
        {
            loseScoreText.text = "Score: " + score;
            loseScreen.SetActive(true);
        }
    }

    

    
}

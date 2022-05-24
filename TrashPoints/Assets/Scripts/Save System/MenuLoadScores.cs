using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoadScores : MonoBehaviour
{
    private string savePath;
    private int[] loadedMaxScores;
    public List<Text> scoreTexts = new List<Text>();
    public List<GameObject> lockIcons = new List<GameObject>();
    public List<GameObject> passedIcons = new List<GameObject>();
    public List<Button> levelButtons = new List<Button>();

    void Awake()
    {
        savePath = Application.persistentDataPath + "/scores.save";
        LoadAndFixData();
    }

    private void DisplayInfo()
    {
        bool unlockedLevelFound = false;
        for (int i = 0; i < scoreTexts.Count; i++)
        {
            if (loadedMaxScores[i] == -1)
            {
                if (unlockedLevelFound)
                {
                    lockIcons[i].SetActive(true);
                    passedIcons[i].SetActive(false);
                    levelButtons[i].interactable = false;
                    scoreTexts[i].text = "Locked";
                } else
                {
                    lockIcons[i].SetActive(false);
                    passedIcons[i].SetActive(false);
                    levelButtons[i].interactable = true;
                    scoreTexts[i].text = "Score: N/A";
                    unlockedLevelFound = true;
                }
            }
            else
            {
                lockIcons[i].SetActive(false);
                passedIcons[i].SetActive(true);
                levelButtons[i].interactable = true;
                scoreTexts[i].text = "Score: " + loadedMaxScores[i];
            }
        }
    }

    public void LoadAndFixData()
    {
        var binaryFormatter = new BinaryFormatter();
        if (File.Exists(savePath))
        {
            SaveInfo save;

            using (var fileStream = File.Open(savePath, FileMode.Open))
            {
                save = (SaveInfo) binaryFormatter.Deserialize(fileStream);
            }

            loadedMaxScores = save.MaxScores;

            if (loadedMaxScores.Length < Constants.NUMBER_OF_LEVELS) // If there are levels missing
            {
                int[] saveData = new int[Constants.NUMBER_OF_LEVELS];
                for (int i = 0; i < Constants.NUMBER_OF_LEVELS; i++)
                {
                    if (i < loadedMaxScores.Length)
                    {
                        saveData[i] = loadedMaxScores[i];
                    }
                    else
                    {
                        saveData[i] = -1; // Never played the level
                    }
                }

                var newSave = new SaveInfo()
                {
                    MaxScores = saveData
                };

                using (var fileStream = File.Create(savePath))
                {
                    binaryFormatter.Serialize(fileStream, newSave);
                }

                loadedMaxScores = saveData;
            }
            else if (loadedMaxScores.Length > Constants.NUMBER_OF_LEVELS) // If there's more scores than levels
            {
                int[] saveData = new int[Constants.NUMBER_OF_LEVELS];
                for (int i = 0; i < Constants.NUMBER_OF_LEVELS; i++)
                {
                    saveData[i] = loadedMaxScores[i];
                }

                var newSave = new SaveInfo()
                {
                    MaxScores = saveData
                };

                using (var fileStream = File.Create(savePath))
                {
                    binaryFormatter.Serialize(fileStream, newSave);
                }

                loadedMaxScores = saveData;
            }
        }
        else
        {
            // No save data, create it
            int[] saveData = new int[Constants.NUMBER_OF_LEVELS];
            for (int i = 0; i < Constants.NUMBER_OF_LEVELS; i++)
            {
                    saveData[i] = -1; // Never played the level
            }

            var newSave = new SaveInfo()
            {
                MaxScores = saveData
            };

            using (var fileStream = File.Create(savePath))
            {
                binaryFormatter.Serialize(fileStream, newSave);
            }

            loadedMaxScores = saveData;
        }
        DisplayInfo();
    }
}
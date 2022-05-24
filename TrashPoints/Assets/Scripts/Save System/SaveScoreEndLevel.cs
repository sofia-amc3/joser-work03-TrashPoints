using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveScoreEndLevel : MonoBehaviour
{
    private string savePath;
    private int[] loadedMaxScores;
    private LevelController levelController;

    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        savePath = Application.persistentDataPath + "/scores.save";
    }

    public void SaveData()
    {
        var binaryFormatter = new BinaryFormatter();

        if (File.Exists(savePath))
        {
            SaveInfo loadedSave;
            
            using (var fileStream = File.Open(savePath, FileMode.Open))
            {
                loadedSave = (SaveInfo)binaryFormatter.Deserialize(fileStream);
            }

            loadedMaxScores = loadedSave.MaxScores;
        }

        if (levelController.Score() > loadedMaxScores[levelController.levelSaveIndex] || loadedMaxScores[levelController.levelSaveIndex] == -1) // If -1, level has never been played
        {
            loadedMaxScores[levelController.levelSaveIndex] = levelController.Score();
            var save = new SaveInfo()
            {
                MaxScores = loadedMaxScores
            };

            using (var fileStream = File.Create(savePath))
            {
                binaryFormatter.Serialize(fileStream, save);
            }
        }
    }
}
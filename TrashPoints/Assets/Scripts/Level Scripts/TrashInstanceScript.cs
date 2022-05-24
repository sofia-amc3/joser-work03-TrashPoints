using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashInstanceScript : MonoBehaviour
{
    private LevelController levelController;
    private TrashItem trashItem;
    private bool found = false;
    private float t = 0;

    public void Initialize(TrashSpawn trashSpawn)
    {
        trashItem = trashSpawn.trashItem;
        gameObject.GetComponent<Image>().sprite = trashItem.trashInScene;
        gameObject.GetComponent<Image>().preserveAspect = true;
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.01f;
        gameObject.SetActive(true);
    }

    public void ClickFindTrash()
    {
        found = true;
        Destroy(gameObject, 1);

        levelController.UpdateScore(Constants.SCORE_FOR_CORRECT_CLICK);
        levelController.FoundTrash(trashItem);
    }

    void Update()
    {
        if (found)
        {
            ItemDisappearOverTime();
        }
    }


    void ItemDisappearOverTime()
    {
        gameObject.GetComponent<Image>().color = Color.Lerp(Color.white, Color.clear, t);

        if (t < 1)
        {
            t += Time.deltaTime / 1.0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }
}

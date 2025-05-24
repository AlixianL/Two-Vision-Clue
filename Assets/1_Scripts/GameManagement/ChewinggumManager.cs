using UnityEngine;

public class ChewinggumManager : MonoBehaviour, ISaveAndPullData
{
    public void PushDataToSave() {} // Ne sert pas dans ce script

    public void PullDataFromSave()
    {
        foreach (string chewinggumInstanceName in SaveData.Instance.gameData.chewinggumsAlreadyTooked)
        {
            GameObject currentChewinggum = GameObject.Find(chewinggumInstanceName);
            if (currentChewinggum != null)
            {
                Destroy(currentChewinggum);
            }
        }

    }
}

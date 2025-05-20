using System.Collections.Generic;
using UnityEngine;

public class TriggerLoadSave : MonoBehaviour, IActivatable
{
    [SerializeField] private bool loadingMode;
    [SerializeField] private LoadAndSave loadAndSave;
    [SerializeField] private List<MonoBehaviour> loadGameScript = new List<MonoBehaviour>();
    public void Activate()
    {
        if (loadingMode)
        {
            loadAndSave.Load();

            foreach (var script in loadGameScript)
            {
                ISaveAndPullData activatable = script.GetComponent<ISaveAndPullData>();
                if (activatable != null)
                {
                    activatable.PullDataFromSave();
                    Debug.Log("Pull Complete");
                }
            }
        }
        
        else loadAndSave.Save();
    }
}

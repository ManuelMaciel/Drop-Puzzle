using Code.Services.SaveLoadService;
using UnityEngine;
using Zenject;

public class ExitAppDetector : MonoBehaviour
{
    private ISaveLoadService _saveLoadService;

    [Inject]
    public void Construct(ISaveLoadService saveLoadService)
    {
        _saveLoadService = saveLoadService;
    }
    
    void OnApplicationQuit()
    {
        _saveLoadService.SaveProgress();
        
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}

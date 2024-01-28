using Code.Runtime.Extensions;
using Code.Runtime.Repositories;
using CodeBase.Services.LogService;
using UnityEngine;

namespace Code.Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly ILogService _logService;

        // private readonly IEnumerable<IProgressSaver> saverServices;
        private PlayerProgress _playerProgress;

        SaveLoadService(ILogService logService)
        {
            _logService = logService;
        }

        public void Initialize(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
        }

        public void SaveProgress()
        {
            // foreach (var saver in saverServices) 
            //     saver.UpdateProgress(_persistentProgressService.Progress);

            PlayerPrefs.SetString(ProgressKey, _playerProgress.ToJson());
        }

        //Оно создает новый объект
        public PlayerProgress LoadProgress()
        {
            _logService.Log("Data loaded");
            
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }
    }
}
using System.Collections.Generic;
using Code.Runtime.Extensions;
using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using CodeBase.Services.LogService;
using UnityEngine;

namespace Code.Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly ILogService _logService;
        private readonly List<IUpdatebleProgress> _updatebleProgresses = new List<IUpdatebleProgress>();
        
        private PlayerProgress _playerProgress;

        SaveLoadService(ILogService logService)
        {
            _logService = logService;
        }

        public void Initialize(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
        }

        public void AddUpdatebleProgress(IUpdatebleProgress updatebleProgress)
        {
            _updatebleProgresses.Add(updatebleProgress);
        }

        public void SaveProgress()
        {
            foreach (var progress in _updatebleProgresses) 
                progress.UpdateProgress();

            PlayerPrefs.SetString(ProgressKey, _playerProgress.ToJson());
        }

        public bool TryLoadProgress(out PlayerProgress playerProgress)
        {
            playerProgress = PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

            bool dataLoaded = playerProgress != null;

            if(dataLoaded) _logService.Log("Data loaded");

            return dataLoaded;
        }
    }
}
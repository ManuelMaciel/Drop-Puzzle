using System.Collections.Generic;
using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using Code.Runtime.Services.LogService;
using Code.Runtime.Services.Progress;
using Plugin.DocuFlow.Documentation;

namespace Code.Runtime.Services.SaveLoadService
{
    [Doc("The SaveLoadService class provides functionality for saving and loading player progress. It manages the serialization and deserialization of player progress data using binary serialization, allowing for persistent storage across game sessions.")]
    public class SaveLoadService : ISaveLoadService
    {
        public const string ProgressKey = "Progress";

        private readonly ILogService _logService;
        private readonly IPersistentProgressService _progressService;
        private readonly List<IUpdatebleProgress> _updatebleProgresses = new List<IUpdatebleProgress>();

        private PlayerProgress _playerProgress;
        private BinarySaver<PlayerProgress> _binarySaver;

        SaveLoadService(ILogService logService, IPersistentProgressService progressService)
        {
            _logService = logService;
            _progressService = progressService;

            _binarySaver = new BinarySaver<PlayerProgress>();
        }

        public void Initialize(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
        }

        [Doc("Adds an updateable progress object to the list of progress objects to be updated during progress saving.")]
        public void AddUpdatebleProgress(IUpdatebleProgress updatebleProgress) =>
            _updatebleProgresses.Add(updatebleProgress);

        [Doc("Removes an updateable progress object from the list of progress objects.")]
        public void RemoveUpdatebleProgress(IUpdatebleProgress updatebleProgress) =>
            _updatebleProgresses.Remove(updatebleProgress);

        public void ClearUpdatebleProgress() =>
            _updatebleProgresses.Clear();

        [Doc("Saves the player progress by updating all updateable progress objects and serializing the player progress data using binary serialization.")]
        public void SaveProgress()
        {
            foreach (var progress in _updatebleProgresses)
                progress.UpdateProgress(_progressService);

            _binarySaver.Save(ProgressKey, _playerProgress);
        }

        [Doc("Attempts to load player progress data from storage using binary deserialization. Returns true if data is successfully loaded, false otherwise.")]
        public bool TryLoadProgress(out PlayerProgress playerProgress)
        {
            playerProgress = _binarySaver.Load(ProgressKey);

            bool dataLoaded = playerProgress != null;
            
            if (dataLoaded) _logService.Log("Data loaded");

            return dataLoaded;
        }
    }
}
using System.Collections.Generic;
using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using Code.Services.Progress;
using CodeBase.Services.LogService;

namespace Code.Services.SaveLoadService
{
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

        public void AddUpdatebleProgress(IUpdatebleProgress updatebleProgress) =>
            _updatebleProgresses.Add(updatebleProgress);

        public void RemoveUpdatebleProgress(IUpdatebleProgress updatebleProgress) =>
            _updatebleProgresses.Remove(updatebleProgress);

        public void ClearUpdatebleProgress() =>
            _updatebleProgresses.Clear();

        public void SaveProgress()
        {
            foreach (var progress in _updatebleProgresses)
                progress.UpdateProgress(_progressService);

            _binarySaver.Save(ProgressKey, _playerProgress);
        }

        public bool TryLoadProgress(out PlayerProgress playerProgress)
        {
            playerProgress = _binarySaver.Load(ProgressKey);

            bool dataLoaded = playerProgress != null;
            
            if (dataLoaded) _logService.Log("Data loaded");

            return dataLoaded;
        }
    }
}
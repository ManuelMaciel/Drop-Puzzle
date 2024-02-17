using Code.Runtime.Configs;
using CodeBase.Services.LogService;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string ShapeSizeConfigPath = "Configs/ShapeSizeConfig";
        private const string ShapeScoreConfigPath = "Configs/ShapeScoreConfig";
        private const string PurchasedBackgroundsConfigPath = "Configs/PurchasedBackgroundsConfig";
        private const string AudioConfigPath = "Configs/AudioConfig";

        public ShapeSizeConfig ShapeSizeConfig { get; private set; }
        public ShapeScoreConfig ShapeScoreConfig { get; private set; }
        public PurchasedBackgroundsConfig PurchasedBackgroundsConfig { get; private set; }
        public AudioConfig AudioConfig { get; private set; }

        private readonly ILogService log;

        public StaticDataService(ILogService log)
        {
            this.log = log;
        }

        public void Initialize()
        {
            ShapeSizeConfig = LoadResource<ShapeSizeConfig>(ShapeSizeConfigPath);
            ShapeScoreConfig = LoadResource<ShapeScoreConfig>(ShapeScoreConfigPath);
            PurchasedBackgroundsConfig = LoadResource<PurchasedBackgroundsConfig>(PurchasedBackgroundsConfigPath);
            AudioConfig = LoadResource<AudioConfig>(AudioConfigPath);

            log.Log("Static data loaded");
        }

        private T LoadResource<T>(string path) where T : Object
        {
            T loadResource = Resources.Load<T>(path);

            if (loadResource == null)
            {
                log.LogError($"Failed to load with path: {path}");
            }

            return loadResource;
        }
    }
}
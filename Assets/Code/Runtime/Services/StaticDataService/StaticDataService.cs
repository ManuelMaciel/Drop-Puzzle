using Code.Runtime.Configs;
using Code.Runtime.Services.LogService;
using UnityEngine;

namespace Code.Runtime.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string CoreConfigPath = "Configs/CoreConfig";

        public ShapeSizeConfig ShapeSizeConfig { get; private set; }
        public ShapeScoreConfig ShapeScoreConfig { get; private set; }
        public PurchasedBackgroundsConfig PurchasedBackgroundsConfig { get; private set; }
        public AudioConfig AudioConfig { get; private set; }
        public WindowAssetsConfig WindowAssetsConfig { get; private set; }
        public GameplayConfig GameplayConfig { get; private set; }
        public AdConfig AdConfig { get; private set; }

        private readonly ILogService log;

        public StaticDataService(ILogService log)
        {
            this.log = log;
        }

        public void Initialize()
        {
            CoreConfig coreConfig = LoadResource<CoreConfig>(CoreConfigPath);

            ShapeSizeConfig = coreConfig.ShapeSizeConfig;
            ShapeScoreConfig = coreConfig.ShapeScoreConfig;
            PurchasedBackgroundsConfig = coreConfig.PurchasedBackgroundsConfig;
            AudioConfig = coreConfig.AudioConfig;
            WindowAssetsConfig = coreConfig.WindowAssetsConfig;
            GameplayConfig = coreConfig.GameplayConfig;
            AdConfig = coreConfig.AdConfig;

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
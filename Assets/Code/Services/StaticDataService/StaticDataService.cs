using Code.Runtime.Configs;
using CodeBase.Services.LogService;
using CodeBase.Services.StaticDataService;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string ShapeSizeConfigPath = "Configs/ShapeSizeConfig";
        private const string ShapeScoreConfigPath = "Configs/ShapeScoreConfig";

        public ShapeSizeConfig ShapeSizeConfig { get; private set; }
        public ShapeScoreConfig ShapeScoreConfig { get; private set; }

        private readonly ILogService log;

        public StaticDataService(ILogService log)
        {
            this.log = log;
        }

        public void Initialize()
        {
            ShapeSizeConfig = Resources.Load<ShapeSizeConfig>(ShapeSizeConfigPath);
            ShapeScoreConfig = Resources.Load<ShapeScoreConfig>(ShapeScoreConfigPath);
            
            log.Log("Static data loaded");
        }
    }
}
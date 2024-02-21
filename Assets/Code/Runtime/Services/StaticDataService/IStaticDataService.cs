using Code.Runtime.Configs;

namespace Code.Runtime.Services.StaticDataService
{
    public interface IStaticDataService
    {
        void Initialize();
        ShapeSizeConfig ShapeSizeConfig { get; }
        ShapeScoreConfig ShapeScoreConfig { get; }
        PurchasedBackgroundsConfig PurchasedBackgroundsConfig { get; }
        AudioConfig AudioConfig { get; }
    }
}
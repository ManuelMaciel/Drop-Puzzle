using Code.Runtime.Configs;

namespace CodeBase.Services.StaticDataService
{
    public interface IStaticDataService
    {
        void Initialize();
        ShapeSizeConfig ShapeSizeConfig { get; }
        ShapeScoreConfig ShapeScoreConfig { get; }
        PurchasedBackgroundsConfig PurchasedBackgroundsConfig { get; }
    }
}
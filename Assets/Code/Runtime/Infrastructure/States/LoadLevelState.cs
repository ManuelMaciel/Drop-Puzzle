using Code.Runtime.Logic;
using Code.Runtime.UI;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly HUD.Factory _hudFactory;
        private readonly Spawner.Factory _spawnerFactory;

        LoadLevelState(ISceneLoader sceneLoader, HUD.Factory hudFactory, Spawner.Factory spawnerFactory)
        {
            _sceneLoader = sceneLoader;
            _hudFactory = hudFactory;
            _spawnerFactory = spawnerFactory;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, InitWorld);
        }

        private void InitWorld()
        {
            _hudFactory.Create();
            _spawnerFactory.Create();
        }

        public void Exit()
        {
        }
    }
}
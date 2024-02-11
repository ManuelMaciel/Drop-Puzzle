using Code.Runtime.Infrastructure.Bootstrappers;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        GameBootstrapper.Factory gameBootstrapperFactory;

        [Inject]
        void Construct(GameBootstrapper.Factory bootstrapperFactory) =>
            this.gameBootstrapperFactory = bootstrapperFactory;

        private void Awake()
        {
            StartBootstrapper();
        }

        private void StartBootstrapper()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null) return;
            
            gameBootstrapperFactory.Create();
        }
    }
}
using Code.Runtime.Infrastructure.ObjectPool;
using Zenject;

namespace Code.Runtime.Installers
{
    public class MenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGlobalGameObjectPool();
        }
        
        private void BindGlobalGameObjectPool()
        {
            Container.BindInterfacesTo<GameObjectsPoolContainer>().AsSingle();
        }
    }
}
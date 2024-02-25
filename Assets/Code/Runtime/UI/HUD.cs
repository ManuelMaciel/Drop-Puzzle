using Code.Runtime.Logic;
using Code.Runtime.Repositories;
using Code.Runtime.Services.Progress;
using Code.Runtime.Services.StaticDataService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private HUDGameplayContent _hudGameplayContent;
        [SerializeField] private HUDLoseContent _hudLoseContent;

        [SerializeField] private GameObject _gameplayContent;
        [SerializeField] private GameObject _loseContent;

        [Inject]
        void Construct(IPersistentProgressService persistentProgressService,
            IShapeDeterminantor shapeDeterminantor,
            IStaticDataService staticDataService)
        {
            IInteractorContainer interactorContainer = persistentProgressService.InteractorContainer;

            _hudGameplayContent.Construct(shapeDeterminantor, interactorContainer, staticDataService);
            _hudLoseContent.Construct(interactorContainer);
            
            SwitchContent(true);
        }

        private void Start() =>
            _hudGameplayContent.Initialize();

        private void OnDestroy() =>
            _hudGameplayContent.Dispose();

        public void ChangeToLose()
        {
            SwitchContent(false);

            _hudLoseContent.DrawLoseContent();
        }

        private void SwitchContent(bool isGameplayContent)
        {
            _gameplayContent.SetActive(isGameplayContent);
            _loseContent.SetActive(!isGameplayContent);
        }
    }
}
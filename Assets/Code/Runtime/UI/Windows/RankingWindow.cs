using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using Code.Runtime.Services.StaticDataService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class RankingWindow : WindowBase
    {
        [SerializeField] private RankElement rankElementPrefab;
        [SerializeField] private Transform rankElementsContainer;
        [SerializeField] private Button sortByTodayButton;
        [SerializeField] private Button sortByWeekButton;
        [SerializeField] private Button sortByAllTimeButton;

        private int _countRankElements;

        private IStaticDataService _staticDataService;
        private RankingInteractor _rankingInteractor;
        private ShapeSizeConfig _shapeSizeConfig;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            SortByAllTime();
        }

        protected override void Initialize()
        {
            _shapeSizeConfig = _staticDataService.ShapeSizeConfig;
            _countRankElements = _shapeSizeConfig.ShapesCount();
            _rankingInteractor = _interactorContainer.Get<RankingInteractor>();
        }

        protected override void SubscribeUpdates()
        {
            sortByTodayButton.onClick.AddListener(SortByToday);
            sortByWeekButton.onClick.AddListener(SortByWeek);
            sortByAllTimeButton.onClick.AddListener(SortByAllTime);
        }

        protected override void Cleanup()
        {
            sortByTodayButton.onClick.RemoveListener(SortByToday);
            sortByWeekButton.onClick.RemoveListener(SortByWeek);
            sortByAllTimeButton.onClick.RemoveListener(SortByAllTime);
        }

        private void SortByToday() =>
            DrawRanking(RankingInteractor.SortByDate.Today);

        private void SortByWeek() =>
            DrawRanking(RankingInteractor.SortByDate.Week);

        private void SortByAllTime() =>
            DrawRanking(RankingInteractor.SortByDate.AllTime);

        private void DrawRanking(RankingInteractor.SortByDate sortByDate)
        {
            ClearRankElementsContainer(); 

            RecordData[] recordsData = _rankingInteractor.GetRecords(sortByDate, _countRankElements);

            for (int i = 0; i < _countRankElements; i++)
            {
                RankElement rankElement = Instantiate(rankElementPrefab, rankElementsContainer);
                int score = recordsData.Length > i ? recordsData[i].Score : 0;
                
                rankElement.Initialize(_shapeSizeConfig.Sprites[(_countRankElements - 1) - i], i, score);
            }
        }

        private void ClearRankElementsContainer()
        {
            foreach (Transform child in rankElementsContainer)
                Destroy(child.gameObject);
        }
    }
}
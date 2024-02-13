using System;
using Code.Runtime.Infrastructure;
using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI
{
    [Serializable]
    public class HUDLoseContent
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _maxScoreText;

        private ScoreInteractor _scoreInteractor;

        public void Construct(IInteractorContainer interactorContainer)
        {
            _scoreInteractor = interactorContainer.Get<ScoreInteractor>();
        }

        public void DrawLoseContent()
        {
            _scoreText.text = _scoreInteractor.GetCurrentScore().ToString();
            _maxScoreText.text = _scoreInteractor.GetMaxScore().ToString();
        }
    }
}
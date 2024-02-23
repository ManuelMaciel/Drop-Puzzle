using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class SwitchButton : MonoBehaviour
    {
        public event Action OnStateChanged;

        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private Button _button;
        private bool _isOn;

        public void Initialize() =>
            _button = this.GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(OnChangeState);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnChangeState);

        public void UpdateState(bool isOn)
        {
            _isOn = isOn;
            
            _button.image.sprite = _isOn ? _onSprite : _offSprite;
        }

        private void OnChangeState()
        {
            UpdateState(!_isOn);

            OnStateChanged?.Invoke();
        }
    }
}
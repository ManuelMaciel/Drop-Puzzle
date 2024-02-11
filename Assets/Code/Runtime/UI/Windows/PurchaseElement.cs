using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseElement : MonoBehaviour
{
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Image background;
    [SerializeField] private GameObject priceField;
    [SerializeField] private GameObject selectedField;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Color prePurchaseColor;

    private bool isPurchased;
    private Action _onPurchased;
    private Action _onSelected;

    private void Start() =>
        purchaseButton.onClick.AddListener(Iteract);

    private void OnDestroy() =>
        purchaseButton.onClick.RemoveListener(Iteract);

    public void Initialize(Sprite backgroundSprite, int price,
        Action onPurchased, Action onSelected)
    {
        background.sprite = backgroundSprite;
        priceText.text = price.ToString();
        background.color = prePurchaseColor;

        _onPurchased = onPurchased;
        _onSelected = onSelected;
    }

    public void Purchase()
    {
        background.color = Color.white;
        priceField.SetActive(false);
        isPurchased = true;
    }

    public void Select()
    {
        selectedField.SetActive(true);
    }

    public void Unselect()
    {
        selectedField.SetActive(false);
    }

    private void Iteract()
    {
        if (isPurchased)
            _onSelected?.Invoke();
        else
            _onPurchased?.Invoke();
    }
}
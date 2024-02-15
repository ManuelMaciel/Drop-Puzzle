using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Windows
{
    public class RankElement : MonoBehaviour
    {
        [SerializeField] private Image shapeImage;
        [SerializeField] private TextMeshProUGUI indexRankText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void Initialize(Sprite shapeIcon, int indexRank, int score)
        {
            shapeImage.sprite = shapeIcon;
            indexRankText.text = indexRank.ToString();
            scoreText.text = score.ToString();
        }
    }
}
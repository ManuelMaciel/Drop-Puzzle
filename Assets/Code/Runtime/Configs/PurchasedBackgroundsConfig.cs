using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "PurchasedBackgrounds", menuName = "Configs/PurchasedBackgrounds")]
    public class PurchasedBackgroundsConfig : ScriptableObject
    {
        public List<PurchasedBackground> PurchasedBackgrounds;
    }

    [Serializable]
    public class PurchasedBackground
    {
        public Sprite Background;
        public int Price;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "PurchasedBackgroundsConfig", menuName = "Configs/PurchasedBackgroundsConfig")]
    public class PurchasedBackgroundsConfig : ScriptableObject
    {
        public List<PurchasedBackground> PurchasedBackgrounds = new List<PurchasedBackground>();

        public PurchasedBackground GetByBackgroundType(BackgroundType backgroundType) => 
            PurchasedBackgrounds.Find(pb => pb.BackgroundType == backgroundType);

        private void OnValidate()
        {
            Array arrayTypes = Enum.GetValues(typeof(BackgroundType));

            Debug.Log(PurchasedBackgrounds.Count < arrayTypes.Length);
            
            if (PurchasedBackgrounds == null || PurchasedBackgrounds.Count < arrayTypes.Length)
            {
                foreach (BackgroundType backgroundType in arrayTypes)
                {
                    if(PurchasedBackgrounds.Any(pb => pb.BackgroundType == backgroundType)) continue;

                    PurchasedBackgrounds.Add(new PurchasedBackground()
                    {
                        BackgroundType = backgroundType
                    });
                }
            }
        }
    }

    [Serializable]
    public class PurchasedBackground
    {
        public BackgroundType BackgroundType;
        public Sprite Background;
        public int Price;
    }
}
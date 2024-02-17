using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip Ambient;

        public List<SfxData> SfxsData = new();

        private void OnValidate()
        {
            Array arrayTypes = Enum.GetValues(typeof(SfxType));

            if (SfxsData == null || SfxsData.Count < arrayTypes.Length)
            {
                foreach (SfxType sfxType in arrayTypes)
                {
                    if (SfxsData.Any(sd => sd.SfxType == sfxType)) continue;

                    SfxsData.Add(new SfxData()
                    {
                        SfxType = sfxType
                    });
                }
            }
        }
    }

    [Serializable]
    public struct SfxData
    {
        public SfxType SfxType;
        public AudioClip Sfx;
    }
}
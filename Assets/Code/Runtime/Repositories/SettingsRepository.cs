using System;
using UnityEngine.Serialization;

namespace Code.Runtime.Repositories
{
    [Serializable]
    public class SettingsRepository : IRepository
    {
        public bool IsEnableVibrate;
        public bool IsEnableSFX;
        public bool IsEnableAmbient;
    }
}
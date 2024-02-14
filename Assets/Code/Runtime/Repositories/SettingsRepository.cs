using System;

namespace Code.Runtime.Repositories
{
    [Serializable]
    public class SettingsRepository : IRepository
    {
        public bool IsVibrate;
    }
}
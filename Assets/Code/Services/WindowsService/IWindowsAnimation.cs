using System;
using UnityEngine;

namespace Code.Services.WindowsService
{
    public interface IWindowsAnimation
    {
        void OpenAnimation(Transform window);
        void CloseAnimation(Transform window, Action onCallback);
    }
}
using System;
using UnityEngine;

namespace Code.Runtime.Services.InputService
{
    public interface IInput
    {
        event Action OnDrop;
        float GetXPosition();
        Vector2 GetPosition();
        bool IsPress();
        bool IsDropped();
    }
}

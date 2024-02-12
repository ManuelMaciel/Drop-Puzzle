using System;
using UnityEngine;

public interface IInput
{
    event Action OnDrop;
    float GetXPosition();
    Vector2 GetPosition();
    bool IsPress();
    bool IsDropped();
}

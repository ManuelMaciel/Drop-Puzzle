using System;

public interface IInput
{
    event Action OnDrop;
    float GetXPosition();
    bool IsPress();
    bool IsDropped();
}

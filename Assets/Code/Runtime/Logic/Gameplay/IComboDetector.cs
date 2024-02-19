using System;
using UnityEngine;

namespace Code.Runtime.Logic.Gameplay
{
    public interface IComboDetector
    {
        event Action<int, Vector3> OnComboDetected;
    }
}
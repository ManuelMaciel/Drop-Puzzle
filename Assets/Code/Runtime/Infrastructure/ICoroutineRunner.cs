using System.Collections;
using UnityEngine;

namespace Code.Runtime.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}
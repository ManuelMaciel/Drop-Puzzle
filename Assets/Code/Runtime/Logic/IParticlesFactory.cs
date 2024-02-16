using UnityEngine;

namespace Code.Runtime.Logic
{
    public interface IParticlesFactory
    {
        ParticleSystem CreateTapVfx(Vector3 at);
        ParticleSystem CreateDeathVfx(Vector3 at);
    }
}
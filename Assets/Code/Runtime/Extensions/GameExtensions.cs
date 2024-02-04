using System;
using Code.Runtime.Infrastructure.States;

namespace Code.Runtime.Extensions
{
    public static class GameExtensions
    {
        public static ShapeSize NextSize(this ShapeSize shapeSize)
        {
            int nextSize = (((int) shapeSize) + 1);

            if (Enum.GetNames(typeof(ShapeSize)).Length < nextSize)
            {
                nextSize = 0;
            }

            return (ShapeSize) nextSize;
        }

        public static int Index(this SceneName sceneName)
        {
            return (int) sceneName;
        }
    }
}
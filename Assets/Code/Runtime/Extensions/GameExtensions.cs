using Code.Runtime.Infrastructure.States;

namespace Code.Runtime.Extensions
{
    public static class GameExtensions
    {
        public static ShapeSize NextSize(this ShapeSize shapeSize) 
            => (ShapeSize) (((int) shapeSize) + 1);

        public static int Index(this SceneName sceneName)
            => (int) sceneName;
    }
}
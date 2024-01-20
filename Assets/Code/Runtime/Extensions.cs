using Code.Runtime.Infrastructure.States;

namespace Code.Runtime
{
    public static class Extensions
    {
        public static ShapeSize NextSize(this ShapeSize shapeSize) 
            => (ShapeSize) (((int) shapeSize) + 1);

        public static int Index(this SceneName sceneName)
            => (int) sceneName;
    }
}
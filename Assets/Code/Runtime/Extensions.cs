namespace Code.Runtime
{
    public static class Extensions
    {
        public static ShapeSize NextSize(this ShapeSize shapeSize) 
            => (ShapeSize) (((int) shapeSize) + 1);
    }
}
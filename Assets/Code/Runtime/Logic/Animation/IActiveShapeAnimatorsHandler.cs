namespace Code.Runtime.Logic
{
    public interface IActiveShapeAnimatorsHandler
    {
        void AddShapeAnimator(Shape shape, ShapeAnimator shapeAnimator);
        void RemoveShapeAnimator(Shape shape);
    }
}
namespace Code.Runtime.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
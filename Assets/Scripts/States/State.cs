namespace NinjaJump.States
{
    public abstract class State
    {
        protected readonly IApplication _application;

        protected State(IApplication application)
        {
            _application = application;
        }

        public virtual void Enter()
        {
        }

        public virtual void Update(float deltaTime)
        {
        }

        public virtual void Exit()
        {
        }
    }
}
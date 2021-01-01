namespace Base.Game.InteractionalObject
{
    using Base.Game.Command;
    using Base.Game.State;

    public class StateMoveableXY : IState
    {
        public StateMoveableXY(MoveableObjectContext context) => context.Movement = new Command<MovementActionToTarget>(context.MovementActions, m => m.MovementXY());
        public virtual void Handle(Context context) => context.State = new StateMoveableXPlus((MoveableObjectContext)context);
    }

    public class StateMoveableXPlus : IState
    {
        public StateMoveableXPlus(MoveableObjectContext context) => context.Movement = new Command<MovementActionToTarget>(context.MovementActions, m => m.MovementXPlus());
        public virtual void Handle(Context context) => context.State = new StateNonMoveable((MoveableObjectContext)context);
    }

    public class StateNonMoveable : IState
    {
        public StateNonMoveable(MoveableObjectContext context) => context.Movement = null;
        public virtual void Handle(Context context) => context.State = new StateMoveableXY((MoveableObjectContext)context);
    }

}

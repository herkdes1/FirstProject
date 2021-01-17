namespace Base.Game.InteractionalObject
{
    using Base.Game.Command;
    using Base.Game.State;

    public class MoveableObjectContext : Context
    {
        public MovementActionToTarget MovementActions { get; private set; }
        public ICommand Movement { get; set; }
        public MoveableObjectContext(IMoveableObject obj) => MovementActions = new MovementActionToTarget(obj);
    }
}

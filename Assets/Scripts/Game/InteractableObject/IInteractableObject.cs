namespace Base.Game.InteractableObject
{
    using Base.Game.InteractionalObject;

    public interface IInteractableObject
    {
        void Interact(IInteractionalObject obj);
        void DeInteract(IInteractionalObject obj);
        void ContinuousInteract(IInteractionalObject obj);
    }
}

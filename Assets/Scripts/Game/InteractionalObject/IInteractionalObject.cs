namespace Base.Game.InteractionalObject
{
    using Base.Game.InteractableObject;
    public interface IInteractionalObject
    {
        UnityEngine.Transform GetTransform();
        void Interaction(IInteractableObject obj);
    }
}

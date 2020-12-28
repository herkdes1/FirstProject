namespace Base.Game.InteractionalObject
{
    public interface IMoveableObject
    {
        UnityEngine.Transform GetTransform();
        float GetSpeed();
        UnityEngine.Vector3 GetTarget();
    }
}

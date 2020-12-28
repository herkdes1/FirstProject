namespace Base.Game.InteractionalObject
{
    using UnityEngine;
    public class MovementActionToTarget
    {
        private IMoveableObject _obj;

        public MovementActionToTarget(IMoveableObject obj) => _obj = obj;

        public void MovementXYZ(Vector3 target) => _obj.GetTransform().position = Vector3.MoveTowards(_obj.GetTransform().position, target, _obj.GetSpeed());

        public void MovementXY()
        {
            Vector3 desiredPos = new Vector3(_obj.GetTarget().x, _obj.GetTarget().y, _obj.GetTransform().position.z);
            _obj.GetTransform().position = Vector3.MoveTowards(_obj.GetTransform().position, desiredPos, _obj.GetSpeed());
        }

        public void MovementYZ()
        {
            Vector3 desiredPos = new Vector3(_obj.GetTransform().position.x, _obj.GetTarget().y, _obj.GetTarget().z);
            _obj.GetTransform().position = Vector3.MoveTowards(_obj.GetTransform().position, desiredPos, _obj.GetSpeed());
        }

        public void MovementXZ()
        {
            Vector3 desiredPos = new Vector3(_obj.GetTarget().x, _obj.GetTransform().position.y, _obj.GetTarget().z);
            _obj.GetTransform().position = Vector3.MoveTowards(_obj.GetTransform().position, desiredPos, _obj.GetSpeed());
        }

        public void MovementX()
        {
            Vector3 desiredPos = new Vector3(_obj.GetTarget().x, _obj.GetTransform().position.y, _obj.GetTransform().position.z);
            _obj.GetTransform().position = Vector3.MoveTowards(_obj.GetTransform().position, desiredPos, _obj.GetSpeed());
        }

        public void MovementXPlus()
        {
            if (_obj.GetTarget().x > _obj.GetTransform().position.x)
                MovementX();
        }

        public void MovementXMinus()
        {
            if (_obj.GetTarget().x < _obj.GetTransform().position.x)
                MovementX();
        }

        public void MovementY()
        {
            Vector3 desiredPos = new Vector3(_obj.GetTransform().position.x, _obj.GetTarget().y, _obj.GetTransform().position.z);
            _obj.GetTransform().position = Vector3.MoveTowards(_obj.GetTransform().position, desiredPos, _obj.GetSpeed());
        }

        public void MovementYPlus()
        {
            if (_obj.GetTarget().y > _obj.GetTransform().position.y)
                MovementY();
        }

        public void MovementYMinus()
        {
            if (_obj.GetTarget().y < _obj.GetTransform().position.y)
                MovementY();
        }

        public void MovementZ()
        {
            Vector3 desiredPos = new Vector3(_obj.GetTransform().position.x, _obj.GetTransform().position.y, _obj.GetTarget().z);
            _obj.GetTransform().position = Vector3.MoveTowards(_obj.GetTransform().position, desiredPos, _obj.GetSpeed());
        }

        public void MovementZPlus()
        {
            if (_obj.GetTarget().z > _obj.GetTransform().position.z)
                MovementZ();
        }

        public void MovementZMinus()
        {
            if (_obj.GetTarget().z < _obj.GetTransform().position.z)
                MovementZ();
        }

    }
}

using Microsoft.Xna.Framework;
using UmbrellaToolsKit;

namespace Project.Components
{
    public class PositionConstrainComponent : Component
    {
        private GameObject _target;
        private Vector2 _offset = new Vector2(0.0f);

        public void SetTarget(GameObject target) => _target = target;

        public void SetOffset(Vector2 offset) => _offset = offset;

        public override void Update(float deltaTime) => GameObject.Position = _target.Position + _offset;
    }
}

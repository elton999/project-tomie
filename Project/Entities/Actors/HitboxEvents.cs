using System;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
using UmbrellaToolsKit.Collision;

namespace Project.Entities.Actors
{
    public abstract class HitboxEvents : Actor
    {
        public static Action<HitboxEvents> OnAnyInteract;

        private Actor _playerHitBoXActor;

        public override void Start()
        {
            _playerHitBoXActor = Scene.Players[1].GetActor();
            tag += " hitbox";
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            if (!overlapCheck(_playerHitBoXActor)) return;
            if (KeyBoardHandler.KeyPressed(Input.INTERACT))
            {
                OnAnyInteract?.Invoke(this);
                OnInteract();
            }
        }

        public abstract void OnInteract();
    }
}

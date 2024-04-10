using System;
using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
using UmbrellaToolsKit.Collision;

namespace Project.Entities.Actors
{
    public abstract class HitboxEvents : Actor
    {
        public static Action<HitboxEvents> OnAnyInteract;

        private Actor _playerActor;

        public override void Start()
        {
            _playerActor = Scene.AllActors[0];
            tag += " hitbox";
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            if (!overlapCheck(_playerActor)) return;
            if (KeyBoardHandler.KeyPressed(Input.INTERACT))
            {
                OnAnyInteract?.Invoke(this);
                OnInteract();
            }
        }

        public abstract void OnInteract();
    }
}

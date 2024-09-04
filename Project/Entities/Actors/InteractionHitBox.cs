using Project.UI;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework;
using Project.Components;

namespace Project.Entities.Actors
{
    public class InteractionHitBox : HitboxEvents
    {
        private InteractionMenu _interactionMenu;
        private InputMovementComponent _playerInputSettings => Scene.Players[0].GetComponent<InputMovementComponent>();

        public override void Start()
        {
            base.Start();

            _interactionMenu = new InteractionMenu();
            Scene.AddGameObject(_interactionMenu, Layers.UI);

            ldtk.FieldInstance field = (ldtk.FieldInstance)Values[0];
            if (field.Identifier != "Boolean") return;

            if ((bool)field.Value[0])
                _interactionMenu.AddButton("Look", null, null);
            if ((bool)field.Value[1])
                _interactionMenu.AddButton("Use", null, null);
            if ((bool)field.Value[2])
                _interactionMenu.AddButton("Combine", null, null);

            _interactionMenu.Position = new Vector2(100, 56);
        }

        public override void OnInteract()
        {
            _playerInputSettings.DisableInput();
            _interactionMenu.OnCloseMenu += OnLeaveInteract;
            _interactionMenu.ShowMenu();
        }

        public override void OnLeaveInteract()
        {
            _interactionMenu.OnCloseMenu -= OnLeaveInteract;
            _playerInputSettings.EnableInput();
        }
    }
}

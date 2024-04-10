using Project.UI;
using Project.Entities.Actors;
using UmbrellaToolsKit;
using Project.Components;

namespace Project.Entities
{
    public class InteractionHitBox : HitboxEvents
    {
        private InteractionMenu _interactionMenu;

        public override void Start()
        {
            base.Start();

            _interactionMenu = new InteractionMenu();
            _interactionMenu.AddButton("Look", null, null);
            _interactionMenu.AddButton("Use", null, null);
            _interactionMenu.AddButton("Combine", null, null);

            Scene.AddGameObject(_interactionMenu, Layers.UI);
        }

        public override void OnInteract()
        {
            Scene.Players[0].GetComponent<InputMovementComponent>().DisableInput();
            _interactionMenu.ShowMenu();
        }
    }
}

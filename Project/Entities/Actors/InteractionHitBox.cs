using Project.UI;
using UmbrellaToolsKit;
using Project.Components;

namespace Project.Entities.Actors
{
    public class InteractionHitBox : HitboxEvents
    {
        private InteractionMenu _interactionMenu;

        public override void Start()
        {
            base.Start();

            _interactionMenu = new InteractionMenu();
            Scene.AddGameObject(_interactionMenu, Layers.UI);

            _interactionMenu.AddButton("Look", null, null);
            _interactionMenu.AddButton("Use", null, null);
            _interactionMenu.AddButton("Combine", null, null);
        }

        public override void OnInteract()
        {
            Scene.Players[0].GetComponent<InputMovementComponent>().DisableInput();
            _interactionMenu.ShowMenu();
        }
    }
}

using UnityEngine;

namespace Gameplay.Interactions{
    public class SimpleLightInteraction : MonoBehaviour
    {
        Interactable _interactable;
        Light _light;

        void Awake()
        {
            _light = GetComponent<Light>();

            _interactable = GetComponent<Interactable>();
            _interactable.OnInteract += ToggleLight;
        }

        private void ToggleLight(CharacterInteractor interactor)
        {
            _light.enabled = !_light.enabled;
        }
    }
}

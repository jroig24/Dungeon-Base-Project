using UnityEngine;

namespace Gameplay.Interactions{
    public class ChestInteraction : MonoBehaviour
    {
        private Interactable _interactable;
        private Animator _animator;

        public GameObject rewardPrefab;
        public int rewardCount = 1;

        void Awake()
        {
            _interactable = GetComponent<Interactable>();
            _interactable.OnInteract += Interact;

            _animator = GetComponent<Animator>();
        }

        private void Interact(CharacterInteractor interactor)
        {
            _animator.SetTrigger("Open");
            
            _interactable.InteractionEnabled = false;
        }

        public void SpawnReward()
        {
            for (int i = 0; i < rewardCount; i++)
            {
                GameObject reward = Instantiate(rewardPrefab, transform.position + Vector3.up * 0.2f, Quaternion.identity);
                LaunchableReward launchableReward = reward.GetComponent<LaunchableReward>();
                if (launchableReward != null)
                {
                    launchableReward.LaunchReward();
                }
            }
        }
    }
}

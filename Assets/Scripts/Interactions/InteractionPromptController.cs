using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractionPromptController : MonoBehaviour
{
    private Interactable _interactable;
    [SerializeField] private InteractPromptIcon _interactPromptInstance;

    void Awake()
    {
        _interactable = GetComponent<Interactable>();
        _interactable.OnCandidateChange += HandleCandidateChange;
    }


    private void HandleCandidateChange(bool isCandidate)
    {
        _interactPromptInstance.SetVisible(isCandidate);
    }

    void OnDestroy()
    {
        _interactable.OnCandidateChange -= HandleCandidateChange;
    }
}

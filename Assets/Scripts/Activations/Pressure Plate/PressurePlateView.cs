using UnityEngine;

public class PressurePlateView : MonoBehaviour
{
    Animator _animator;
    [SerializeField] ParticleSystem puffParticles;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        
        if (TryGetComponent(out ActivatorBase activator))
        {
            activator.ToActivate.AddListener(OnActivated);
        }
    }

    private void OnActivated(bool isActivated)
    {
        _animator.SetBool("isPressed", isActivated);
    }

    //This method is called from the animation.
    public void PlayPuffParticles()
    {
        puffParticles.Play();
    }
}

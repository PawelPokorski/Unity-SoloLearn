using UnityEngine;

[System.Serializable]
public abstract class Manager : MonoBehaviour
{
    protected InputHandler inputHandler;
    protected GeneralManager generalManager;
    protected CharacterController controller;
    protected Animator animator;

    public virtual void Initialize(GeneralManager manager)
    {
        generalManager = manager;

        inputHandler = manager.gameObject.GetComponent<InputHandler>();
        controller = manager.gameObject.GetComponent<CharacterController>();
        animator = manager.gameObject.GetComponentInChildren<Animator>();
    }
}
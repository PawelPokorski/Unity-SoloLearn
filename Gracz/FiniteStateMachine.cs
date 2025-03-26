using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    public CharacterController Controller => GetComponent<CharacterController>();
    public GeneralManager GeneralManager => GetComponent<GeneralManager>();
    public Animator Animator => GetComponentInChildren<Animator>();
    public InputHandler InputHandler => GetComponent<InputHandler>();

    public StateFactory stateFactory;
    public State currentState;

    private void Awake()
    {
        InitializeStateMachine();
    }

    private void Update()
    {
        currentState.UpdateStates();
    }

    private void InitializeStateMachine()
    {
        stateFactory = new StateFactory(this);
        currentState = stateFactory.Grounded();
        currentState.OnEnter();
    }

    // Do przepisania do MovementManagera
    /*
    private void SearchForObstackles()
    {
        Vector3[] jointPoses = { new(0f, 0.475f, 0f), new(0f, 0.85f, 0f), new(0f, 1.225f, 0f), new(0f, 1.6f, 0f) };
            float[] distances = new float[4];
            float minDistance = 3f;

            for (int i = 0; i < jointPoses.Length; i++)
            {
                if (Physics.SphereCast(transform.position + jointPoses[i], Controller.radius, moveDirection, out RaycastHit hit, 3f, groundLayer))
                {
                    distances[i] = hit.distance;
                    minDistance = Mathf.Min(minDistance, hit.distance);
                    Debug.DrawLine(transform.position + jointPoses[i], hit.point, GetDebugColor(hit.distance));
                }
                else
                {
                    Debug.DrawLine(transform.position + jointPoses[i], transform.position + jointPoses[i] + moveDirection * 5f, Color.black);
                }
            }

            isGoingToWall = minDistance < 0.5f;
    }

    private Color GetDebugColor(float distance)
    {
        if (distance < 0.5f) return Color.red;
        if (distance < 3f) return Color.yellow;
        return Color.red;
    }

    */
}
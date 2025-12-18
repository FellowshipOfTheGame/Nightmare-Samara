using UnityEngine;
using UnityEngine.Playables;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State initialState;
    public State currentState;

    public Player player { get; private set; }

    private void Start()
    {
        player = GetComponent<Player>();
        this.Init(initialState);
    }

    public void Init(State state)
    {
        currentState = state;
        currentState.Enter(this.player);
    }

    public void ChangeState(State state)
    {
        currentState.Exit(this.player);
        currentState = state;
        currentState.Enter(this.player);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.FrameUpdate(this.player);
        }
    }

    public void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.PhysicsUpdate(this.player);
        }
    }
}

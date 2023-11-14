using UnityEngine;
using FSM;

public class Bot : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _resourceHolder;
    [SerializeField] private float _interactDistance;
    [SerializeField] private ConstructionYard _home;

    private StateMachine _stateMachine;
    private Resource _resource;
    private ConstructionYard _newYard;
    private Transform _newYardTransform;

    public Transform ResourceHolder => _resourceHolder;
    public float InteractDistance => _interactDistance;
    public ConstructionYard Home => _home;
    public Resource TargetResource => _resource;
    public ConstructionYard NewYard => _newYard;
    public Transform NewYardTransform => _newYardTransform;

    private void Awake()
    {
        _stateMachine = GetComponentInChildren<StateMachine>();

        if (_stateMachine == null)
            Debug.LogError($"Bot {name} couldn't find its State Machine.");
    }

    public void SetHome(ConstructionYard newHome)
    {
        _home = newHome;

        if (_home != null)
            _home.AddBot(this);
    }

    public void LookAt(Transform target)
    {
        if (target == null)
            return;

        Vector3 lookAtPosition = new(target.position.x, transform.position.y, target.position.z);

        transform.LookAt(lookAtPosition);
    }

    public void MoveTowards(Transform target)
    {
        if (target == null)
            return;

        float currentY = transform.position.y;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
        newPosition.y = currentY;

        transform.position = newPosition;
    }

    public void Gather(Resource resource)
    {
        _resource = resource;
        _stateMachine.ChangeState("Gather");
    }

    public void Build(ConstructionYard newYard, Transform newYardTransform)
    {
        _newYard = newYard;
        _newYardTransform = newYardTransform;

        _stateMachine.ChangeState("Build");
    }

    public void ClearGoals()
    {
        _resource = null;
        _newYard = null;
    }
}

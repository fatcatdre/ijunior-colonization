using UnityEngine;
using FSM;

[RequireComponent(typeof(BotGarage))]
[RequireComponent(typeof(ResourceSilo))]
[RequireComponent(typeof(ResourceScanner))]
[RequireComponent(typeof(ResourceGatherer))]
[RequireComponent(typeof(YardBuilder))]
public class ConstructionYard : MonoBehaviour, ISelectable, ICommandable
{
    [SerializeField] private GameObject _selectionRing;
    [SerializeField] private GameObject _collectionRing;
    [SerializeField] private GameObject _constructionFlag;

    private StateMachine _stateMachine;

    private BotGarage _garage;
    private ResourceSilo _silo;
    private ResourceScanner _scanner;
    private ResourceGatherer _gatherer;
    private YardBuilder _yardBuilder;

    public BotGarage Garage => _garage;
    public ResourceScanner Scanner => _scanner;
    public ResourceGatherer Gatherer => _gatherer;
    public YardBuilder YardBuilder => _yardBuilder;

    public int AvailableResources => _silo.AvailableResources;

    private void Awake()
    {
        _stateMachine = GetComponentInChildren<StateMachine>();

        if (_stateMachine == null)
            Debug.LogError($"Construction yard {name} couldn't find its State Machine.");

        _garage = GetComponent<BotGarage>();
        _silo = GetComponent<ResourceSilo>();
        _scanner = GetComponent<ResourceScanner>();
        _gatherer = GetComponent<ResourceGatherer>();
        _yardBuilder = GetComponent<YardBuilder>();

        Deselect();
    }

    private void Start()
    {
        _stateMachine.ChangeState("BuildBot");
    }

    public void Select()
    {
        _selectionRing.SetActive(true);
        _collectionRing.SetActive(true);
    }

    public void Deselect()
    {
        _selectionRing.SetActive(false);
        _collectionRing.SetActive(false);
    }

    public void Command(Command command)
    {
        Vector3 commandPosition = command.Position;

        if (Vector3.Distance(transform.position, commandPosition) < _scanner.ScanRadius)
            return;

        PlaceFlag(command.Position);

        _stateMachine.ChangeState("BuildYard");
    }

    private void PlaceFlag(Vector3 buildPosition)
    {
        _constructionFlag.transform.position = buildPosition;
        _constructionFlag.SetActive(true);
    }

    private void HideFlag()
    {
        _constructionFlag.SetActive(false);
        _constructionFlag.transform.position = Vector3.zero;
    }

    public void FinishBuilding()
    {
        _yardBuilder.FinishBuilding();
        HideFlag();

        _stateMachine.ChangeState("BuildBot");
    }

    public void Deposit(Bot bot, Resource resource)
    {
        _garage.AddBot(bot);
        _silo.Deposit(resource);
    }

    public void RequestResources(int count)
    {
        if (count > _silo.AvailableResources)
            return;

        _silo.Withdraw(count);
    }

    public void AddBot(Bot bot)
    {
        _garage.AddBot(bot);
    }

    public Bot RequestBot()
    {
        return _garage.RequestBot();
    }

    public Resource FindResource()
    {
        return _scanner.GetRandomResource();
    }
}

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RaycastSelector : MonoBehaviour
{
    private Camera _camera;
    private ISelectable _selected;

    private Ray ScreenPointRay { get => _camera.ScreenPointToRay(Input.mousePosition); }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            TrySelect();

        if (Input.GetKeyDown(KeyCode.Mouse1))
            TryCommand();
    }

    private void TrySelect()
    {
        if (Physics.Raycast(ScreenPointRay, out RaycastHit hit, _camera.farClipPlane) == false)
            return;

        if (hit.collider.TryGetComponent(out ISelectable selectable) == false)
        {
            _selected?.Deselect();
            _selected = null;
            return;
        }

        if (selectable == _selected)
        {
            selectable.Select();
            return;
        }

        _selected?.Deselect();
        _selected = selectable;
        _selected.Select();
    }

    private void TryCommand()
    {
        if (_selected == null || _selected is not ICommandable)
            return;

        if (Physics.Raycast(ScreenPointRay, out RaycastHit hit, _camera.farClipPlane) == false)
            return;

        (_selected as ICommandable).Command(new Command(hit.point));
    }
}

using UnityEngine;

public struct Command
{
    private Vector3 _position;

    public Vector3 Position => _position;

    public Command(Vector3 position)
    {
        _position = position;
    }
}

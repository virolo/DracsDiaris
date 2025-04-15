using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 _mousePosition;
    private bool _mouseDown;
    
    public Vector2 MousePosition { get { return _mousePosition; } }
    public bool MouseDown { get { return _mouseDown; } }
    
    private void Update()
    {
        _mousePosition = Input.mousePosition;
        _mouseDown = Input.GetMouseButtonDown(0);
    }
}

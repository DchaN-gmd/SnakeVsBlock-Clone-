using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    public Vector2 GetDirectionToClick(Vector2 headPosition)
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToViewportPoint(mousePosition);
        mousePosition.y = 1;
        mousePosition = Camera.main.ViewportToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - headPosition.x, mousePosition.y - headPosition.y);
        return direction;
    }
}

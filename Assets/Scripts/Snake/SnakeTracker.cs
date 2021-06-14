using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _offset;
    [SerializeField] private SnakeHead _head;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), _speed * Time.fixedDeltaTime);
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(transform.position.x, _head.transform.position.y + _offset, transform.position.z);
    }
}

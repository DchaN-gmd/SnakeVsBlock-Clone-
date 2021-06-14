using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SnakeTileGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] private int _countTiles;
    [SerializeField] private float _speed;
    [SerializeField] private float _tileSpringiness;
    
    private SnakeInput _input;
    private SnakeTileGenerator _tailGenerator;
    private List<Segment> _tail;

    public event UnityAction<int> SnakeSizeUpdated;

    private void Awake()
    {
        _tailGenerator = GetComponent<SnakeTileGenerator>();
        _input = GetComponent<SnakeInput>();

        _tail = _tailGenerator.Generate(_countTiles);  
    }

    private void Start() => SnakeSizeUpdated?.Invoke(_tail.Count);


    private void FixedUpdate()
    {
        Move(_head.transform.position + _head.transform.up * _speed*Time.fixedDeltaTime);

        _head.transform.up = _input.GetDirectionToClick(_head.transform.position);
    }

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollected += OnBonusCollected;
    }

    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollected -= OnBonusCollected;
    }

    private void Move(Vector2 nextPosition)
    {
        Vector2 previousPosition = _head.transform.position;

        foreach (var segment in _tail)
        {
            Vector2 tempPosition = segment.transform.position;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition, _tileSpringiness * Time.fixedDeltaTime);
            previousPosition = tempPosition;
        }
        _head.Move(nextPosition);
    }

    private void OnBlockCollided()
    {
        Segment deltedSegment = _tail[_tail.Count - 1];
        _tail.Remove(deltedSegment);
        Destroy(deltedSegment.gameObject);
        SnakeSizeUpdated?.Invoke(_tail.Count);
    }

    private void OnBonusCollected(int bonusSize)
    {
        _tail.AddRange(_tailGenerator.Generate(bonusSize));
        SnakeSizeUpdated?.Invoke(_tail.Count);
    }
}

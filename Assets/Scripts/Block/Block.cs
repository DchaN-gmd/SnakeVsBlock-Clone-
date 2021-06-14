using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int _destroyPriceRange;

    private int _destroyPrice;
    private int _filliing;

    public event UnityAction<int> FilliingUpdated;
    public int LeftToFill => _destroyPrice - _filliing;

    void Start()
    {
        _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y);
        FilliingUpdated?.Invoke(LeftToFill);
    }

    public void Fill()
    {
        _filliing++;
        FilliingUpdated?.Invoke(LeftToFill);

        if (_filliing==_destroyPrice)
        {
            Destroy(gameObject);
        }
    }
}

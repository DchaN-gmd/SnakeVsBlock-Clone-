using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private Vector2Int _bonusSizeRange;
    [SerializeField] private TMP_Text _view;

    private int _bonusSize;

    private void Awake()
    {
        _bonusSize = Random.Range(_bonusSizeRange.x, _bonusSizeRange.y);
        _view.text = _bonusSize.ToString();
    }

    public int Collect()
    {
        Destroy(gameObject);    
        return _bonusSize;
    }
}
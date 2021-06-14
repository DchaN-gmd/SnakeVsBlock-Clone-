using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Block))]
public class BlockViewer : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _text;

    private Block _block;

    void Awake()
    {
        _block = GetComponent<Block>();
    }

    private void OnEnable()
    {
        _block.FilliingUpdated += OnFillingUpdate;
    }

    private void OnDisable()
    {
        _block.FilliingUpdated -= OnFillingUpdate;
    }

    private void OnFillingUpdate(int leftToFill)
    {
        _text.text = leftToFill.ToString();
    }
}

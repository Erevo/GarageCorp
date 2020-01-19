using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.ForEditor;

public class Subject : MonoBehaviour
{
    [SerializeField]
    private byte _id = 0;

    [SerializeField]
    private float _speed = 1f;

    [SerializeField, ReadOnly]
    private Transform _hidePosition = null;

    [SerializeField, ReadOnly]
    private Transform _viewPosition = null;

    [SerializeField, ReadOnly]
    private bool _isVisible = false;

    [SerializeField, ReadOnly]
    private Vector3 _targetPosition = Vector3.zero;

    public byte Id
    {
        get => _id;
        set => _id = value;
    }

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            if (IsVisible)
                _targetPosition = ViewPosition;
            else
            {
                _targetPosition = HidePosition;
            }
        }
    }

    private Vector3 HidePosition
    {
        get => _hidePosition.position;
        set => _hidePosition.position = value;
    }
    private Vector3 ViewPosition
    {
        get => _viewPosition.position;
        set => _viewPosition.position = value;
    }

    private void Awake()
    {
        _viewPosition = GameObject.FindGameObjectWithTag("ViewPosition").transform;
        _hidePosition = GameObject.FindGameObjectWithTag("HidePosition").transform;

        IsVisible = false;
    }

    private void Update()
    {
        if (transform.position != _targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed);
        }
    }
}

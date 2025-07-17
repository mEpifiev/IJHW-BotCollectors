using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collector))]
public class Bot : MonoBehaviour
{
    [SerializeField] private float _collectionDelay = 0.5f;

    private Mover _mover;
    private Collector _collector;
    private Resource _currentResource;
    private Vector3 _resourceDropPoint;
    private bool _isAssigned;

    public bool IsAssigned => _isAssigned;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _collector = GetComponent<Collector>();
    }

    private void OnEnable()
    {
        _mover.DestinationReached += OnDestinationReached;
    }

    private void OnDisable()
    {
        _mover.DestinationReached -= OnDestinationReached;
    }

    public void AssignTask(Resource resource, Transform dropPoint)
    {
        if (_isAssigned || resource == null || dropPoint == null)
            return;

        _currentResource = resource;
        _resourceDropPoint = dropPoint.position;
        _isAssigned = true;

        _mover.MoveTo(resource.transform.position);
    }

    private void OnDestinationReached(Vector3 position)
    {
        if(_collector.HaveResource == false && _currentResource != null)
        {
            StartCoroutine(CollectResource());
        }
        else if(_collector.HaveResource)
        {
            _collector.Drop(position);
            _isAssigned = false;
        }
    }

    private IEnumerator CollectResource()
    {
        yield return new WaitForSeconds(_collectionDelay);

        if(_collector.TryCollect(_currentResource))
        {
            _mover.MoveTo(_resourceDropPoint);
            _currentResource = null;
        }
        else
        {
            _isAssigned = false;
        }
    }
}

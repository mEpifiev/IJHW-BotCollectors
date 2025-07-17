using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _scanRadius = 30f;
    [SerializeField] private float _delay = 3f;

    private List<Resource> _resources = new();

    public event Action<List<Resource>> ResourceScanned;

    private void Start()
    {
        StartCoroutine(ScanRoutine());
    }

    private void Scan()
    {
        if(_resources.Count != 0)
            _resources.Clear();

        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius, _layerMask);

        foreach (Collider hit in hits)
            if(hit.TryGetComponent(out Resource resource))
                _resources.Add(resource);
    }

    private IEnumerator ScanRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while(enabled)
        {
            yield return wait;

            Scan();
            ResourceScanned?.Invoke(_resources);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}

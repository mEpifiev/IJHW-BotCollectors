using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Transform _holder;

    private Resource _currentResource;

    public bool HaveResource => _currentResource != null;

    public bool TryCollect(Resource resource)
    {
        if (resource == null || _currentResource != null)
            return false;

        _currentResource = resource;
        _currentResource.DisablePhysics();
        _currentResource.transform.SetParent(_holder);
        _currentResource.transform.localPosition = Vector3.zero; 

        return true;
    }

    public void Drop(Vector3 newPosition)
    {
        if (_currentResource == null)
            return;

        _currentResource.transform.position = newPosition;
        _currentResource.transform.SetParent(null);
        _currentResource = null;
    }
}

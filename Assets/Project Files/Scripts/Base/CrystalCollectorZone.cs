using UnityEngine;

public class CrystalCollectorZone : MonoBehaviour
{
    [SerializeField] private CrystalCounter _crystalCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _crystalCounter.Add();
            resource.Release();
        }
    }
}

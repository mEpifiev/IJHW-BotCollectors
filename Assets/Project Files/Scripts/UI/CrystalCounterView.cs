using TMPro;
using UnityEngine;

[RequireComponent(typeof(CrystalCounter))]
public class CrystalCounterView : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;

    private CrystalCounter _counter;

    private void Awake()
    {
        _counter = GetComponent<CrystalCounter>();
    }

    private void OnEnable()
    {
        _counter.Changed += OnDisplayChanged;
    }

    private void OnDisable()
    {
        _counter.Changed -= OnDisplayChanged;
    }

    private void OnDisplayChanged(int count)
    {
        _view.text = count.ToString();
    }
}

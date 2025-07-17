using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private ResourceDispatcher _resourceDispatcher;
    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private Transform _dropResourcePoint;
    [SerializeField] private int _initialBotCount = 3;
    [SerializeField] private float _assignInterval = 0.5f;

    private List<Bot> _bots = new();

    private void Start()
    {
        _bots = _botSpawner.Spawn(_initialBotCount);

        StartCoroutine(AssignTaskBotRoutine());
    }

    private IEnumerator AssignTaskBotRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_assignInterval);

        while(enabled)
        {
            foreach(Bot bot in _bots)
            {
                if (bot.IsAssigned)
                    continue;

                Resource targetResurce = _resourceDispatcher.GetClosestResource(bot.transform.position);

                if (targetResurce != null)
                    bot.AssignTask(targetResurce, _dropResourcePoint);
            }

            yield return wait;
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class BotGarage : ConstructionYardModule
{
    [SerializeField] private int _botCost;
    [SerializeField] private Bot _prefab;
    [SerializeField] private Transform _botSpawnPoint;
    [SerializeField] private List<Bot> _availableBots;

    public int BotCost => _botCost;
    public bool IsBotAvailable => _availableBots.Count > 0;

    public void BuildBot()
    {
        if (_constructionYard.AvailableResources < _botCost)
            return;

        _constructionYard.RequestResources(_botCost);

        Bot bot = Instantiate(_prefab, _botSpawnPoint.position, _botSpawnPoint.rotation);
        bot.SetHome(_constructionYard);
    }

    public Bot RequestBot()
    {
        if (_availableBots.Count == 0)
            return null;

        Bot bot = _availableBots[Random.Range(0, _availableBots.Count)];

        _availableBots.Remove(bot);

        return bot;
    }

    public void AddBot(Bot bot)
    {
        if (bot == null)
            return;

        _availableBots.Add(bot);
    }
}

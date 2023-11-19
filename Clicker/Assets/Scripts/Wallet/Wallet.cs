using System;
using Zenject;

public class Wallet
{
    public event Action<int> CoinsChanged;

    private IPersistentData _persistentData;

    [Inject]
    public Wallet(IPersistentData persistentData) =>
        _persistentData = persistentData;

    public void AddCoin(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _persistentData.PlayerData.Money += coins;

        CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
    }

    public int GetCurrentCoins() => _persistentData.PlayerData.Money;

    public bool IsEnough(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        return _persistentData.PlayerData.Money >= coins;
    }

    public void Spend(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        _persistentData.PlayerData.Money -= coins;

        CoinsChanged?.Invoke(_persistentData.PlayerData.Money);
    }
}

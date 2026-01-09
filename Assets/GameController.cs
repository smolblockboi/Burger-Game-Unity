using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public PlayerData playerData;

    public List<BurgerData> burgerOrders;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeMoney(int amount)
    {
        playerData.money += amount;

        Debug.Log($"Player has ${playerData.money}");
    }

}

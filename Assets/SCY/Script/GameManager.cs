using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int coinCount = 0;
    public int deathCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void AddCoin(int amount = 1)
    {
        coinCount += amount;
        UIManager.Instance.UpdateCoinText(coinCount);
        Debug.Log("ÄÚÀÎ: " + coinCount);
    }

    public void AddDeath(int amount = 1)
    {
        deathCount += amount;
        Debug.Log("Á×À½: " + deathCount);
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI deathText;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            coinText.text = "Coins: " + GameManager.Instance.coinCount;
            deathText.text = "Deaths: " + GameManager.Instance.deathCount;
        }
        else
        {
            coinText.text = "Coins: N/A";
            deathText.text = "Deaths: N/A";
        }
    }
}
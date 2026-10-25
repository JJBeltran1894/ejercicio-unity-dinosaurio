using UnityEngine;
using TMPro; // Si usas Legacy Text (el texto clásico), cambia esta línea por: using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public static CoinUI instance;

    [Header("Referencias de UI")]
    [Tooltip("Arrastra aquí el objeto de texto TextMeshPro")]
    public TextMeshProUGUI coinText; 
    // Nota: Si usas UI > Legacy > Text, cambia la línea anterior por: public Text coinText;

    private int coinCount = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    // Método para sumar monedas
    public void AddCoin(int amount = 1)
    {
        coinCount += amount;
        UpdateUI();
    }

    // Actualiza el texto en pantalla
    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Monedas: " + coinCount;
        }
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
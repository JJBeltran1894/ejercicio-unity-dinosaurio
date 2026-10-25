using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Ajustes de Moneda")]
    public int coinValue = 1;

    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta si quien atravesó el trigger tiene el tag "Player"
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            // Notifica al contador de UI para sumar la moneda
            if (CoinUI.instance != null)
            {
                CoinUI.instance.AddCoin(coinValue);
            }

            // Destruye el objeto de la moneda de la escena
            Destroy(gameObject);
        }
    }
}
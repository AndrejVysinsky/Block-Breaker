using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TripleBallButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] PowerUpAddBall ballPowerUp;

    [SerializeField] GameObject buyPowerUpMenu;

    private int amount;

    void Start()
    {
        amount = ApplicationDataManager.Instance.GetNumberOfTripleBallPowerups();

        UpdateAmountText();
    }

    public void UpdateAmountText()
    {
        if (amount == 0)
        {
            amountText.text = "+";
        }
        else
        {
            amountText.text = amount.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (amount > 0)
        {
            ballPowerUp.ExternalActivation(3);
            
            amount--;
            UpdateAmountText();

            ApplicationDataManager.Instance.SetNumberOfTripleBallPowerUps(amount);
        }
        else
        {
            OpenShopMenu(); 
        }
    }

    private void OpenShopMenu()
    {
        Time.timeScale = 0f;
        buyPowerUpMenu.SetActive(true);
    }

    public void CloseShopMenu()
    {
        Time.timeScale = 1f;
        buyPowerUpMenu.SetActive(false);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShieldButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] PowerUpShield shieldPowerUp;

    [SerializeField] GameObject buyPowerUpMenu;

    private int amount;

    void Start()
    {
        amount = ApplicationDataManager.Instance.GetNumberOfShieldPowerUps();

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
            shieldPowerUp.ExternalActivation(20);

            amount--;
            UpdateAmountText();

            ApplicationDataManager.Instance.SetNumberOfShieldPowerUps(amount);
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

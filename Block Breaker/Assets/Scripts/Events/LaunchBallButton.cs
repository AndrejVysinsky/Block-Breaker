using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaunchBallButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI ballsText;

    private Player player;
    private GameController gameController;


    void Start()
    {
        player = FindObjectOfType<Player>();
        gameController = GameController.Instance;

        ballsText.text = gameController.Balls.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        player.LaunchBall();

        ballsText.text = gameController.Balls.ToString();
    }
}

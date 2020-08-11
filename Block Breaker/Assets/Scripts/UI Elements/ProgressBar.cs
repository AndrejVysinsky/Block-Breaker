using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour, IScoreChangedEvent
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject[] stars;
    [SerializeField] GameObject[] points;

    private float[] tresholds = new float[] {0.226f, 0.5f, 0.775f };

    private float currentValue;

    private void Start()
    {
        GameEventListeners.Instance.AddListener(gameObject);
    }

    public void OnScoreChanged(Vector3 position, int score)
    {
        currentValue += score;
        DisplayProgress();
    }

    private void DisplayProgress()
    {
        slider.value = Mathf.Clamp(currentValue, 0, slider.maxValue);

        float filledPart = slider.value / slider.maxValue;

        for (int i = 0; i < tresholds.Length; i++)
        {
            if (filledPart >= tresholds[i])
            {
                DisplayStar(i);
            }
            else
            {
                HideStar(i);
            }
        }
    }

    private void DisplayStar(int index)
    {
        stars[index].SetActive(true);
        points[index].SetActive(true);
    }

    private void HideStar(int index)
    {
        stars[index].SetActive(false);
        points[index].SetActive(false);
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }

    public int GetNumberOfStars()
    {
        for (int i = stars.Length - 1; i >= 0; i--)
        {
            if (stars[i].activeSelf)
            {
                return i + 1;
            }
        }
        return 0;
    }
}

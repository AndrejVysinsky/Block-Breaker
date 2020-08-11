using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] GameObject[] winScreenVariants;
    [SerializeField] GameObject[] activeStars;

    private int ownedStarsCount;
    private int earnedStarsCount;

    private Level level;

    private void Start()
    {
        level = Level.Instance;

        ActivateWinScreenVariant();

        ownedStarsCount = level.BestStars;
        earnedStarsCount = level.GetNumberOfStars() - ownedStarsCount;

        Debug.Log($"Level stars: {level.GetNumberOfStars()}\n Best: {level.BestStars}\n Earned: {earnedStarsCount}");

        if (earnedStarsCount < 0)
            earnedStarsCount = 0;

        SetupAnimator();
    }

    private void ActivateWinScreenVariant()
    {
        bool isNewRecord = level.Score > level.BestScore;

        if (isNewRecord)
        {
            winScreenVariants[0].SetActive(true);
        }
        else
        {
            winScreenVariants[1].SetActive(true);
        }
    }

    private void SetupAnimator()
    {
        Animator animator = GetComponent<Animator>();

        for (int i = ownedStarsCount + 1; i <= earnedStarsCount + ownedStarsCount; i++)
        {
            animator.SetBool($"star{i}earned", true);
        }

        animator.SetInteger("ownedStars", ownedStarsCount);
    }

    private void LateUpdate()
    {
        for (int i = 0; i < ownedStarsCount; i++)
        {
            activeStars[i].SetActive(true);
        }
    }
}
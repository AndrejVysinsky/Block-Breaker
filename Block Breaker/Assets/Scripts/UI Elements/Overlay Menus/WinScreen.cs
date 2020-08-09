using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] GameObject[] winScreenVariants;
    [SerializeField] GameObject[] activeStars;

    private int ownedStarsCount;
    private int earnedStarsCount;

    private void Start()
    {
        ActivateWinScreenVariant();
        
        ownedStarsCount = 1;
        earnedStarsCount = 2;

        SetupAnimator();
    }

    private void ActivateWinScreenVariant()
    {
        bool isNewRecord = true;

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
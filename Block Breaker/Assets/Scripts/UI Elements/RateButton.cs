using UnityEngine;

public class RateButton : MonoBehaviour
{
    public void RateAppRedirect()
    {
        Application.OpenURL(string.Format("market://details?id=" + Application.identifier));
    }
}

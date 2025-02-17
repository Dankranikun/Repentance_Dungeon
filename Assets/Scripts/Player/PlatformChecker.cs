using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    public GameObject joystick;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            joystick.SetActive(true);
        else joystick.SetActive(false);
    }
}

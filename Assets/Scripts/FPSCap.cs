using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class FPSCap : MonoBehaviour
{
    [SerializeField] private int framerate = 60;

    private void Start()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = framerate;
#endif

    }
}

using UnityEngine;

[ExecuteInEditMode]
public class FPSCap : MonoBehaviour
{
	[SerializeField] private int framerate = 60;

	private void Start()
	{
		QualitySettings.vSyncCount = 0; // Desactiva VSync
		Application.targetFrameRate = framerate; // Limita los FPS a 'framerate'
	}
}

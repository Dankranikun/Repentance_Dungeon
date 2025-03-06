using UnityEngine;

public class PauseManager : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu = null;
	private bool isPaused = false;

	void Start()
	{
		if (pauseMenu != null)
		{
			pauseMenu.SetActive(false); // Asegurar que el menú está oculto al inicio
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePause();
		}
	}

	public void TogglePause()
	{
		isPaused = !isPaused;
		pauseMenu.SetActive(isPaused);

		if (isPaused)
		{
			Time.timeScale = 0;
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPaused = true; // Pausar también el editor
#endif
		}
		else
		{
			ResumeGame();
		}
	}

	public void ResumeGame()
	{
		isPaused = false;
		Time.timeScale = 1; // Reanudar el tiempo
		pauseMenu.SetActive(false);

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPaused = false; // Reanudar en el editor
#endif
	}

	public void QuitGame()
	{
		Time.timeScale = 1; // Asegurar que el tiempo vuelve a la normalidad antes de salir
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false; // Detener el modo Play en Unity
#else
        Application.Quit(); // Cerrar el juego en la build
#endif
	}
}

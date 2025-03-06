using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
	public GameObject gameOverMenu; // Asigna el GameOverMenu en el Inspector
	public GameObject canvas; // Asigna el Canvas en el Inspector

	private void Start()
	{
		gameOverMenu.SetActive(false); // Asegura que el menú está oculto al inicio
	}

	public void ShowGameOver()
	{
		// Desactiva todos los hijos de Canvas EXCEPTO GameOverMenu
		foreach (Transform child in canvas.transform)
		{
			if (child.gameObject != gameOverMenu)
			{
				child.gameObject.SetActive(false);
			}
		}

		// Activa el menú de Game Over
		gameOverMenu.SetActive(true);
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu"); // Cambia a la escena del menú principal
	}

	public void QuitGame()
	{
		Application.Quit(); // Cierra el juego
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false; // Para detener en el editor
#endif
	}
}

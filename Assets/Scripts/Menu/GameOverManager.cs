using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
	public GameObject canvasGameOver; // Asigna el Canvas de Game Over en el Inspector

	private void Start()
	{
		if (canvasGameOver == null)
		{
			Debug.LogError("❌ CanvasGameOver no está asignado en el Inspector.");
		}
		else
		{
			canvasGameOver.SetActive(false); // Asegura que esté oculto al inicio
		}
	}

	public void ShowGameOver()
	{
		if (canvasGameOver == null)
		{
			Debug.LogError("❌ CanvasGameOver no está asignado en el Inspector.");
			return;
		}

		Debug.Log("✅ Activando pantalla de Game Over...");
		canvasGameOver.SetActive(true);

		// 🔹 Asegurar que el Canvas es visible si tiene CanvasGroup
		CanvasGroup cg = canvasGameOver.GetComponent<CanvasGroup>();
		if (cg != null)
		{
			cg.alpha = 1;
			cg.interactable = true;
			cg.blocksRaycasts = true;
		}
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu"); // Carga la escena del menú principal
	}

	public void QuitGame()
	{
		Application.Quit(); // Cierra el juego
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false; // Para detener en el editor
#endif
	}
}

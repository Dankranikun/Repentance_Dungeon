using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void StartGame()
	{
		StartCoroutine(LoadGameScenes());
	}

	IEnumerator LoadGameScenes()
	{
		// Cargar GameManagerScene (LoadSceneMode.Single descarga MainMenu automáticamente)
		AsyncOperation gameManagerLoad = SceneManager.LoadSceneAsync("GameManagerScene", LoadSceneMode.Single);
		yield return gameManagerLoad;

		Debug.Log("🎮 Juego cargado correctamente");
	}

	public void CloseGame()
	{
		Application.Quit();
	}

	public void ToggleFullScreen()
	{
		Screen.fullScreen = !Screen.fullScreen;
	}
}
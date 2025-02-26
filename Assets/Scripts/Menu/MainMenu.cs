using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Cargar GameManagerScene si no está ya cargada
        if (!SceneManager.GetSceneByName("GameManagerScene").isLoaded)
        {
            SceneManager.LoadScene("GameManagerScene", LoadSceneMode.Additive);
        }

        // Cargar la primera sala
        SceneManager.LoadScene("room0", LoadSceneMode.Additive);

        // Asegurar que MainMenu se elimina completamente
        StartCoroutine(UnloadMainMenu());
    }

    IEnumerator UnloadMainMenu()
    {
        yield return new WaitForSeconds(0.5f); // Pequeña espera para asegurar que las nuevas escenas cargan primero
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}

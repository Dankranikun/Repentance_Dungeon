using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NextMenu(){
        SceneManager.LoadSceneAsync(1);
    }

}

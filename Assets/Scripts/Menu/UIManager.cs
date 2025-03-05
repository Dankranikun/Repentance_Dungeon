using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	public static UIManager instance; // Singleton para acceso global a la UI
	public TextMeshProUGUI collectedPickUps; // Referencia al texto de UI

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject); // La UI persiste entre escenas
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void UpdateUI(int coins, int keys, int bombs)
	{
		if (collectedPickUps != null)
		{
			collectedPickUps.text = "Coins: " + coins + "\nKeys: " + keys + "\nBombs: " + bombs;
		}
	}
}

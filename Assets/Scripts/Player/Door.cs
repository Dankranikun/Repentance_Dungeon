using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	public string[] possibleRooms = { "room0", "room1", "room2" }; // Salas disponibles
	public Vector3 spawnPosition; // Posición donde aparecerá el jugador en la nueva sala

	private static bool isTransitioning = false; // Evita múltiples activaciones
	private static string currentRoom = "room0"; // Guarda la habitación actual

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !isTransitioning)
		{
			isTransitioning = true; // Bloquea nuevas activaciones

			// Elegir una sala aleatoria distinta a la actual
			string newRoom;
			do
			{
				newRoom = possibleRooms[Random.Range(0, possibleRooms.Length)];
			} while (newRoom == currentRoom);

			// Guardar la posición del jugador antes de cambiar de sala
			PlayerPrefs.SetFloat("SpawnX", spawnPosition.x);
			PlayerPrefs.SetFloat("SpawnY", spawnPosition.y);
			PlayerPrefs.SetFloat("SpawnZ", spawnPosition.z);

			// Cargar la nueva sala
			SceneManager.LoadScene(newRoom, LoadSceneMode.Additive);

			// Mover al jugador a la nueva posición justo después de cargar
			StartCoroutine(SetPlayerPositionAfterLoad(other.gameObject));

			// Iniciar una corrutina para descargar la habitación anterior
			StartCoroutine(UnloadPreviousRoom(currentRoom));

			// Actualizar la habitación actual
			currentRoom = newRoom;
			Debug.Log("🚪 Tocando la puerta: " + gameObject.name + " | Colisión con: " + other.name);

			if (other.CompareTag("Player") && !isTransitioning)
			{
				Debug.Log("🔄 Iniciando teletransporte...");
			}
		}
	}

	// Nueva corrutina para mover al jugador después de cargar la sala
	private System.Collections.IEnumerator SetPlayerPositionAfterLoad(GameObject player)
	{
		yield return new WaitForSeconds(0.01f); // Espera un poco para asegurarse de que la escena ha cargado

		if (PlayerPrefs.HasKey("SpawnX"))
		{
			float x = PlayerPrefs.GetFloat("SpawnX");
			float y = PlayerPrefs.GetFloat("SpawnY");
			float z = PlayerPrefs.GetFloat("SpawnZ");
			player.transform.position = new Vector3(x, y, z);
			Debug.Log("📍 Posición antes del cambio: " + player.transform.position);
			player.transform.position = new Vector3(x, y, z);
			Debug.Log("📍 Nueva posición del jugador: " + player.transform.position);
		}
		else
		{
			Debug.LogWarning("⚠ No se encontró la posición guardada en PlayerPrefs.");
		}
		isTransitioning = false; // Permitir nuevas transiciones

	}

	private System.Collections.IEnumerator UnloadPreviousRoom(string roomName)
	{
		yield return new WaitForSeconds(0.0001f); // Espera para evitar fallos de carga
		SceneManager.UnloadSceneAsync(roomName); // Descarga la habitación anterior
		isTransitioning = false; // Permite nuevas transiciones
	}
}

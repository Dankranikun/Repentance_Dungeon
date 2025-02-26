using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string[] possibleRooms = { "room0", "room1" }; // Salas disponibles
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

            // Guardar la posición del jugador
            PlayerPrefs.SetFloat("SpawnX", spawnPosition.x);
            PlayerPrefs.SetFloat("SpawnY", spawnPosition.y);
            PlayerPrefs.SetFloat("SpawnZ", spawnPosition.z);

            // Asegurar que GameManagerScene sigue cargado
            if (!SceneManager.GetSceneByName("GameManagerScene").isLoaded)
            {
                SceneManager.LoadScene("GameManagerScene", LoadSceneMode.Additive);
            }

            // Cargar la nueva sala
            SceneManager.LoadScene(newRoom, LoadSceneMode.Additive);

            // Iniciar una corrutina para descargar la habitación anterior después de la carga
            StartCoroutine(UnloadPreviousRoom(currentRoom));

            // Actualizar la habitación actual
            currentRoom = newRoom;
        }
    }

    private System.Collections.IEnumerator UnloadPreviousRoom(string roomName)
    {
        yield return new WaitForSeconds(1f); // Espera para evitar fallos de carga
        SceneManager.UnloadSceneAsync(roomName); // Descarga la habitación anterior
        isTransitioning = false; // Permite nuevas transiciones
    }
}

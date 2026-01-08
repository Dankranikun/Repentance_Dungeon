using UnityEngine;

public class Door : MonoBehaviour
{
	[Header("Door Configuration")]
	public DoorDirection direction; // Norte, Sur, Este, Oeste
	public Vector3 playerSpawnOffset = Vector3.zero; // Offset desde el centro de la sala

	private bool isTransitioning = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !isTransitioning)
		{
			StartCoroutine(TransitionToRoom(other.gameObject));
		}
	}

	private System.Collections.IEnumerator TransitionToRoom(GameObject player)
	{
		isTransitioning = true;

		Debug.Log($"🚪 Usando puerta {direction}");

		// Generar la sala adyacente
		Vector2Int newRoomCoords = DungeonManager.Instance.GenerateAdjacentRoom(direction);

		// Esperar un frame para asegurar que la sala se ha instanciado
		yield return null;

		// Calcular la nueva posición del jugador
		Vector3 newRoomWorldPos = DungeonManager.Instance.GetWorldPosition(newRoomCoords);
		Vector3 spawnOffset = GetSpawnOffsetForDirection();
		Vector3 newPlayerPosition = newRoomWorldPos + spawnOffset + playerSpawnOffset;

		// Mover al jugador
		CharacterController controller = player.GetComponent<CharacterController>();
		if (controller != null)
		{
			// Si usa CharacterController, desactivarlo temporalmente
			controller.enabled = false;
			player.transform.position = newPlayerPosition;
			controller.enabled = true;
		}
		else
		{
			// Si no, mover directamente
			player.transform.position = newPlayerPosition;
		}

		Debug.Log($"📍 Jugador movido a: {newPlayerPosition}");

		// Mover la cámara si es necesario
		Camera mainCamera = Camera.main;
		if (mainCamera != null)
		{
			mainCamera.transform.position = new Vector3(
				newRoomWorldPos.x,
				mainCamera.transform.position.y,
				newRoomWorldPos.z
			);
		}

		yield return new WaitForSeconds(0.3f);
		isTransitioning = false;
	}

	/// <summary>
	/// Devuelve el offset de spawn según la dirección de la puerta
	/// (Jugador aparece en el lado opuesto de la nueva sala)
	/// </summary>
	private Vector3 GetSpawnOffsetForDirection()
	{
		float distance = 8f; // Distancia desde el centro

		switch (direction)
		{
			case DoorDirection.North:
				return new Vector3(0, 0, -distance); // Entra por el sur
			case DoorDirection.South:
				return new Vector3(0, 0, distance); // Entra por el norte
			case DoorDirection.East:
				return new Vector3(-distance, 0, 0); // Entra por el oeste
			case DoorDirection.West:
				return new Vector3(distance, 0, 0); // Entra por el este
			default:
				return Vector3.zero;
		}
	}

	// Visualización en el editor
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Vector3 arrowDirection = Vector3.zero;

		switch (direction)
		{
			case DoorDirection.North:
				arrowDirection = Vector3.forward;
				break;
			case DoorDirection.South:
				arrowDirection = Vector3.back;
				break;
			case DoorDirection.East:
				arrowDirection = Vector3.right;
				break;
			case DoorDirection.West:
				arrowDirection = Vector3.left;
				break;
		}

		Gizmos.DrawRay(transform.position, arrowDirection * 2f);
		Gizmos.DrawSphere(transform.position + arrowDirection * 2f, 0.3f);
	}
}
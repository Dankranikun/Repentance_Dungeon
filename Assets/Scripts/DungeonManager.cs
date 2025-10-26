using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
	public static DungeonManager Instance { get; private set; }

	[Header("Room Settings")]
	public RoomData[] roomPrefabs; // Tus prefabs de salas
	public float roomWidth = 20f; // Ancho de cada sala
	public float roomHeight = 20f; // Alto de cada sala

	[Header("Player Settings")]
	public GameObject playerPrefab; // Arrastra aquí el prefab del jugador
	public Vector3 playerSpawnOffset = new Vector3(0, 1, 0); // Offset desde el suelo

	[Header("Camera Settings")]
	public Vector3 cameraPosition = new Vector3(0, 50, -11);
	public Vector3 cameraRotation = new Vector3(78, 0, 0);

	[Header("Current State")]
	public Vector2Int currentRoomCoords = Vector2Int.zero;

	// Grid que almacena las salas generadas
	private Dictionary<Vector2Int, GameObject> loadedRooms = new Dictionary<Vector2Int, GameObject>();

	// Referencias
	private GameObject currentRoom;
	private GameObject playerInstance; // Referencia al jugador instanciado
	private Camera mainCamera;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		SetupCamera();

		// Generar la sala inicial (0,0)
		GenerateRoom(Vector2Int.zero);

		// Instanciar al jugador en el centro de esa sala
		Vector3 playerSpawnPosition = GetWorldPosition(Vector2Int.zero) + playerSpawnOffset;
		playerInstance = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
		playerInstance.name = "Player";

		Debug.Log($"🎮 Jugador instanciado en: {playerSpawnPosition}");
	}

	private void SetupCamera()
	{
		mainCamera = Camera.main;
		if (mainCamera != null)
		{
			mainCamera.transform.position = cameraPosition;
			mainCamera.transform.rotation = Quaternion.Euler(cameraRotation);
			Debug.Log("✅ Cámara configurada correctamente.");
		}
		else
		{
			Debug.LogWarning("❌ No se encontró la cámara en la escena.");
		}
	}

	/// <summary>
	/// Genera una sala en las coordenadas especificadas del grid
	/// </summary>
	public void GenerateRoom(Vector2Int coords)
	{
		// Si ya existe esta sala, no la generamos de nuevo
		if (loadedRooms.ContainsKey(coords))
		{
			currentRoomCoords = coords;
			currentRoom = loadedRooms[coords];
			return;
		}

		// Seleccionar un prefab aleatorio
		RoomData randomRoomData = roomPrefabs[Random.Range(0, roomPrefabs.Length)];

		// Calcular la posición en el mundo
		Vector3 worldPosition = new Vector3(
			coords.x * roomWidth,
			0,
			coords.y * roomHeight
		);

		// Instanciar la sala
		GameObject newRoom = Instantiate(randomRoomData.roomPrefab, worldPosition, Quaternion.identity);
		newRoom.name = $"Room_{coords.x}_{coords.y}";

		// Guardarla en el diccionario
		loadedRooms[coords] = newRoom;
		currentRoomCoords = coords;
		currentRoom = newRoom;

		Debug.Log($"🏠 Sala generada en {coords} | Posición: {worldPosition}");
	}

	/// <summary>
	/// Genera la sala adyacente en la dirección especificada
	/// </summary>
	public Vector2Int GenerateAdjacentRoom(DoorDirection direction)
	{
		Vector2Int newCoords = currentRoomCoords;

		switch (direction)
		{
			case DoorDirection.North:
				newCoords += Vector2Int.up;
				break;
			case DoorDirection.South:
				newCoords += Vector2Int.down;
				break;
			case DoorDirection.East:
				newCoords += Vector2Int.right;
				break;
			case DoorDirection.West:
				newCoords += Vector2Int.left;
				break;
		}

		GenerateRoom(newCoords);
		return newCoords;
	}

	/// <summary>
	/// Obtiene la posición en el mundo para unas coordenadas del grid
	/// </summary>
	public Vector3 GetWorldPosition(Vector2Int coords)
	{
		return new Vector3(coords.x * roomWidth, 0, coords.y * roomHeight);
	}

	/// <summary>
	/// Obtiene la sala en las coordenadas especificadas
	/// </summary>
	public GameObject GetRoom(Vector2Int coords)
	{
		if (loadedRooms.ContainsKey(coords))
			return loadedRooms[coords];
		return null;
	}

	/// <summary>
	/// Obtiene la referencia del jugador instanciado
	/// </summary>
	public GameObject GetPlayer()
	{
		return playerInstance;
	}

	/// <summary>
	/// Obtiene la referencia de la cámara principal
	/// </summary>
	public Camera GetCamera()
	{
		return mainCamera;
	}

	/// <summary>
	/// Limpia todas las salas (útil para reiniciar el dungeon)
	/// </summary>
	public void ClearAllRooms()
	{
		foreach (var room in loadedRooms.Values)
		{
			Destroy(room);
		}
		loadedRooms.Clear();
		currentRoomCoords = Vector2Int.zero;
	}
}
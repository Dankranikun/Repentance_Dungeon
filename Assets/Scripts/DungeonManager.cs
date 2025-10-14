using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance { get; private set; }

    [Header("Room Settings")]
    public RoomData[] roomPrefabs; // Tus prefabs de salas
    public float roomWidth = 20f; // Ancho de cada sala
    public float roomHeight = 20f; // Alto de cada sala

    [Header("Current State")]
    public Vector2Int currentRoomCoords = Vector2Int.zero;

    // Grid que almacena las salas generadas
    private Dictionary<Vector2Int, GameObject> loadedRooms = new Dictionary<Vector2Int, GameObject>();

    // Sala actual
    private GameObject currentRoom;

    [Header("Player Settings")]
    public GameObject playerPrefab; // Arrastra aquí el prefab del jugador
    public Vector3 playerSpawnOffset = new Vector3(0, 1, 0); // Offset desde el suelo

    private GameObject playerInstance; // Referencia al jugador instanciado

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
        // 1. Generar la sala inicial (0,0)
        GenerateRoom(Vector2Int.zero);

        // 2. Instanciar al jugador en el centro de esa sala
        Vector3 playerSpawnPosition = GetWorldPosition(Vector2Int.zero) + playerSpawnOffset;
        playerInstance = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
        playerInstance.name = "Player";

        Debug.Log($"🎮 Jugador instanciado en: {playerSpawnPosition}");
    }
    public void GenerateRoom(Vector2Int coords)
    {
        // 1. VERIFICAR SI YA EXISTE
        // Si ya generamos esta sala antes, no la volvemos a crear
        if (loadedRooms.ContainsKey(coords))
        {
            currentRoomCoords = coords;
            currentRoom = loadedRooms[coords];
            return; // Salir, no hacer nada más
        }

        // 2. SELECCIONAR PREFAB ALEATORIO
        // Elige un prefab random del array roomPrefabs
        RoomData randomRoomData = roomPrefabs[Random.Range(0, roomPrefabs.Length)];

        // 3. CALCULAR POSICIÓN EN EL MUNDO
        // Convierte coordenadas del grid a posición 3D
        // Ejemplo: (1, 0) con roomWidth=20 → (20, 0, 0)
        //          (0, 1) con roomHeight=20 → (0, 0, 20)
        Vector3 worldPosition = new Vector3(
            coords.x * roomWidth,  // Posición X
            0,                     // Altura Y (siempre 0)
            coords.y * roomHeight  // Posición Z
        );

        // 4. INSTANCIAR LA SALA
        // Crea el GameObject en la escena
        GameObject newRoom = Instantiate(
            randomRoomData.roomPrefab,  // Qué prefab
            worldPosition,               // Dónde
            Quaternion.identity          // Sin rotación
        );

        // Le ponemos un nombre descriptivo
        newRoom.name = $"Room_{coords.x}_{coords.y}";

        // 5. GUARDAR EN EL DICCIONARIO
        // Para no volverla a crear si volvemos a esta coordenada
        loadedRooms[coords] = newRoom;

        // 6. ACTUALIZAR ESTADO ACTUAL
        currentRoomCoords = coords;
        currentRoom = newRoom;

        Debug.Log($"🏠 Sala generada en {coords} | Posición: {worldPosition}");
    }
public GameObject GetPlayer()
{
    return playerInstance;
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


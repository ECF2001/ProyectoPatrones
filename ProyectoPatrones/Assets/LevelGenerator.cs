using UnityEngine;
using System.Collections.Generic;

// Clase para representar una habitación
public class Room
{
    public int x, y, width, height;


    public Room(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }
}

// Prototype para clonar habitaciones
public class RoomPrototype : Room
{
    public RoomPrototype(int x, int y, int width, int height) : base(x, y, width, height) { }

    // Clonar una habitación con la misma configuración
    public RoomPrototype Clone()
    {
        return new RoomPrototype(this.x, this.y, this.width, this.height);
    }
}

// Builder para la creación del nivel
public class LevelBuilder
{
    private LevelGenerator _levelGenerator;
    private int _gridWidth;
    private int _gridHeight;
    private List<Room> _rooms;

    public LevelBuilder(LevelGenerator levelGenerator, int gridWidth, int gridHeight)
    {
        _levelGenerator = levelGenerator;
        _gridWidth = gridWidth;
        _gridHeight = gridHeight;
        _rooms = new List<Room>();
    }

    public void BuildRooms(int numberOfRooms)
    {
        for (int i = 0; i < numberOfRooms; i++)
        {
            CreateRoom();
        }
    }

    public void ConnectRooms()
    {
        for (int i = 0; i < _rooms.Count - 1; i++)
        {
            _levelGenerator.CreatePassage(_rooms[i], _rooms[i + 1]);
        }
    }

    private void CreateRoom()
    {
        // Crear una habitación prototipo (habitaciones estándar que se clonarán)
        RoomPrototype roomPrototype = new RoomPrototype(0, 0, 12, 8);

        // Si es la primera habitación, colocarla en el centro de la cámara
        Room clonedRoom;
        if (_rooms.Count == 0)
        {
            // Obtener la posición de la cámara principal
            Camera mainCamera = Camera.main;
            float cameraCenterX = mainCamera.transform.position.x;
            float cameraCenterY = mainCamera.transform.position.y;

            // Clonar la habitación prototipo y colocarla centrada
            clonedRoom = roomPrototype.Clone();
            clonedRoom.x = Mathf.FloorToInt(cameraCenterX - clonedRoom.width / 2);  // Centrado en X
            clonedRoom.y = Mathf.FloorToInt(cameraCenterY - clonedRoom.height / 2); // Centrado en Y
        }
        else
        {
            // Clonar la habitación de forma aleatoria para las siguientes habitaciones
            clonedRoom = roomPrototype.Clone();
            clonedRoom.x = Random.Range(0, _gridWidth - clonedRoom.width);
            clonedRoom.y = Random.Range(0, _gridHeight - clonedRoom.height);
        }

        _rooms.Add(clonedRoom);
        _levelGenerator.CreateRoom(clonedRoom);  // Aquí pasamos la habitación clonada
    }

}


// Generador del nivel (ajustado para evitar spawn fuera de habitaciones)
public class LevelGenerator : MonoBehaviour
{
    public GameObject sueloPrefab;
    public GameObject muroPrefab;
    public int gridWidth = 50;
    public int gridHeight = 50;
    public int numberOfRooms = 3;
    public ObjectPool enemyPool;

    private List<Room> rooms = new List<Room>();
    private List<Vector2> sueloPositions = new List<Vector2>(); // NUEVO: registrar suelo válido

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        rooms.Clear();
        sueloPositions.Clear();

        LevelBuilder builder = new LevelBuilder(this, gridWidth, gridHeight);
        builder.BuildRooms(numberOfRooms);
        builder.ConnectRooms();

        SpawnEnemiesInRooms();
    }

    public void CreateRoom(Room room)
    {
        int x = room.x;
        int y = room.y;
        int roomWidth = room.width;
        int roomHeight = room.height;



        for (int i = 0; i < roomWidth; i++)
        {
            for (int j = 0; j < roomHeight; j++)
            {
                Vector2 tilePos = new Vector2(x + i + 0.5f, y + j + 0.5f);
                GameObject sueloInst = Instantiate(sueloPrefab, tilePos, Quaternion.identity);
                sueloInst.GetComponent<SpriteRenderer>().sortingOrder = 1;
                sueloPositions.Add(tilePos); //  registrar suelo válido
            }
        }



        for (int i = 0; i < roomWidth + 2; i++)
        {
            if (!IsPassage(x + i - 1, y - 1))
                CreateWall(x + i - 1, y - 1);

            if (!IsPassage(x + i - 1, y + roomHeight))
                CreateWall(x + i - 1, y + roomHeight);
        }

        for (int j = 0; j < roomHeight + 2; j++)
        {
            if (!IsPassage(x - 1, y + j - 1))
                CreateWall(x - 1, y + j - 1);

            if (!IsPassage(x + roomWidth, y + j - 1))
                CreateWall(x + roomWidth, y + j - 1);
        }
    }

    void SpawnEnemiesInRooms()
    {
        int enemiesPerRoom = 3;
        int margin = 1;

        foreach (Room room in rooms)
        {
            List<Vector2> validPositions = new List<Vector2>();

            for (int i = room.x + margin; i < room.x + room.width - margin; i++)
            {
                for (int j = room.y + margin; j < room.y + room.height - margin; j++)
                {
                    Vector2 pos = new Vector2(i + 0.5f, j + 0.5f);

                    // 1. Verificar que la posición esté en sueloPositions (garantiza tile pintado)
                    if (!sueloPositions.Contains(pos))
                        continue;

                    // 2. Verificar colisiones
                    Collider2D hit = Physics2D.OverlapCircle(pos, 0.4f, LayerMask.GetMask("Muro", "Enemy"));
                    if (hit == null)
                    {
                        validPositions.Add(pos);
                    }
                }
            }

            // Mezclar para aleatoriedad
            for (int i = 0; i < validPositions.Count; i++)
            {
                Vector2 temp = validPositions[i];
                int randIndex = Random.Range(i, validPositions.Count);
                validPositions[i] = validPositions[randIndex];
                validPositions[randIndex] = temp;
            }

            // Spawnear
            int spawned = 0;
            foreach (Vector2 pos in validPositions)
            {
                if (spawned >= enemiesPerRoom) break;

                GameObject enemy = enemyPool.GetEnemy();
                if (enemy != null)
                {
                    enemy.SetActive(false); // Asegura que OnEnable se reinicie limpio
                    enemy.transform.position = pos;
                    enemy.SetActive(true);
                    spawned++;
                }
            }

            if (spawned < enemiesPerRoom)
            {
                Debug.LogWarning($"No se pudieron generar los {enemiesPerRoom} enemigos en la habitación ({room.x}, {room.y})");
            }
        }
    }


    public bool IsRoomValid(int x, int y, int width, int height)
    {
        foreach (Room room in rooms)
        {
            if (x < room.x + room.width && x + width > room.x && y < room.y + room.height && y + height > room.y)
                return false;
        }
        return true;
    }

    public bool IsPassage(int x, int y)
    {
        foreach (Room room in rooms)
        {
            if (x >= room.x && x < room.x + room.width && y >= room.y && y < room.y + room.height)
                return true;
        }
        return false;
    }

   public void CreatePassage(Room roomA, Room roomB)
{
    int x1 = roomA.x + roomA.width / 2;
    int y1 = roomA.y + roomA.height / 2;
    int x2 = roomB.x + roomB.width / 2;
    int y2 = roomB.y + roomB.height / 2;

    while (y1 != y2)
    {
        Vector2 pos1 = new Vector2(x1 + 0.5f, y1 + 0.5f);
        sueloPositions.Add(pos1);
        Instantiate(sueloPrefab, pos1, Quaternion.identity);

        Vector2 pos2 = new Vector2(x1 + 1 + 0.5f, y1 + 0.5f);
        sueloPositions.Add(pos2);
        Instantiate(sueloPrefab, pos2, Quaternion.identity);

        Vector2 pos3 = new Vector2(x1 + 2 + 0.5f, y1 + 0.5f);
        sueloPositions.Add(pos3);
        Instantiate(sueloPrefab, pos3, Quaternion.identity);

        CreateWall(x1 - 1, y1);
        CreateWall(x1 + 3, y1);

        y1 += (y2 > y1) ? 1 : -1;
    }

    while (x1 != x2)
    {
        Vector2 pos1 = new Vector2(x1 + 0.5f, y1 + 0.5f);
        sueloPositions.Add(pos1);
        Instantiate(sueloPrefab, pos1, Quaternion.identity);

        Vector2 pos2 = new Vector2(x1 + 0.5f, y1 + 1 + 0.5f);
        sueloPositions.Add(pos2);
        Instantiate(sueloPrefab, pos2, Quaternion.identity);

        Vector2 pos3 = new Vector2(x1 + 0.5f, y1 + 2 + 0.5f);
        sueloPositions.Add(pos3);
        Instantiate(sueloPrefab, pos3, Quaternion.identity);

        CreateWall(x1, y1 - 1);
        CreateWall(x1, y1 + 3);

        x1 += (x2 > x1) ? 1 : -1;
    }
}


    void CreateWall(int x, int y)
    {
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                int wx = x + i;
                int wy = y + j;

                if (!IsPassage(wx, wy))
                {
                    Vector2 wallPos = new Vector2(wx + 0.5f, wy + 0.5f);

                    //  Elimina la posición de suelo si está ocupada por un muro
                    if (sueloPositions.Contains(wallPos))
                        sueloPositions.Remove(wallPos);

                    GameObject muroInst = Instantiate(muroPrefab, new Vector3(wx, wy, 0), Quaternion.identity);
                    muroInst.layer = LayerMask.NameToLayer("Muro");
                    muroInst.AddComponent<BoxCollider2D>().isTrigger = false;
                    muroInst.GetComponent<SpriteRenderer>().sortingOrder = -1;
                }
            }
        }
    }
}


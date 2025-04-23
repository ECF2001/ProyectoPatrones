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
            CreateRoom();
    }

    public void ConnectRooms()
    {
        for (int i = 0; i < _rooms.Count - 1; i++)
            _levelGenerator.CreatePassage(_rooms[i], _rooms[i + 1]);
    }
        
    private void CreateRoom()
    {
        RoomPrototype roomPrototype = new RoomPrototype(0, 0, 20, 14);
        Room clonedRoom;

        if (_rooms.Count == 0)
        {
            // Obtener la posici�n de la c�mara principal
            Camera mainCamera = Camera.main;
            float cameraCenterX = mainCamera.transform.position.x;
            float cameraCenterY = mainCamera.transform.position.y;

            // Clonar la habitaci�n prototipo y colocarla centrada
            clonedRoom = roomPrototype.Clone();
            clonedRoom.x = Mathf.FloorToInt(cameraCenterX - clonedRoom.width / 2);
            clonedRoom.y = Mathf.FloorToInt(cameraCenterY - clonedRoom.height / 2);
        }
        else
        {
            clonedRoom = roomPrototype.Clone();
            clonedRoom.x = Random.Range(0, _gridWidth - clonedRoom.width);
            clonedRoom.y = Random.Range(0, _gridHeight - clonedRoom.height);
        }

        _rooms.Add(clonedRoom);
        _levelGenerator.CreateRoom(clonedRoom);
    }
}

// ----------------------------------------------------------
// GENERADOR DE NIVEL
// ----------------------------------------------------------
public class LevelGenerator : MonoBehaviour
{
    public GameObject sueloPrefab;
    public GameObject muroPrefab;
    public GameObject healthPotionPrefab;
    public ObjectPool enemyPool;
    public SecondEnemyPool secondEnemyPool;

    public int gridWidth = 50;
    public int gridHeight = 50;
    public int numberOfRooms = 3;

    private List<Room> rooms = new List<Room>();
    private List<Vector2> sueloPositions = new List<Vector2>();

    void Start() => GenerateLevel();

        void Start()
        {
            GenerateLevel();  // Llamamos a la funci�n para generar el nivel al iniciar
        }

        void GenerateLevel()
        {
            rooms.Clear(); // Limpiar la lista de habitaciones

            // Crear las habitaciones con el patr�n Builder
            LevelBuilder builder = new LevelBuilder(this, gridWidth, gridHeight);
            builder.BuildRooms(numberOfRooms);

            // Conectar las habitaciones con pasillos
            builder.ConnectRooms();
        }
    public GameObject healthPotionPrefab;

    // M�todo para crear habitaci�n con par�metros (debe ser llamado por el Builder)
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
                sueloPositions.Add(tilePos);
        {
            int x = room.x;
            int y = room.y;
            int roomWidth = room.width;
            int roomHeight = room.height;

            // Instanciar el suelo para la habitaci�n
            for (int i = 0; i < roomWidth; i++)
            {
                for (int j = 0; j < roomHeight; j++)
                {
                    GameObject sueloInst = Instantiate(sueloPrefab, new Vector3(x + i, y + j, 0), Quaternion.identity); // Colocar el suelo
                    sueloInst.GetComponent<SpriteRenderer>().sortingOrder = 1; // Asegurarnos de que el suelo est� por encima
                }
            }
            Vector2 centerPos = new Vector2(x + roomWidth / 2, y + roomHeight / 2);
            if (healthPotionPrefab != null)
            {
                Instantiate(healthPotionPrefab, centerPos, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No se asign� el prefab de Health Potion.");
            }
        }

        Vector2 centerPos = new Vector2(x + roomWidth / 2f, y + roomHeight / 2f);
        if (healthPotionPrefab != null)
            Instantiate(healthPotionPrefab, centerPos, Quaternion.identity);

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
        int secondEnemiesInFirstRoom = 2;  // Ajusta este número si deseas más o menos

        for (int roomIndex = 0; roomIndex < rooms.Count; roomIndex++)
        {
            Room room = rooms[roomIndex];
            List<Vector2> validPositions = new List<Vector2>();
            int margin = 1;

            for (int i = room.x + margin; i < room.x + room.width - margin; i++)
            {
                if (!IsPassage(x + i - 1, y - 1))
                {
                    Vector2 pos = new Vector2(i + 0.5f, j + 0.5f);
                    if (!sueloPositions.Contains(pos)) continue;
                    Collider2D hit = Physics2D.OverlapCircle(pos, 0.4f, LayerMask.GetMask("Muro", "Enemy"));
                    if (hit == null)
                        validPositions.Add(pos);
                }
            }

            // Mezclamos posiciones para aleatoriedad
            for (int i = 0; i < validPositions.Count; i++)
            {
                Vector2 temp = validPositions[i];
                int randIndex = Random.Range(i, validPositions.Count);
                validPositions[i] = validPositions[randIndex];
                validPositions[randIndex] = temp;
            }

            int spawned = 0;

            // En la primera habitación se spawnean también SecondEnemies
            if (roomIndex == 0)
            {
                for (int i = 0; i < secondEnemiesInFirstRoom && i < validPositions.Count; i++)
                {
                    GameObject secondEnemy = secondEnemyPool.GetEnemy();
                    if (secondEnemy != null)
                    {
                        secondEnemy.SetActive(false);
                        secondEnemy.transform.position = validPositions[i];
                        secondEnemy.SetActive(true);
                        spawned++;
                    }
                }
            }

            // Spawnea enemigos normales en las demás habitaciones (y en la primera si queda espacio)
            for (int i = spawned; i < enemiesPerRoom && i < validPositions.Count; i++)
            {
                GameObject enemy = enemyPool.GetEnemy();
                if (enemy != null)
                {
                    enemy.SetActive(false);
                    enemy.transform.position = validPositions[i];
                    enemy.SetActive(true);
                }
            }
        }
    }

    public void CreatePassage(Room roomA, Room roomB)
    {
        int x1 = roomA.x + roomA.width / 2;
        int y1 = roomA.y + roomA.height / 2;
        int x2 = roomB.x + roomB.width / 2;
        int y2 = roomB.y + roomB.height / 2;

        while (y1 != y2)
        {
            sueloPositions.Add(new Vector2(x1 + 0.5f, y1 + 0.5f));
            Instantiate(sueloPrefab, new Vector2(x1 + 0.5f, y1 + 0.5f), Quaternion.identity);
            CreateWall(x1 - 1, y1);
            CreateWall(x1 + 1, y1);
            y1 += (y2 > y1) ? 1 : -1;
        }

        while (x1 != x2)
        {
            sueloPositions.Add(new Vector2(x1 + 0.5f, y1 + 0.5f));
            Instantiate(sueloPrefab, new Vector2(x1 + 0.5f, y1 + 0.5f), Quaternion.identity);
            CreateWall(x1, y1 - 1);
            CreateWall(x1, y1 + 1);
            x1 += (x2 > x1) ? 1 : -1;
        }
    }

    public bool IsRoomValid(int x, int y, int width, int height)
    {
        foreach (Room room in rooms)
            if (x < room.x + room.width && x + width > room.x && y < room.y + room.height && y + height > room.y)
                return false;
        return true;
    }

    public bool IsPassage(int x, int y)
    {
        foreach (Room room in rooms)
            if (x >= room.x && x < room.x + room.width && y >= room.y && y < room.y + room.height)
                return true;
        return false;
    }

    void CreateWall(int x, int y)
    {
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                int wx = x + i;
                int wy = y + j;
                Vector2 wallPos = new Vector2(wx + 0.5f, wy + 0.5f);

                if (!IsPassage(wx, wy) && sueloPositions.Contains(wallPos))
                    sueloPositions.Remove(wallPos);

                GameObject muroInst = Instantiate(muroPrefab, new Vector3(wx, wy, 0), Quaternion.identity);
                muroInst.layer = LayerMask.NameToLayer("Muro");
                muroInst.AddComponent<BoxCollider2D>().isTrigger = false;
                muroInst.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
        }
    }
}

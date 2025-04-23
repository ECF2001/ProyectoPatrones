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

    // Generador del nivel
    public class LevelGenerator : MonoBehaviour
    {
        public GameObject sueloPrefab;   // Prefab para el suelo
        public GameObject muroPrefab;    // Prefab para los muros
        public int gridWidth = 50;       // Ancho del mapa
        public int gridHeight = 50;      // Alto del mapa
        public int numberOfRooms = 3;    // Número de habitaciones a generar
        private List<Room> rooms = new List<Room>();  // Lista para almacenar las habitaciones

        void Start()
        {
            GenerateLevel();  // Llamamos a la función para generar el nivel al iniciar
        }

        void GenerateLevel()
        {
            rooms.Clear(); // Limpiar la lista de habitaciones

            // Crear las habitaciones con el patrón Builder
            LevelBuilder builder = new LevelBuilder(this, gridWidth, gridHeight);
            builder.BuildRooms(numberOfRooms);

            // Conectar las habitaciones con pasillos
            builder.ConnectRooms();
        }
    public GameObject healthPotionPrefab;

    // Método para crear habitación con parámetros (debe ser llamado por el Builder)
    public void CreateRoom(Room room)
        {
            int x = room.x;
            int y = room.y;
            int roomWidth = room.width;
            int roomHeight = room.height;

            // Instanciar el suelo para la habitación
            for (int i = 0; i < roomWidth; i++)
            {
                for (int j = 0; j < roomHeight; j++)
                {
                    GameObject sueloInst = Instantiate(sueloPrefab, new Vector3(x + i, y + j, 0), Quaternion.identity); // Colocar el suelo
                    sueloInst.GetComponent<SpriteRenderer>().sortingOrder = 1; // Asegurarnos de que el suelo esté por encima
                }
            }
            Vector2 centerPos = new Vector2(x + roomWidth / 2, y + roomHeight / 2);
            if (healthPotionPrefab != null)
            {
                Instantiate(healthPotionPrefab, centerPos, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No se asignó el prefab de Health Potion.");
            }

        // Instanciar las paredes alrededor de la habitación
        for (int i = 0; i < roomWidth + 2; i++)
            {
                if (!IsPassage(x + i - 1, y - 1))
                {
                    GameObject muroInst = Instantiate(muroPrefab, new Vector3(x + i - 1, y - 1, 0), Quaternion.identity);
                    muroInst.layer = LayerMask.NameToLayer("Muro");
                    muroInst.AddComponent<BoxCollider2D>(); // Añadir el collider al muro
                    muroInst.GetComponent<BoxCollider2D>().isTrigger = false; // Asegurarse de que no sea un trigger (físico)
                    muroInst.GetComponent<SpriteRenderer>().sortingOrder = -1; // Colocar el muro por debajo
                }

                if (!IsPassage(x + i - 1, y + roomHeight))
                {
                    GameObject muroInst = Instantiate(muroPrefab, new Vector3(x + i - 1, y + roomHeight, 0), Quaternion.identity);
                    muroInst.layer = LayerMask.NameToLayer("Muro");
                    muroInst.AddComponent<BoxCollider2D>(); // Añadir el collider al muro
                    muroInst.GetComponent<BoxCollider2D>().isTrigger = false; // Asegurarse de que no sea un trigger (físico)
                    muroInst.GetComponent<SpriteRenderer>().sortingOrder = -1; // Colocar el muro por debajo
                }
            }

            for (int j = 0; j < roomHeight + 2; j++)
            {
                if (!IsPassage(x - 1, y + j - 1))
                {
                    GameObject muroInst = Instantiate(muroPrefab, new Vector3(x - 1, y + j - 1, 0), Quaternion.identity);
                    muroInst.layer = LayerMask.NameToLayer("Muro");
                    muroInst.AddComponent<BoxCollider2D>(); // Añadir el collider al muro
                    muroInst.GetComponent<BoxCollider2D>().isTrigger = false; // Asegurarse de que no sea un trigger (físico)
                    muroInst.GetComponent<SpriteRenderer>().sortingOrder = -1; // Colocar el muro por debajo
                }

                if (!IsPassage(x + roomWidth, y + j - 1))
                {
                    GameObject muroInst = Instantiate(muroPrefab, new Vector3(x + roomWidth, y + j - 1, 0), Quaternion.identity);
                    muroInst.layer = LayerMask.NameToLayer("Muro");
                    muroInst.AddComponent<BoxCollider2D>(); // Añadir el collider al muro
                    muroInst.GetComponent<BoxCollider2D>().isTrigger = false; // Asegurarse de que no sea un trigger (físico)
                    muroInst.GetComponent<SpriteRenderer>().sortingOrder = -1; // Colocar el muro por debajo
                }
            }
        }

        // Verificar si una habitación es válida (no se solapa)
        public bool IsRoomValid(int x, int y, int width, int height)
        {
            foreach (Room room in rooms)
            {
                if (x < room.x + room.width && x + width > room.x && y < room.y + room.height && y + height > room.y)
                {
                    return false;
                }
            }
            return true;
        }

        // Verificar si una posición es un pasillo
        public bool IsPassage(int x, int y)
        {
            foreach (Room room in rooms)
            {
                if (x >= room.x && x < room.x + room.width && y >= room.y && y < room.y + room.height)
                {
                    return true;
                }
            }
            return false;
        }

        // Crear pasillos entre habitaciones
        public void CreatePassage(Room roomA, Room roomB)
        {
            int x1 = roomA.x + roomA.width / 2;
            int y1 = roomA.y + roomA.height / 2;
            int x2 = roomB.x + roomB.width / 2;
            int y2 = roomB.y + roomB.height / 2;

            while (y1 != y2)
            {
                Instantiate(sueloPrefab, new Vector3(x1, y1, 0), Quaternion.identity);
                Instantiate(sueloPrefab, new Vector3(x1 + 1, y1, 0), Quaternion.identity);
                Instantiate(sueloPrefab, new Vector3(x1 + 2, y1, 0), Quaternion.identity);

                CreateWall(x1 - 1, y1);
                CreateWall(x1 + 3, y1);

                if (y1 < y2) y1++;
                else if (y1 > y2) y1--;
            }

            while (x1 != x2)
            {
                Instantiate(sueloPrefab, new Vector3(x1, y1, 0), Quaternion.identity);
                Instantiate(sueloPrefab, new Vector3(x1, y1 + 1, 0), Quaternion.identity);
                Instantiate(sueloPrefab, new Vector3(x1, y1 + 2, 0), Quaternion.identity);

                CreateWall(x1, y1 - 1);
                CreateWall(x1, y1 + 3);

                if (x1 < x2) x1++;
                else if (x1 > x2) x1--;
            }
        }

        // Método para crear muros en las posiciones correctas
        void CreateWall(int x, int y)
        {
            if (!IsPassage(x, y))  // Verificar si no es un pasillo
            {
                // Ampliar el rango de creación de muros
                // Crear muros en un área más grande alrededor del pasillo
                for (int i = -2; i <= 2; i++)  // Aumentando el rango de -1 a 2 para mayor cobertura
                {
                    for (int j = -2; j <= 2; j++)  // Aumentando el rango de -1 a 2 para mayor cobertura
                    {
                        // Verificar si la posición es fuera del área del pasillo
                        if (!IsPassage(x + i, y + j))
                        {
                            GameObject muroInst = Instantiate(muroPrefab, new Vector3(x + i, y + j, 0), Quaternion.identity);
                            muroInst.AddComponent<BoxCollider2D>();  // Añadir el collider al muro
                            muroInst.GetComponent<BoxCollider2D>().isTrigger = false;  // Asegurarse de que no sea un trigger (físico)
                            muroInst.GetComponent<SpriteRenderer>().sortingOrder = -1;  // Colocar el muro por debajo del suelo
                        }
                    }
                }
            }
        }
    }

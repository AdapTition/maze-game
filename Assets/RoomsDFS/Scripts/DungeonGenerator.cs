using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Implementation of dungeon generation using depth-first search algorithm.
 * Uses three methods: MazeGenerator(), CheckNeighbours(), and GenerateDungeon().
 */
public class DungeonGenerator : MonoBehaviour
{
    // Class representing a cell, storing necessary door information
    public class Cell
    {
        public bool visited = false; // Whether the cell has been visited
        public bool[] status = new bool[4]; // Array representing doors: 0 - Up; 1 - Down; 2 - Right; 3 - Left
    }

    public Vector2 size; // Size of the dungeon
    public int startPos = 0; // Starting position
    public GameObject[] rooms; // Array of room prefabs
    public Vector2 offset; // Offset for room positions

    List<Cell> board; // List of Cell objects
    public GameObject portalRoom; // Portal room prefab
    public GameObject key; // Key object
    private bool keySpawned = false; // Flag to ensure only one key is spawned

    void Start()
    {
        MazeGenerator(); // Start generating the maze
    }

    /*
     * GenerateDungeon() method creates dungeon rooms based on generated maze.
     */
    void GenerateDungeon()
    {
        int b = board.Count - 1; // Variable b and keySpawned used for guaranteed key placement in one of the rooms
        bool keySpawned = false;

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                b--;

                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];

                // Create the last room with portal
                if (i == j && i == size.x - 1)
                {
                    var newRoom = Instantiate(portalRoom, new Vector2(i * offset.x, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i + j * size.x)].status);
                    newRoom.name += " " + i + "-" + j;
                }
                // Create regular rooms
                else if (currentCell.visited)
                {
                    // Guaranteed key placement per level
                    if (Random.Range(0, b) == 0 && !keySpawned)
                    {
                        Instantiate(key, new Vector2(i * offset.x, -j * offset.y), Quaternion.identity, transform);
                        keySpawned = true;
                    }

                    int r = UnityEngine.Random.Range(1, rooms.Length); // Randomly choose a room from the rooms array
                    var newRoom = Instantiate(rooms[r], new Vector2(i * offset.x, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i + j * size.x)].status);
                }
            }
        }
    }

    /*
     * MazeGenerator() method initializes a list of Cell objects which will be used to generate the maze on the scene.
     */
    void MazeGenerator()
    {
        // Initialize an empty board for the maze
        board = new List<Cell>();

        // Create maze cells with size.x by size.y dimensions
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos; // Set the initial cell
        Stack<int> path = new Stack<int>(); // Stack to store the path

        // Main maze generation loop
        while (true)
        {
            // Mark current cell as visited
            board[currentCell].visited = true;

            // Check if we've reached the last cell
            if (currentCell == board.Count - 1)
            {
                break;
            }

            // Get list of neighbors for the current cell
            List<int> neighbours = CheckNeighbours(currentCell);

            // If no available neighbors
            if (neighbours.Count == 0)
            {
                // If path stack is empty, break the loop
                if (path.Count == 0)
                {
                    break;
                }
                // Otherwise, backtrack to the previous cell
                else
                {
                    currentCell = path.Pop();
                }
            }
            // If there are available neighbors
            else
            {
                // Push current cell onto the path stack
                path.Push(currentCell);

                // Randomly choose a new cell from available neighbors
                int newCell = neighbours[Random.Range(0, neighbours.Count)];

                // Proper connections between cells based on their relative positions
                if (newCell > currentCell)
                {
                    // Down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true; // Down
                        currentCell = newCell;
                        board[currentCell].status[3] = true; // Up
                    }
                    else
                    {
                        board[currentCell].status[1] = true; // Right
                        currentCell = newCell;
                        board[currentCell].status[0] = true; // Left
                    }
                }
                else
                {
                    // Up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true; // Up
                        currentCell = newCell;
                        board[currentCell].status[2] = true; // Down
                    }
                    else
                    {
                        board[currentCell].status[0] = true; // Left
                        currentCell = newCell;
                        board[currentCell].status[1] = true; // Right
                    }
                }
            }
        }

        // Generate dungeon rooms after maze generation is complete
        GenerateDungeon();
    }

    /*
     * CheckNeighbours() method returns a list of neighboring cells for a given cell index.
     */
    List<int> CheckNeighbours(int cell)
    {
        List<int> neighbours = new List<int>();

        // Check neighbor above
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell - size.x));
        }

        // Check neighbor below
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell + size.x));
        }

        // Check neighbor to the right
        if ((cell + 1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell + 1));
        }

        // Check neighbor to the left
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbours.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbours;
    }
}

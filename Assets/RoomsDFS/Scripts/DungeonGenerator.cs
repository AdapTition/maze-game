using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random=UnityEngine.Random;

public class NewBehaviourScript : MonoBehaviour
{
    public class Cell{
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    public Vector2 size;
    public int startPos = 0;
    public GameObject[] rooms;
    public GameObject vidro;
    public bool lastRoom = false;

    public Vector2 offset;

    List<Cell> board;
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }


    void GenerateDungeon(){
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i+j*size.x)];
                if (currentCell.visited){
                    int r = UnityEngine.Random.Range(0, rooms.Length);
                    var newRoom = Instantiate(rooms[r], new Vector2(i*offset.x , -j*offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i+j*size.x)].status);

                    newRoom.name += " "+ i + "-" + j;
                }
            }
        }
    }
    void MazeGenerator(){
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++){
            for (int j = 0; j < size.y; j++){
                board.Add(new Cell());
            }
        }
        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        
        while (true){
            board[currentCell].visited = true;

            if (currentCell == board.Count-1){
                break;
            }

            List<int> neighbours = CheckNeighbours(currentCell);
            if (neighbours.Count == 0){
                if (path.Count == 0){
                    break;
                }
                else{
                    currentCell = path.Pop();
                }
            }
            else{
                path.Push(currentCell);

                int newCell = neighbours[Random.Range(0, neighbours.Count)];
                if(newCell > currentCell){
                    //down or right
                    if (newCell-1== currentCell){
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else{
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;                        
                    }
                }
                else{
                    //up or left
                    if (newCell + 1== currentCell){
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else{
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;                        
                    }

                }
            }
        }
        GenerateDungeon();
    }
    List<int> CheckNeighbours(int cell){
        List<int> neighbours = new List<int>();

        //check up neighbourg
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited){
            neighbours.Add(Mathf.FloorToInt(cell - size.x));
        }
        //check down neighbourg
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited){
            neighbours.Add(Mathf.FloorToInt(cell + size.x));
        }
        //check right neighbourg
        if ((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited){
            neighbours.Add(Mathf.FloorToInt(cell + 1));
        }
        //check left neighbourg
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited){
            neighbours.Add(Mathf.FloorToInt(cell - 1));
        }
        return neighbours;
    }
}

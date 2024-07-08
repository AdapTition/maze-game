using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random=UnityEngine.Random;


/*
*Реалізація генерації карти алгоритмом пошуку в глибину
* Використовується три методи: MazeGenerator(), CheckNeighbours() та GenerateDungeon()
*/
public class DungeonGenerator : MonoBehaviour
{
    //Клас, об'єкт якого зберігає в собі список необхідих дверей.
    public class Cell{
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    public Vector2 size; // розмір лабіринту
    public int startPos = 0; // стартова позиція
    public GameObject[] rooms; //масив префабів кімнат
    public Vector2 offset; // змінщення координат створення кімнат

    List<Cell> board; //список об'єктів Cell
    public GameObject portalRoom;
    public GameObject key;
    private bool keySpawned = false;

    void Start()
    {
        MazeGenerator();
    }


    void GenerateDungeon(){
        /*змінна b та kewSpawned використовується для гарантованого створення ключа в одній з кімнат. 

        При створенні звичайної кімнати ми випадково вибираємо значення між 0 та b (спочатку є рівним кількості кімнат).

        якщо значення == 0 -- ключ створюється, а зміннf keySpawned отриумує значення true, не даючи цим створити ще один ключ.
        
        якщо ж випадкове значення не дорівнює 0, тоді змінна b зменшує своє значення й чекає наступної ітерації циклу.
        зменшуючи b, ми призводимо рядок до того, що колись Random.Range(0 , b) отримає значення 0 та значення b, яке довінює 0.

        це і призводить до того, що ключ точно колись створиться.*/

        int b = board.Count - 1; 
        bool keySpawned = false;

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                b--; //зменшення b
                Cell currentCell = board[Mathf.FloorToInt(i+j*size.x)];

                //створення останньої кімнати з порталом.
                if ( i == j && i == size.x - 1){
                    var newRoom = Instantiate(portalRoom, new Vector2(i*offset.x , -j*offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i+j*size.x)].status);
                    newRoom.name += " "+ i + "-" + j;
                }
                //створення звичайних кімнат.
                else if (currentCell.visited){
                    //Гарантоване створення одного ключа на рівні.
                    if ( Random.Range(0 , b) == 0 && !keySpawned ){
                        Instantiate(key, new Vector2(i*offset.x , -j*offset.y), Quaternion.identity, transform);
                        keySpawned = true;
                    }

                    int r = UnityEngine.Random.Range(1, rooms.Length); // випадковий вибір кімнати з масиву кімнат.
                    var newRoom = Instantiate(rooms[r], new Vector2(i*offset.x , -j*offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i+j*size.x)].status);

                    newRoom.name += " "+ i + "-" + j;
                    
                }
                
            }
        }
    }

/* метод MazeGenerator() створює список об'єктів Cell, який пізніше буде використаний для створення лабіринту на сцені.
*/
 void MazeGenerator(){
    // Ініціалізуємо пусту дошку для лабіринту
    board = new List<Cell>();

    // Створюємо клітинки лабіринту розміром size.x на size.y
    for (int i = 0; i < size.x; i++){
        for (int j = 0; j < size.y; j++){
            board.Add(new Cell());
        }
    }

    // Встановлюємо початкову клітинку
    int currentCell = startPos;

    // Стек для збереження шляху
    Stack<int> path = new Stack<int>();

    // Головний цикл генерації лабіринту
    while (true){
        // Відмічаємо поточну клітинку як відвідану
        board[currentCell].visited = true;

        // Перевіряємо, чи ми досягли кінцевої клітинки
        if (currentCell == board.Count-1){
            break;
        }

        // Отримуємо список сусідів поточної клітинки
        List<int> neighbours = CheckNeighbours(currentCell);

        // Якщо немає доступних сусідів
        if (neighbours.Count == 0){
            // Якщо стек шляху порожній, виходимо з циклу
            if (path.Count == 0){
                break;
            }
            // Інакше повертаємося до попередньої клітинки
            else{
                currentCell = path.Pop();
            }
        }

        // Якщо є доступні сусіди
        else{
            // Зберігаємо поточну клітинку в стек
            path.Push(currentCell);

            // Вибираємо нову клітинку випадковим чином з доступних сусідів
            int newCell = neighbours[Random.Range(0, neighbours.Count)];

            // Встановлюємо зв'язки між клітинками в залежності від розташування нової клітинки
            if(newCell > currentCell){
                // Вниз або вправо
                if (newCell-1 == currentCell){
                    board[currentCell].status[2] = true; // Вниз
                    currentCell = newCell;
                    board[currentCell].status[3] = true; // Вгору
                }
                else{
                    board[currentCell].status[1] = true; // Вправо
                    currentCell = newCell;
                    board[currentCell].status[0] = true; // Вліво
                }
            }
            else{
                // Вгору або вліво
                if (newCell + 1 == currentCell){
                    board[currentCell].status[3] = true; // Вгору
                    currentCell = newCell;
                    board[currentCell].status[2] = true; // Вниз
                }
                else{
                    board[currentCell].status[0] = true; // Вліво
                    currentCell = newCell;
                    board[currentCell].status[1] = true; // Вправо
                }
            }
        }
    }

    // Генеруємо підземелля після завершення генерації лабіринту
    GenerateDungeon();
}

    List<int> CheckNeighbours(int cell){
        List<int> neighbours = new List<int>();

        // Перевіряємо сусіда зверху
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited){
            neighbours.Add(Mathf.FloorToInt(cell - size.x));
        }

        // Перевіряємо сусіда знизу
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited){
            neighbours.Add(Mathf.FloorToInt(cell + size.x));
        }

        // Перевіряємо сусіда справа
        if ((cell + 1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited){
            neighbours.Add(Mathf.FloorToInt(cell + 1));
        }

        // Перевіряємо сусіда зліва
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited){
            neighbours.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbours;
    }
}
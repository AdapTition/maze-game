using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Клас RoomBehaviour створений для того, щоб правильно ставити стіни та двері.
Отримує інформацію про входи кімнати від методу GenerateDungeon() класу DungeonGenerator і змінює їх вже після генерації кімнат рівня.
*/
public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up; 1 - Down; 2 - Right; 3 - Left;
    public GameObject[] doors;// 0 - Up; 1 - Down; 2 - Right; 3 - Left;

    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++ ){
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}

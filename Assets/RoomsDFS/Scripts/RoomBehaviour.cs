using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
The RoomBehaviour class is designed to correctly place walls and doors.
It receives information about room entrances from the GenerateDungeon() method of the DungeonGenerator class 
and updates them after the rooms of the level have been generated.
*/
public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up; 1 - Down; 2 - Right; 3 - Left;
    public GameObject[] doors; // 0 - Up; 1 - Down; 2 - Right; 3 - Left;

    // Method to update the room's walls and doors based on the status array
    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++){
            // Activate doors and deactivate walls based on the status array
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}

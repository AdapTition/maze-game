using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armour : MonoBehaviour
{
    [Header ("Armour")]
    public int armourCount;
    public Image armourDisplay;
    public Image[] armourPNG = new Image[5];
    // Start is called before the first frame update
    void Start()
    {
        armourDisplay = armourPNG[armourCount];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public void ChangeArmour (int changeValue){
        armourCount += changeValue;
        armourDisplay = armourPNG[armourCount];
    }
}

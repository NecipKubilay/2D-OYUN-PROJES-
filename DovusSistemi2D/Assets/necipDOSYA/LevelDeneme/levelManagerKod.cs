using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class Level
{
    public int levelID;
    public bool isLocked;
}



public class levelManagerKod : MonoBehaviour
{

    [SerializeField] private List<Level> levels = new List<Level>();


    public static levelManagerKod Instance;

    private void Awake()
    {
        Instance = this; 
    }

    public void UnlockLevel(int levelID)
    {
        Level tempLevel = levels.Find(level => level.levelID == levelID);
        if (tempLevel != null)
        {
            tempLevel.isLocked = false;
        }
        
    }

    public bool isLevelLocked(int levelID)
    {
        Level tempLevel = levels.Find(level => level.levelID == levelID);
        if (tempLevel != null)
        {

            return tempLevel.isLocked;
        }
        return false;
    }

}

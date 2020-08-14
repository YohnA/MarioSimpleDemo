using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelScenes")]
public class LevelScenes : ScriptableObject
{
    public List<LevelSceneWithName> level = new List<LevelSceneWithName>();

    public bool IsIn(int id)
    {
        return level.Find(a => { return a.id == id; }) == null ? false : true;
    }

    public string GetName(int id)
    {
        return level.Find(a => { return a.id == id; }).name;
    }
}
[System.Serializable]
public class LevelSceneWithName
{
    public int id;
    public string name;
}
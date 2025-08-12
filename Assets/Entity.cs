using UnityEngine;

public class Entity : MonoBehaviour
{
    private int timeToAct = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void AddTime(int t)
    {
        timeToAct += t;
    }

    public int GetTime()
    {
        return timeToAct;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

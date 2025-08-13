using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameManagement gameManager; //probably grab this in void Start(); but I'm lazy rn
    public float timeToAct = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }
    public void Act() // This function MUST eventually call GetFastestActing();
    {
        AddTime(5);
        gameManager.GetFastestActing(); 
    }
    public void AddTime(float t)
    {
        timeToAct += t;
    }

    public float GetTime()
    {
        return timeToAct;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameManagement gameManager; //probably grab this in void Start(); but I'm lazy rn
    public float timeToAct = 6;
    public float x;
    public float y;
    public int movement = 3;
    public CreateMap map;
    public int health = 5; // change later
    public Battlemap tileOccupied;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {

    }
    public virtual void Act() // This function MUST eventually call GetFastestActing();
    {
        AddTime(5);
        Debug.Log("no");
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
    public virtual void TakeDamage(int d)
    {
        health -= d;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeX(float a)
    {
        x = a;
    }
    public void ChangeY(float b)
    {
        y = b;
    }
    public float GetX()
    {
        return x;
    }
    public float GetY()
    {
        return y;
    }
}

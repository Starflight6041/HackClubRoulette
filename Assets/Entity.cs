using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameManagement gameManager; //probably grab this in void Start(); but I'm lazy rn
    public float timeToAct = 6;
    public int x;
    public int y;
    public int movement = 3;
    public CreateMap map;
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

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeX(int a)
    {
        x = a;
    }
    public void ChangeY(int b)
    {
        y = b;
    }
    public int GetX()
    {
        return x;
    }
    public int GetY()
    {
        return y;
    }
}

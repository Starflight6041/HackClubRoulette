using UnityEngine;

public class Battlemap : MonoBehaviour
{
    private int x;
    private int y;
    private bool isOccupied; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool OccupiedStatus()
    {
        return isOccupied; 
    }
    public void ChangeOccupied()
    {
        isOccupied = !isOccupied;
    }

    public void ChangeCoords(int a, int b)
    {
        x = a;
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
    public Vector2 GetPosition()
    {
        if (x % 2 == 0)
        {
            return new Vector2((float)(x * 1.5 - 7), (float)(y * 1.5 - 4));
        }
        else
        {
            return new Vector2((float)(x * 1.5 - 7), (float)(y * 1.5 - 3.25));
        }

    }
    public Vector2 ReturnPosition(int i, int x)
    {
        if (i % 2 == 0)
        {
            return new Vector2((float)(i * 1.5 - 7), (float)(x * 1.5 - 4));
        }
        else
        {
            return new Vector2((float)(i * 1.5 - 7), (float)(x * 1.5 - 3.25));
        }


    }
    
}

using UnityEngine;

public class Battlemap : MonoBehaviour
{
    private float x;
    private float y;
    private bool isOccupied = false; 
    // add prospective damage variable to display on hexagon

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool GetOccupied()
    {
        return isOccupied; 
    }
    public void SetOccupied(bool a)
    {
        isOccupied = a;
    }

    public void ChangeCoords(float a, float b)
    {
        x = a;
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
    public Vector2 GetPosition()
    {
        if (x % 2 == 0)
        {
            return new Vector2((float)(x * 1.5 - 7), (float)(y * 1.5 - 4));
        }
        else
        {
            return new Vector2((float)(x * 1.5 - 7), (float)((y-.5) * 1.5 - 3.25));
        }

    }
    public static Vector2 ReturnPosition(float i, float x)
    {
        if (i % 2 == 0)
        {
            return new Vector2((float)(i * 1.5 - 7), (float)(x * 1.5 - 4));
        }
        else
        {
            return new Vector2((float)(i * 1.5 - 7), (float)((x - .5) * 1.5 - 3.25));
        }


    }
    
}

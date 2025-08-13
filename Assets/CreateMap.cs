using JetBrains.Annotations;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject hexagon; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hexagon = GameObject.Find("Hexagon");
        for (int i = 0; i < 10; i++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (i % 2 == 0)
                {
                    GameObject h = Instantiate(hexagon, new Vector2((float)(i * 1.5 - 7), (float)(x * 1.5 - 4)), Quaternion.identity);
                    h.GetComponent<Battlemap>().ChangeCoords(i, x); // establish coordinates to use for movement and where attacks hit
                }
                else
                {
                    GameObject h = Instantiate(hexagon, new Vector2((float)(i * 1.5 - 7), (float)(x * 1.5 - 3.25)), Quaternion.identity);
                    h.GetComponent<Battlemap>().ChangeCoords(i, x);
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

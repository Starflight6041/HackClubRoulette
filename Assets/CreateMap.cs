using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject hexagon;
    public List<Battlemap> places = new List<Battlemap>();
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
                    places.Add(h.GetComponent<Battlemap>());
                }
                else
                {
                    GameObject h = Instantiate(hexagon, new Vector2((float)(i * 1.5 - 7), (float)(x * 1.5 - 3.25)), Quaternion.identity);
                    h.GetComponent<Battlemap>().ChangeCoords(i, x);
                    places.Add(h.GetComponent<Battlemap>());
                }
            }

        }
        ChooseRandom(); 
    }
    public List<Battlemap> GetMap()
    {
        return places;
    }
    public Battlemap ChooseRandom()
    {
        Battlemap randomB = places[UnityEngine.Random.Range(0, places.Count - 1)];
        Occupy(randomB);
        return randomB;
    }
    public void Occupy(Battlemap b)
    {

        places.Remove(b);
        b.ChangeOccupied();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

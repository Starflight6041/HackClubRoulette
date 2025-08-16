using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject hexagon;
    public List<Battlemap> places = new List<Battlemap>();
    public List<Battlemap> removed = new List<Battlemap>();
    
    public GameManagement gameManager;
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
                    h.GetComponent<Battlemap>().ChangeCoords(i, (float) (x + .5));
                    places.Add(h.GetComponent<Battlemap>());
                }
            }

        }
        //ChooseRandom(); 
        Enemy.changeNumber(FindObjectsByType<Ally>(FindObjectsSortMode.None));
        gameManager.ScatterEntities();
        gameManager.GetFastestActing();
    }
    public List<Battlemap> GetMap()
    {
        return places;
    }
    public Battlemap ChooseRandom()
    {
        
        Battlemap randomB = places[UnityEngine.Random.Range(0, places.Count - 1)];
        //Occupy(randomB);
        return randomB;
    }
    public void Occupy(float a, float b)
    {

        //places.Remove(b);
        // removed.Add(b);
        // b.ChangeOccupied();
        for (int i = places.Count - 1; i >= 0; i--)
        {
            if (places[i].GetX() == a && places[i].GetY() == b)
            {

                
                places[i].SetOccupied(true);
                removed.Add(places[i]);
                places.Remove(places[i]);
                

            }
        }
        
    }
    public bool IsPositionUnoccupied(float x, float y)
    {
        for (int i = 0; i < places.Count; i++)
        {
            if (places[i].GetX() == x && places[i].GetY() == y)
            {
                return true;
            }
        }
        return false;
    }
    public Battlemap GetTile(float x, float y)
    {
        for (int i = 0; i < places.Count; i++)
        {
            if (places[i].GetX() == x && places[i].GetY() == y)
            {
                return places[i];
            }
        }
        for (int i = 0; i < removed.Count; i++)
        {
            if (removed[i].GetX() == x && removed[i].GetY() == y)
            {
                return removed[i];
            }
        }
        return null;
    }


    public void Unoccupy(float a, float b)
    {
        //places.Add(b);
        //removed.Remove(b);
        for (int i = removed.Count - 1; i >= 0; i--)
        {
            if (removed[i].GetX() == a && removed[i].GetY() == b)
            {
                removed[i].SetOccupied(false);
                places.Add(removed[i]);
                removed.Remove(removed[i]);

            }

        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

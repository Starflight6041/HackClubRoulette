using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagement : MonoBehaviour
{
    private float time;
    public List<Entity> entities = new List<Entity>();
    private bool EntitySelectionPhase = true;
    public List<Entity> fastestEntities = new List<Entity>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputAction mousepos = InputSystem.actions.FindAction("point");
        InputAction click = InputSystem.actions.FindAction("click");
        // now use Raycast
    }

    public void ClickReceived()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetFastestActing() // Get the fastest acting to use for the RunTurn() method
    {
        
        fastestEntities.Add(entities[0]);
        for (int i = 1; i < entities.Count; i++)
        {
            if (entities[i].GetTime() < fastestEntities[0].GetTime()) // can be at 0 because they'll always have the same value if I didn't mess up badly
            {
                for (int x = fastestEntities.Count - 1; i >= 0; x--)
                {
                    fastestEntities.RemoveAt(x);
                }
                fastestEntities.Add(entities[i]);

            }
            if (entities[i].GetTime() == fastestEntities[0].GetTime())
            {
                fastestEntities.Add(entities[i]);
            }
            // now prompt the player for their choice of entity to act
            PlayerSelects(fastestEntities);
        }
    }
    public void PlayerSelects(List<Entity> e)
    {
        for (int i = 0; i < e.Count; i++)
        {
            // reference e[i].gameObject and place arrows over each of them
        }
        EntitySelectionPhase = true;
    }
    public void RunTurn(Entity e, float t)
    {




    }
}

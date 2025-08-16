using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
//using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    private float time;
    public List<Entity> entities = new List<Entity>();
    // maybe make a list of enemies too to only randomize the positions of the enemies and not the player?
    private bool EntitySelectionPhase = true;
    public List<Entity> fastestEntities = new List<Entity>();
    public Entity nextActing = null;
    public Battlemap battlemap;
    public InputAction mousepos;
    public InputAction click;
    public List<Vector2> possiblePositions = new List<Vector2>();
    public CreateMap map;
    public int score = 0;
    public TMP_Text scoreText;
    public Entity e;
    public int deadAllies;
    public bool gameOver = false;
    public Canvas gameOverCanvas;
    public TMP_Text finalScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverCanvas.gameObject.SetActive(false);
        //for (int i = 0; i < 10; i++)
        // {
        //  for (int x = 0; x < 5; x++)
        //  {
        //      possiblePositions.Add(battlemap.ReturnPosition(i, x));
        //      Debug.Log(battlemap.ReturnPosition(i, x));
        //   }
        // }
        //ScatterEntities();


        // test
        mousepos = InputSystem.actions.FindAction("point");
        click = InputSystem.actions.FindAction("click");
        // now use Raycast
    }
    public void RemoveEntity(Entity e)
    {
        entities.Remove(e);
    }
    public void ScatterEntities()
    {

        for (int i = 0; i < entities.Count; i++)
        {
            Battlemap b = map.ChooseRandom();
            entities[i].gameObject.transform.position = b.GetPosition();
            entities[i].tileOccupied = b;
            map.Occupy(b.GetX(), b.GetY());
            entities[i].ChangeX(b.GetX());
            entities[i].ChangeY(b.GetY());

            // Vector2 p = possiblePositions[UnityEngine.Random.Range(0, possiblePositions.Count - 1)];
            //Debug.Log(p);
            //entities[i].gameObject.transform.position = p;

            //possiblePositions.Remove(p);



        }
    }

    public void ClickReceived()
    {
        //Debug.Log("yes");
        if (click.WasPressedThisFrame())
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousepos.ReadValue<Vector2>()), Vector2.zero);

            if (hit.collider != null)
            {
                GameObject gO = hit.collider.gameObject;
                //  Debug.Log(gO);
                //  Debug.Log(EntitySelectionPhase);
                if (EntitySelectionPhase)
                {
                    //   Debug.Log("yes");
                    if (gO.GetComponent<Ally>() != null && fastestEntities.Contains(gO.GetComponent<Entity>()))
                    {
                        //Debug.Log("wahoooo");
                        // Debug.Log("mhm");
                        nextActing = gO.GetComponent<Entity>();
                        //for (int x = fastestEntities.Count - 1; x >= 0; x--)
                        // {
                        //
                        //     fastestEntities.RemoveAt(x);
                        // }
                        EntitySelectionPhase = false; // this does nothing for now
                        RunTurn(nextActing, nextActing.GetTime());
                        //Debug.Log(nextActing);


                    }


                }


            }





        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetFastestActing() // Get the fastest acting to use for the RunTurn() method
    {
        //Debug.Log("fastest acting running");

        fastestEntities.Add(entities[0]);
        if (fastestEntities.Count > 1)
        {
            // Debug.Log(fastestEntities[0]);
            // Debug.Log(fastestEntities[1]);
            //  Debug.Log(fastestEntities[0].GetTime());
            // Debug.Log(fastestEntities[1].GetTime());
        }

        for (int i = 1; i < entities.Count; i++)
        {
            if (entities[i].GetTime() < fastestEntities[0].GetTime()) // can be at 0 because they'll always have the same value if I didn't mess up badly
            {
                for (int x = fastestEntities.Count - 1; x >= 0; x--)
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
            //Debug.Log(fastestEntities[0]);
            // Debug.Log(fastestEntities[0].GetTime());


        }
        bool isPlayerActing = false;
        for (int i = 0; i < fastestEntities.Count; i++)
        {
            if (fastestEntities[i].GetComponent<Ally>())
            {
                isPlayerActing = true;
            }
        }
        if (isPlayerActing)
        {
            PlayerSelects(fastestEntities);
            Debug.Log("player acting");
        }
        else
        {
            Debug.Log("enemy acting");

            RunTurn(fastestEntities[0], fastestEntities[0].GetTime());


        }
    }
    public void PlayerSelects(List<Entity> e)
    {
        for (int i = 0; i < e.Count; i++)
        {
            // reference e[i].gameObject and place arrows over each of them
        }
        // Debug.Log("PlayerSelects running");
        EntitySelectionPhase = true;
        //Debug.Log(EntitySelectionPhase);
    }
    public void RunTurn(Entity e, float t)
    {
        if (!gameOver)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].AddTime(t * -1);


            }
            for (int x = fastestEntities.Count - 1; x >= 0; x--)
            {

                fastestEntities.RemoveAt(x);
            }
            e.Act();
            for (int i = 0; i < entities.Count; i++)
            {

                //Debug.Log(entities[i].GetTime());
                //Debug.Log(entities[i]);
            }
        }





    }
    public void AddScore(int s)
    {
        score += s;
        scoreText.text = "Score: " + score;

    }
    public void SpawnEnemy()
    {

        Battlemap b = map.ChooseRandom();
        Entity a = Instantiate(e);
        a.gameObject.transform.position = b.GetPosition();
        a.tileOccupied = b;
        map.Occupy(b.GetX(), b.GetY());
        a.ChangeX(b.GetX());
        a.ChangeY(b.GetY());
        entities.Add(a);
        if (score > 3)
        {
            Battlemap c = map.ChooseRandom();
            Entity d = Instantiate(e);
            d.gameObject.transform.position = c.GetPosition();
            d.tileOccupied = c;
            map.Occupy(c.GetX(), c.GetY());
            d.ChangeX(c.GetX());
            d.ChangeY(c.GetY());
            entities.Add(d);
        }
    }
    public void IncreaseDeadAllies()
    {
        deadAllies += 1;
        if (deadAllies == 2)
        {
            gameOver = true;
            gameOverCanvas.gameObject.SetActive(true);
            finalScore.text = "Score: " + score;
            
        }
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

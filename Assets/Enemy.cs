using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : Entity
{
    //private int health;
    public int numPlayers = 1; //change later
    public static Ally[] players;
    public Ally closestPlayer;
    public List<Vector2> willAttack = new List<Vector2>();
    public List<int> damage = new List<int>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("this is the number of allies" + FindAnyObjectByType(typeof(Ally)));
        
    }
    public static void changeNumber(Ally[] a)
    {
        players = a; 
    }


    // Update is called once per frame
    void Update()
    {

    }
    //public void TakeDamage(int damage)
    //{
    //    health -= damage;
    //}

    public void Attack(int x, int y)
    {

    }
    public override void Act()
    {
        ExecuteAttacks();
        FindClosestPlayer();
        MoveTowardsClosest();
        LineAttack();
        AddTime(5);
        gameManager.GetFastestActing();
    }
    public void ExecuteAttacks()
    {
        for (int i = 0; i < willAttack.Count; i++)
        {
            for (int a = 0; a < players.Count(); a++)
            {
                if (players[a].GetX() == willAttack[i].x && players[a].GetY() == willAttack[i].y)
                {
                    players[a].TakeDamage(damage[a]);
                }
            }
        }
        for (int i = willAttack.Count - 1; i >= 0; i--)
        {
            UnhighlightAttack(willAttack[i].x, willAttack[i].y);
            willAttack.RemoveAt(i);
            damage.RemoveAt(i);
        }
    }
    public void LineAttack()
    {
        FindClosestPlayer();
        QueueAttack(closestPlayer.GetX(), closestPlayer.GetY(), 1);
        QueueAttack(closestPlayer.GetX() + 1, (float) (closestPlayer.GetY() + .5), 1);
        QueueAttack(closestPlayer.GetX() + - 1, (float) (closestPlayer.GetY() - .5), 1);

    }
    public void QueueAttack(float x, float y, int d)
    {
        willAttack.Add(new Vector2(x, y));
        damage.Add(d);
        HighlightAttack(x, y);
    }
    public void SpotAttack()
    {
        FindClosestPlayer();
        QueueAttack(closestPlayer.GetX(), closestPlayer.GetY(), 2);

    }
    public void AreaControl()
    {
        
    }
    public void HighlightAttack(float x, float y)
    {
        if (map.GetTile(x,y))
        {
            map.GetTile(x, y).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.orange);
        }
        
        
    }
    public void UnhighlightAttack(float x, float y)
    {
        if (map.GetTile(x, y))
        {
            map.GetTile(x, y).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
        
    }
    public void FindClosestPlayer()
    {
        Debug.Log(players[0]);
        Ally closest = players[0];
        for (int i = 1; i < players.Count(); i++)
        {
            //Math.Abs(x - map.GetMap()[i].GetX()) + Math.Abs(y - map.GetMap()[i].GetY()) <= movement
            if (Math.Abs(players[i].GetX() - x) + Math.Abs(players[i].GetY() - y) < Math.Abs(closest.GetX() - x) + Math.Abs(closest.GetY() - y))
            {
                closest = players[i];
            }
        }
        closestPlayer = closest;
        //Debug.Log(closestPlayer);
    }
    public void MoveTowardsClosest()
    {
        Debug.Log("moving");

        int xMove = 0;
        if (closestPlayer.GetX() > x)
        {
            xMove = 1;
        }
        if (closestPlayer.GetX() < x)
        {
            xMove = -1;
        }
        int yMove = 0;
        if (closestPlayer.GetY() > y)
        {
            yMove = 1;
        }
        if (closestPlayer.GetY() < y)
        {
            yMove = -1;
        }
        if (map.IsPositionUnoccupied(x + xMove, y + yMove))
        {

            map.Unoccupy(x, y);
            x = x + xMove;
            y = y + yMove;
            gameObject.transform.position = tileOccupied.ReturnPosition(x, y);
            map.Occupy(x, y);


        }
        else if (map.IsPositionUnoccupied(x, y + yMove))
        {
            map.Unoccupy(x, y);

            y = y + yMove;
            gameObject.transform.position = tileOccupied.ReturnPosition(x, y);
            map.Occupy(x, y);
        }
        else if (map.IsPositionUnoccupied(x + xMove, y))
        {
            map.Unoccupy(x, y);
            x = x + xMove;

            gameObject.transform.position = tileOccupied.ReturnPosition(x, y);
            map.Occupy(x, y);
        }
        //when enemies have more movement, use a for loop to loop through possible locations to move to



    }

}

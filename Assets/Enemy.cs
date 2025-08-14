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
    private int health;
    public int numPlayers = 1; //change later
    public static Ally[] players;
    public Ally closestPlayer;
    
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
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void Attack(int x, int y)
    {

    }
    public override void Act()
    {
        FindClosestPlayer();
        MoveTowardsClosest();
        AddTime(5);
        gameManager.GetFastestActing();
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

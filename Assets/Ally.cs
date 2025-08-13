using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
public class Ally : Entity
{
    private bool isMoving = false;
    public InputAction mousepos;
    public InputAction click;
    private Vector2 prospectivePosition;
    private int maxMove;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mousepos = InputSystem.actions.FindAction("point");
        click = InputSystem.actions.FindAction("click");
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void Act()
    {
        isMoving = true;
        HighlightMoves();
        AddTime(5);
        //Debug.Log("yes");
    }
    public void PlayerMove()
    {

        if (click.WasPressedThisFrame())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousepos.ReadValue<Vector2>()), Vector2.zero);
            //Debug.Log("Check -1");
            if (hit.collider != null)
            {
                GameObject gO = hit.collider.gameObject;
                //Debug.Log("Check 0");
                //Debug.Log(isMoving);
                if (isMoving && gO.GetComponent<Battlemap>() != null)
                {
                    //Debug.Log("Check 1");

                    if (!gO.GetComponent<Battlemap>().GetOccupied() && Math.Abs(x - gO.GetComponent<Battlemap>().GetX()) + Math.Abs(y - gO.GetComponent<Battlemap>().GetY()) <= movement)
                    {
                        //Debug.Log("Check 2");
                        prospectivePosition = gO.GetComponent<Battlemap>().GetPosition();
                        gameObject.transform.position = prospectivePosition;

                        map.Unoccupy(tileOccupied.GetX(), tileOccupied.GetY());
                        ChangeX(gO.GetComponent<Battlemap>().GetX());
                        ChangeY(gO.GetComponent<Battlemap>().GetY());
                        Unhighlight();
                        map.Occupy(gO.GetComponent<Battlemap>().GetX(), gO.GetComponent<Battlemap>().GetY()); 
                        tileOccupied = gO.GetComponent<Battlemap>(); 
                        isMoving = false;
                        
                        //gameObject.layer = 2;
                        gameManager.GetFastestActing();
                    }
                }



            }

        }

    }
    public void Confirm()
    {

    }
    public void HighlightMoves()
    {
        for (int i = 0; i < map.GetMap().Count; i++)
        {
            if (Math.Abs(x - map.GetMap()[i].GetX()) + Math.Abs(y - map.GetMap()[i].GetY()) <= movement)
            {
                map.GetMap()[i].GetComponent<Renderer>().material.SetColor("_Color", Color.softRed);
            }
            else
            {
                map.GetMap()[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
        }
        
    }
    public void Unhighlight()
    {
        for (int i = 0; i < map.GetMap().Count; i++)
        {
            
            map.GetMap()[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            
        }
    }
}

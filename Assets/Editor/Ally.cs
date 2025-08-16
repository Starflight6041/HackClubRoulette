using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class Ally : Entity
{
    private bool isMoving = false;
    public InputAction mousepos;
    
    public InputAction click;
    private Vector2 prospectivePosition;
    private int maxMove;
    public TMP_Text healthText;
    public Canvas attacks;
    public bool isAttacking = false;
    public bool isInstantAttacking = false;
    public bool isAoeAttacking = false;
    public GameObject attackCircle;
    public bool isAlive = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attacks.gameObject.SetActive(false);
        mousepos = InputSystem.actions.FindAction("point");
        click = InputSystem.actions.FindAction("click");
        attackCircle.SetActive(false);
        
    }
    public override void TakeDamage(int d)
    {
        base.TakeDamage(d);

        healthText.text = "Ally Health: " + health;
        if (health <= 0 && isAlive)
        {
            map.Unoccupy(x, y);
            isAlive = false;
            gameManager.RemoveEntity(this);
            gameObject.SetActive(false);
            gameManager.IncreaseDeadAllies();
            
        }
        
    }

    
    // Update is called once per frame
    void Update()
    {

    }
    public void InstantAttack()
    {
        //Debug.Log(isAttacking);
        if (isAttacking && !isMoving)
        {
            isInstantAttacking = !isInstantAttacking;
            isAoeAttacking = false;
            
            isAttacking = !isAttacking;
        //    Debug.Log("yup" + " " + isInstantAttacking);
        }
        if (isInstantAttacking)
        {
            
            HighlightInstant();
        }
        else
        {
            UnhighlightInstant();
        }
        
    }
    public void PassTurn()
    {
        if (isAttacking && !isMoving)
        {

            isInstantAttacking = false;
            UnhighlightInstant();
            isAttacking = !isAttacking;
            gameManager.GetFastestActing();
            //    Debug.Log("yup" + " " + isInstantAttacking);
        }
    }

    public void DeclareAttack()
    {

        if (click.WasPressedThisFrame())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousepos.ReadValue<Vector2>()), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject gO = hit.collider.gameObject;
                if (isInstantAttacking)
                {
                    if (gO.GetComponent<Enemy>())
                    {
                        if (Math.Abs(gO.GetComponent<Enemy>().GetX() - x) + Math.Abs(gO.GetComponent<Enemy>().GetY() - y) < 2)
                        {
                            gO.GetComponent<Enemy>().TakeDamage(2);
                            isInstantAttacking = false;
                            attacks.gameObject.SetActive(false);
                            UnhighlightInstant();
                            gameManager.GetFastestActing();
                        }
                    }
                }

            }
        }
    }


    public override void Act()
    {
        isMoving = true;
        isAttacking = true;
        isInstantAttacking = false;
        isAoeAttacking = false;
        HighlightMoves();
        AttackUI();
        AddTime(5);
        //Debug.Log("yes");
    }
    public void AttackUI()
    {
        attacks.gameObject.SetActive(true);
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
                    Debug.Log(gO.GetComponent<Battlemap>().GetOccupied());
                    Debug.Log("yes");
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
                        //gameManager.GetFastestActing();
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
            if (Math.Abs(x - map.GetMap()[i].GetX()) + Math.Abs(y - map.GetMap()[i].GetY()) <= movement && map.GetMap()[i].GetComponent<Renderer>().material.color == Color.white)
            {
                map.GetMap()[i].GetComponent<Renderer>().material.SetColor("_Color", Color.softRed);
            }
            
        }

    }
    public void HighlightInstant()
    {
        //for (int i = 0; i < map.GetMap().Count; i++)
        // {
        //    if (Math.Abs(x - map.GetMap()[i].GetX()) + Math.Abs(y - map.GetMap()[i].GetY()) <= 2 && map.GetMap()[i].GetComponent<Renderer>().material.color == Color.white)
        //    {
        //        Debug.Log("greening");
        //        map.GetMap()[i].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        //        Debug.Log(map.GetMap()[i].GetComponent<Renderer>().material.color);
        //    }

        //}
        attackCircle.SetActive(true);
    }
    public void UnhighlightInstant()
    {
        //for (int i = 0; i < map.GetMap().Count; i++)
        //{


        //    if (map.GetMap()[i].gameObject.GetComponent<Renderer>().material.color == Color.green)
        //    {
        //        map.GetMap()[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        //    }

        //}
        attackCircle.SetActive(false);
        
    }
    public void Unhighlight()
    {
        for (int i = 0; i < map.GetMap().Count; i++)
        {

            
            if (map.GetMap()[i].gameObject.GetComponent<Renderer>().material.color == Color.softRed)
            {
                map.GetMap()[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
            
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class Ally : Entity
{
    private bool isMoving = false;
    public InputAction mousepos;
    public InputAction click;
    private Vector2 prospectivePosition;
    private int maxMove;
    private int x;
    private int y;
    

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

                    if (!gO.GetComponent<Battlemap>().GetOccupied())
                    {
                        //Debug.Log("Check 2");
                        prospectivePosition = gO.GetComponent<Battlemap>().GetPosition();
                        gameObject.transform.position = prospectivePosition;
                        isMoving = false;
                        gameManager.GetFastestActing();
                    }
                }



            }

        }
        
    }
    public void Confirm()
    {
        
    }
}

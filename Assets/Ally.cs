using UnityEngine;
using UnityEngine.InputSystem;
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

    }

    // Update is called once per frame
    void Update()
    {

    }
    new public void Act()
    {
        isMoving = true;

    }
    public void PlayerMove()
    {
        if (click.WasPressedThisFrame())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousepos.ReadValue<Vector2>()), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject gO = hit.collider.gameObject;
                if (isMoving && gO.GetComponent<Battlemap>() != null)
                {
                    //if (gO.GetComponent<Battlemap>().)
                    prospectivePosition = gO.GetComponent<Battlemap>().GetPosition();
                }
                
                    
                
            }

        }
        isMoving = false;
    }
    public void Confirm()
    {
        
    }
}

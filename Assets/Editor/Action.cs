using UnityEngine;

public class Action : MonoBehaviour
{
    private int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Execute(Enemy target)
    {
        target.TakeDamage(damage);
    }
}

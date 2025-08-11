using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}

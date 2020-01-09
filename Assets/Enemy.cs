using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int contactDamage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    abstract public int GetContactDamage();
}

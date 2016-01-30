using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
    public float health = 100;
    public float strength = 20;
    public float blood = 10;

    Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
    }

	// Use this for initialization
	public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            ani.SetBool("isDead", true); 
        }
    }

    public void CompleteDie()
    {
       
    }
    public void AttackPlayer()
    {
        transform.parent.SendMessage("AttackPlayer", SendMessageOptions.DontRequireReceiver);
    }

    public void AttackEnemy()
    {
        transform.parent.SendMessage("AttackEnemy", SendMessageOptions.DontRequireReceiver);
    }
}

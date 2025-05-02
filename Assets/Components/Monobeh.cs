using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monobeh : MonoBehaviour
{
    public Animator anim;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Attack2");
        }
    }
}

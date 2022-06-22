using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vlna : MonoBehaviour
{
    public Animator anim;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("Vlna");
        }
    }
}

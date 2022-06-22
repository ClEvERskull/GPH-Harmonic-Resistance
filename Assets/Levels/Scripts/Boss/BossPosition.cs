using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPosition : MonoBehaviour
{
    public Transform[] points;
    public Transform boss;
    public Transform player;
    public float startingHealth;
    private float currentHealth;
    private Transform lastpos;
    public int lastPoint;
    public int currentPoint;
    public Transform shield;
    public Animator bossanim;
    public Animator shieldanim;
    public Transform molystart;
    public Transform moly;
    public Animator molyanim;
    public float molyspeed;
    public BossHealthbar bosshealth;
    private void Start()
    {
        currentHealth = startingHealth;
        currentPoint = Random.Range(0, points.Length);
        boss.position = points[currentPoint].position;
        lastPoint = currentPoint;
        shield.position = boss.position;
        bosshealth.SetHealth(currentHealth, startingHealth);
        //InvokeRepeating("Utok", 1f, 3f);
    }
    void PickPoints()
    {
        while (lastPoint == currentPoint)
        {
            currentPoint = Random.Range(0, points.Length);
            if (lastPoint != currentPoint)
            {
                boss.position = points[currentPoint].position;
                shield.position = boss.position;
            }
        }
    }

    void Check()
    {
        lastPoint = currentPoint;
    }
    void Update()
    {
        if (player.position.x < boss.position.x)
        {
            boss.localScale = new Vector3(-1, 1, 1);
            shield.localScale = new Vector3(-1, 1, 1);
        }
        else if (player.position.x > boss.position.x)
        {
            boss.localScale = new Vector3(1, 1, 1);
            shield.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentHealth > 1)
        {
            if (collision.CompareTag("Gitara"))
            {
                PickPoints();
                Check();
                currentHealth -= 1;
                bosshealth.SetHealth(currentHealth, startingHealth);
                Debug.Log(currentHealth);
            }
        }
        else
        {
            Death();
        }
    }
    void Death()
    {
        bosshealth.slider.gameObject.SetActive(false);
        bossanim.SetTrigger("Dead");
        shieldanim.SetTrigger("Dead");
    }
    //void Utok()
    //{
    //    if (Vector2.Distance(molystart.position, player.position) <= 8f)
    //    {
    //        bossanim.SetTrigger("Hod");
    //        molyanim.SetTrigger("Hod");
    //        moly.position = molystart.position;
    //        while (moly.position != player.position)
    //        {
    //            moly.position = Vector2.MoveTowards(moly.position, player.position, molyspeed * Time.deltaTime);
    //        }
    //        molyanim.SetTrigger("Bum");
    //    }
    //}
}
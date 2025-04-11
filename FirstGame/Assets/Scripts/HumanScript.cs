using System;
using UnityEngine;

public class HumanScript : MonoBehaviour
{
    private Animator animator;
    public int speed;
    public bool move = true;
    private MainControl zombi; 

    void Start()
    {
        animator = GetComponent<Animator>();
        zombi = GameObject.Find("MainControl").GetComponent<MainControl>();
    }
    void Update()
    {
        if (move)    transform.Translate(Vector3.right * speed * MainControl.y * Time.deltaTime);
        if (transform.position.x > 0)   Destroy(gameObject);  
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("IsTouched", true);
            //zombi.SkorArtir();
        }
    }
}

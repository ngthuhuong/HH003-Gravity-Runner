using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBehaviour : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isFly", true);
            Debug.Log("collision w crow");
            // Fly follow player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
        }
    }
}

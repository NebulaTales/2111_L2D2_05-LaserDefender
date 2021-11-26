using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float speed = 10f;
    //[SerializeField] float speed = 3000f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.1f;

    public bool isFiring;
    Coroutine firingCoroutine;

    void Start()
    {

    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, 
                                                transform.position, 
                                                Quaternion.identity);
            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                //rb.velocity = transform.up * speed * Time.deltaTime;
                rb.velocity = transform.up * speed;
            }

            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(firingRate);
        }
    }
}

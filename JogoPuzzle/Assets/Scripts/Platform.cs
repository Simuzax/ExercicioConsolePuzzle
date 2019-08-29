using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    float speed;

    bool isGoingRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        if (isGoingRight) transform.Translate(Vector3.right * speed * Time.deltaTime);
        else transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Parede"))
            isGoingRight = !isGoingRight;
    }
}

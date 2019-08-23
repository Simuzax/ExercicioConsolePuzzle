using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float timeValue;

    bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        this.AttachTimer(timeValue, switchDirection, isLooped: true);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        if (isRight) transform.Translate(Vector3.right * speed * Time.deltaTime);
        else transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void switchDirection()
    {
        isRight = !isRight;
    }
}

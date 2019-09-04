using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    [SerializeField]
    float distance;

    [SerializeField]
    float timeToMove;

    [SerializeField]
    float waitTime;

    enum Directions
    {
        Up,
        Down,
        Right,
        Left,
        Forward,
        Backward
    }

    [SerializeField]
    Directions direção;

    bool invertDirection;

    [SerializeField]
    bool useGlobalOrientation;

    // Start is called before the first frame update
    void Start()
    {
        if (useGlobalOrientation)
        {
            switch (direção)
            {
                case Directions.Up:
                    move(invertDirection, Vector3.up);
                    break;
                case Directions.Down:
                    move(invertDirection, Vector3.down);
                    break;
                case Directions.Left:
                    move(invertDirection, Vector3.left);
                    break;
                case Directions.Right:
                    move(invertDirection, Vector3.right);
                    break;
                case Directions.Forward:
                    move(invertDirection, Vector3.forward);
                    break;
                case Directions.Backward:
                    move(invertDirection, Vector3.back);
                    break;
            }
        }
        else
        {
            switch (direção)
            {
                case Directions.Up:
                    move(invertDirection, transform.up);
                    break;
                case Directions.Down:
                    move(invertDirection, -transform.up);
                    break;
                case Directions.Left:
                    move(invertDirection, -transform.right);
                    break;
                case Directions.Right:
                    move(invertDirection, transform.right);
                    break;
                case Directions.Forward:
                    move(invertDirection, transform.forward);
                    break;
                case Directions.Backward:
                    move(invertDirection, -transform.forward);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void move(bool inverse, Vector3 direction)
    {
        if (inverse)
        {
            transform.DOMove(transform.position + (direction * distance), timeToMove).SetEase(Ease.Linear).SetDelay(waitTime).OnComplete(() =>
            {
                move(inverse, direction);
            });
        }
        else
        {
            transform.DOMove(transform.position + (direction * distance * -1), timeToMove).SetEase(Ease.Linear).SetDelay(waitTime).OnComplete(() =>
            {
                move(inverse, direction);
            });
        }

        inverse = !inverse;
    }
}

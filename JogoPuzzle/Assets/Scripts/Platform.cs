using DG.Tweening;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float distance;

    [SerializeField]
    private float timeToMove;

    [SerializeField]
    private float waitTime;

    private enum Directions
    {
        Up,
        Down,
        Right,
        Left,
        Forward,
        Backward
    }

    [SerializeField]
    private Directions direção;

    private bool invertDirection;

    [SerializeField]
    private bool useGlobalOrientation;

    public Vector3 lastPos, deltaPos;

    // Start is called before the first frame update
    private void Start()
    {
        lastPos = transform.position;

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
    private void Update()
    {
        deltaPos = transform.position - lastPos;

        lastPos = transform.position;
    }

    private void move(bool inverse, Vector3 direction)
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
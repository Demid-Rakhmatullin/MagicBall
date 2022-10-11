using System.Diagnostics.CodeAnalysis;
using UnityEngine;


public class RotatorScript : MonoBehaviour
{
    private static readonly float PI_DIV_4 = /*Round*/(Mathf.Sqrt(2) / 2); //45 gradus angle

    //public static float Round(float f)
    //=> f;
    // => Mathf.Round(f * 10_000) / 10_000;

    public float Speed;
    public Transform pivot;

    public Vector2 CurrentDirection { get; private set; }

   // private Vector2 center;
    //private float radius;
    private float angle45;
    private Vector2 top;
    private Vector2 right;
    private Vector2 bisector;
    private float relativeSpeed;

    //just for debug log
    //
#pragma warning disable 0414
    private byte octant;
    public GameObject marker;
#pragma warning restore 0414

    void Awake()
    {
        if (Speed < 0)
            throw new System.Exception("Rotation speed must be positive number");
        //transform.LookAt(pivot);

        angle45 = PI_DIV_4;
        top = new Vector2(0, 1);
        right = new Vector2(1, 0);
        bisector = new Vector2(angle45, angle45);

        CurrentDirection = -top;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            rotate(false);
        else if (Input.GetKey(KeyCode.RightArrow))
            rotate(true);
    }

    private void rotate(bool clockwise)
    {
        //TryGetComponent(out FollowerScript follower);
        //center = getPosition(pivot);
        //var objectPos = getPosition(transform);
        //radius = follower.distance; ///*Round*/(Vector2.Distance(objectPos, center));
       

        //var relativePosition = objectPos - center;
        var newDirection = clockwise
            ? rotateClockwise()
            : rotateCounterclockwise();
        //transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.y);
        CurrentDirection = newDirection;
        //transform.LookAt(pivot);
        //if (TryGetComponent(out FollowerScript follower))
            //follower.UpdateDelta();

        //Debug.Log($"Rot dist: {radius}");
        //Debug.Log($"New pos: {newDirection}");

        //Debug.Log((clockwise ? "Clockwise; " : "Counterclockwise; ") + octant + ";" +
        //    " Current: " + objectPos + "; Args: " + relativePosition + "; Go to: " + newPosition);
        //Instantiate(marker, transform.position, Quaternion.identity);
    }

    public static Vector2 getPosition(Transform transform)
        => new Vector2(/*Round*/(transform.position.x), /*Round*/(transform.position.z));


    //based on Midpoint circle algorithm (MCA) - https://en.wikipedia.org/wiki/Midpoint_circle_algorithm
    //

    private Vector2 rotateClockwise()
    {
        Vector2 newPosition = default;
        Vector2 MCA_Result = default;

        relativeSpeed = Speed * Time.deltaTime;

        if (CurrentDirection.x >= 0 && CurrentDirection.y > 0)
        {
            if (CurrentDirection.x < angle45)
            {
                // 1 octant
                MCA_Result = increaseX(CurrentDirection.x, CurrentDirection.y,
                    angle45, bisector);
                octant = 1;
            }
            else
            {
                // 2 octant
                MCA_Result = decreaseY(CurrentDirection.x, CurrentDirection.y,
                    0, right);
                octant = 2;
            }
            newPosition = MCA_Result;
        }
        else if (CurrentDirection.x > 0 && CurrentDirection.y <= 0)
        {
            if (CurrentDirection.y > -angle45)
            {
                // 3 octant
                MCA_Result = decreaseY(CurrentDirection.x, CurrentDirection.y,
                    -angle45, bisector);
                octant = 3;
            }
            else
            {
                // 4 octant
                MCA_Result = decreaseX(CurrentDirection.x, CurrentDirection.y,
                    0, top);
                octant = 4;
            }
            newPosition = new Vector2(MCA_Result.x, - MCA_Result.y);
        }
        else if (CurrentDirection.x <= 0 && CurrentDirection.y < 0)
        {
            if (CurrentDirection.x > -angle45)
            {
                // 5 octant
                MCA_Result = decreaseX(CurrentDirection.x, CurrentDirection.y,
                    -angle45, bisector);
                octant = 5;
            }
            else
            {
                // 6 octant
                MCA_Result = increaseY(CurrentDirection.x, CurrentDirection.y,
                    0, right);
                octant = 6;
            }
            newPosition = -MCA_Result;
        }
        else if (CurrentDirection.x < 0 && CurrentDirection.y >= 0)
        {
            if (CurrentDirection.y < angle45)
            {
                // 7 octant
                MCA_Result = increaseY(CurrentDirection.x, CurrentDirection.y,
                    angle45, bisector);
                octant = 7;
            }
            else
            {
                // 8 octant
                MCA_Result = increaseX(CurrentDirection.x, CurrentDirection.y,
                    0, top);
                octant = 8;
            }
            newPosition = new Vector2(-MCA_Result.x, MCA_Result.y);
        }
        else
            throw new System.Exception("Can't detect octant");

        return newPosition;
    }

    private Vector2 rotateCounterclockwise()
    {
        Vector2 newPosition = default;
        Vector2 MCA_Result = default;

        relativeSpeed = Speed * Time.deltaTime;

        if (CurrentDirection.x <= 0 && CurrentDirection.y > 0)
        {
            if (CurrentDirection.x > -angle45)
            {
                // 8 octant
                MCA_Result = decreaseX(CurrentDirection.x, CurrentDirection.y,
                    -angle45, bisector);
                octant = 8;
            }
            else
            {
                // 7 octant
                MCA_Result = decreaseY(CurrentDirection.x, CurrentDirection.y,
                    0, right);
                octant = 7;
            }
            newPosition = new Vector2(-MCA_Result.x, MCA_Result.y);
        }
        else if (CurrentDirection.x < 0 && CurrentDirection.y <= 0)
        {
            if (CurrentDirection.y > -angle45)
            {
                // 6 octant
                MCA_Result = decreaseY(CurrentDirection.x, CurrentDirection.y,
                    -angle45, bisector);
                octant = 6;
            }
            else
            {
                // 5 octant
                MCA_Result = increaseX(CurrentDirection.x, CurrentDirection.y,
                    0, top);
                octant = 5;
            }
            newPosition = -MCA_Result;
        }
        else if (CurrentDirection.x >= 0 && CurrentDirection.y < 0)
        {
            if (CurrentDirection.x < angle45)
            {
                // 4 octant
                MCA_Result = increaseX(CurrentDirection.x, CurrentDirection.y,
                   angle45, bisector);
                octant = 4;
            }
            else
            {
                // 3 octant
                MCA_Result = increaseY(CurrentDirection.x, CurrentDirection.y,
                   0, right);
                octant = 3;
            }
            newPosition = new Vector2(MCA_Result.x, -MCA_Result.y);
        }
        else if (CurrentDirection.x > 0 && CurrentDirection.y >= 0)
        {
            if (CurrentDirection.y < angle45)
            {
                // 2 octant
                MCA_Result = increaseY(CurrentDirection.x, CurrentDirection.y,
                   angle45, bisector);
                octant = 2;
            }
            else
            {
                // 1 octant
                MCA_Result = decreaseX(CurrentDirection.x, CurrentDirection.y,
                   0, top);
                octant = 1;
            }
            newPosition = MCA_Result;
        }
        else
            throw new System.Exception("Can't detect octant");

        return newPosition;
    }

    private Vector2 increaseX(float x, float y, float limit, Vector2 limitPoint)
    {
        if (x + relativeSpeed >= limit)
            return limitPoint;
        else
            return new Vector2(
                    Mathf.Sqrt(x * x + 2 * x * relativeSpeed + relativeSpeed * relativeSpeed),
                    Mathf.Sqrt(y * y - 2 * x * relativeSpeed - relativeSpeed * relativeSpeed)
                );
    }

    private Vector2 decreaseX(float x, float y, float limit, Vector2 limitPoint)
    {
        if (x - relativeSpeed <= limit)
            return limitPoint;
        else
            return new Vector2(
                Mathf.Sqrt(x * x - 2 * x * relativeSpeed + relativeSpeed * relativeSpeed),
                Mathf.Sqrt(y * y + 2 * x * relativeSpeed - relativeSpeed * relativeSpeed)
            );
    }

    private Vector2 increaseY(float x, float y, float limit, Vector2 limitPoint)
    {
        if (y + relativeSpeed >= limit)
            return limitPoint;
        else
            return new Vector2(
                Mathf.Sqrt(x * x - 2 * y * relativeSpeed - relativeSpeed * relativeSpeed),
                Mathf.Sqrt(y * y + 2 * y * relativeSpeed + relativeSpeed * relativeSpeed)
            );
    }

    private Vector2 decreaseY(float x, float y, float limit, Vector2 limitPoint)
    {
        if (y - relativeSpeed <= limit)
            return limitPoint;
        else
            return new Vector2(
                Mathf.Sqrt(x * x + 2 * y * relativeSpeed - relativeSpeed * relativeSpeed),
                Mathf.Sqrt(y * y - 2 * y * relativeSpeed + relativeSpeed * relativeSpeed)
            );
    }
}


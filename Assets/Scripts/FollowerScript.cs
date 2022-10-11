using UnityEngine;

public class FollowerScript : MonoBehaviour
{

    public Transform objToFollow;
    public float ShiftY = 6.05f;
    public float ShiftZ = 15.8f;
    public float Accuracy = 0.2f;

    private Vector3 delta;
    private Vector2 lastDirection;
    private RotatorScript rotator;
    private float flatDistance;

    void Start()
    {
        transform.position = new Vector3(objToFollow.position.x, objToFollow.position.y + ShiftY, objToFollow.position.z - ShiftZ);
        //currentPos = SkipHeight(transform.position);
        flatDistance = Vector2.Distance(SkipHeight(transform.position), SkipHeight(objToFollow.position));
        rotator = GetComponent<RotatorScript>();

        transform.LookAt(objToFollow);
        lastDirection = rotator.CurrentDirection;
        //UpdateDelta();
    }

    public void LateUpdate()
    {
        if (Vector3.Distance(transform.position, objToFollow.position) > Accuracy)
        {
            var flatPos = SkipHeight(objToFollow.position) + rotator.CurrentDirection * flatDistance;
            transform.position = new Vector3(flatPos.x, objToFollow.position.y + ShiftY, flatPos.y);           
        }

        if (lastDirection != rotator.CurrentDirection)
        {
            transform.LookAt(objToFollow);
            lastDirection = rotator.CurrentDirection;
        }


        //transform.position = objToFollow.position - delta;
        //var pos = (objToFollow.position - transform.position).normalized * distance;
        //transform.position = objToFollow.position + new Vector3(pos.x, ShiftY, -pos.z);

        //var direction = -transform.forward * distance;
        //transform.position = objToFollow.position + new Vector3(direction.x, ShiftY, direction.z);
        //Debug.Log($"Follow pos: {transform.position }");
    }

    //public void UpdateDelta()
    //    => delta = objToFollow.position - transform.position;

    private Vector2 SkipHeight(Vector3 vector)
        => new Vector2(vector.x, vector.z);

}

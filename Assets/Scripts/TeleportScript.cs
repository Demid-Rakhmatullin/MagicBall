using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public GameObject Destination;
    public bool StopMovement;
    public bool ResetPlayerCamera;

    private bool isTeleportedInside;

    void OnTriggerEnter(Collider other)
    {
        if (!isTeleportedInside)
        {
            //teleportaion, literally
            other.gameObject.transform.position = Destination.transform.position;

            //process player camera
            if (ResetPlayerCamera && other.gameObject.CompareTag("Player"))
            {
                var playerCamera = GameObject.FindGameObjectWithTag("MainCamera")
                    .GetComponent<Transform>();
                var player = other.gameObject.transform;
                
                //playerCamera.TryGetComponent(out FollowerScript followerScript);
                //followerScript?.LateUpdate();

                //make player camera look at same direction as destination teleport rotation
                if (ResetPlayerCamera)
                {
                    var distance = /*RotatorScript.Round*/(
                        Vector2.Distance(RotatorScript.getPosition(playerCamera), RotatorScript.getPosition(player.transform)));
                    //set player camera to default position around the player
                    playerCamera.transform.position = new Vector3(player.transform.position.x, playerCamera.transform.position.y, player.transform.position.z - distance);

                    //rotate camera using Unity build in function
                    //playerCamera.RotateAround(player.position, Vector3.up, Destination.transform.rotation.eulerAngles.y);

                    //rotate camera using rotation matrix, just for education purposes
                    var rot = -Destination.transform.rotation.eulerAngles.y * Mathf.PI / 180;
                    var cos = Mathf.Cos(rot);
                    var sin = Mathf.Sin(rot);
                    var relPos = skipHeight(playerCamera.position) - skipHeight(player.transform.position);
                    var rotated = new Vector2(relPos.x * cos - relPos.y * sin, relPos.x * sin + relPos.y * cos);
                    playerCamera.transform.position = new Vector3(rotated.x + player.transform.position.x, playerCamera.transform.position.y, rotated.y + player.transform.position.z);

                    playerCamera.transform.LookAt(player);
                    var rotator = playerCamera.GetComponent<RotatorScript>();
                    rotator.CurrentDirection = new Vector2(0, -1);
                    var follower = playerCamera.GetComponent<FollowerScript>();
                    follower.LastDirection = default;
                    //followerScript?.UpdateDelta();
                }
            }

            //prevent endless teleporation loop
            TeleportScript destinationTeleport = Destination.GetComponent<TeleportScript>();
            if (destinationTeleport != null)
            {
                destinationTeleport.isTeleportedInside = true;
            }

            //stop movent of an object after teleportation
            if (StopMovement)
            {
                Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();
                if (otherRb != null)
                {
                    otherRb.velocity = Vector3.zero;
                    otherRb.angularVelocity = Vector3.zero;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        isTeleportedInside = false;
    }

    private Vector2 skipHeight(Vector3 vector)
        => new Vector2(vector.x, vector.z);
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform transformToFollow;
    public Vector3 aimPositionOffset;
    public Vector3 cameraPositionOffset;

    void Start()
    {
        transform.position = transformToFollow.position + cameraPositionOffset;
        transform.LookAt(transformToFollow.position + aimPositionOffset);
    }

    void Update()
    {
        //This line makes instant camera movement.
        //transform.position = transformToFollow.position + cameraPositionOffset;

        //This line makes smooth camera movement.
        transform.position = Vector3.Lerp(transform.position, transformToFollow.position + cameraPositionOffset, Time.deltaTime * 5f);

        //Aim the camera at the desired position
        transform.LookAt(transformToFollow.position + aimPositionOffset);
    }

    //On validate is called when a parameter is set on the inspector.
    //This will update the camera transform so you can preview how it looks without having to play the game.
    void OnValidate()
    {
        if(transformToFollow != null)
        {
            transform.position = transformToFollow.position + cameraPositionOffset;
            transform.LookAt(transformToFollow.position + aimPositionOffset);            
        }
    }
}


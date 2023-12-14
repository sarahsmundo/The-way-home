using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // [SerializeField] private Transform player; //assigned to gojo in unity bcz serialize field
    public Transform Target;
    public float Cameraspeed;
    public float minX, maxX;
    public float minY, maxY;


    void LateUpdate()
    {
        if (Target != null)
        {
            Vector2 newCamPosition = Vector2.Lerp(transform.position, Target.position, Cameraspeed * Time.deltaTime);
            float ClampX = Mathf.Clamp(newCamPosition.x, minX, maxX);
            float ClampY = Mathf.Clamp(newCamPosition.y, minY, maxY);
            transform.position = new Vector3(ClampX, ClampY, -10f);
        }
    }

    //private void Update()
    //{
    //  transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
    //transform doesnt need get component can just be accessed like this 
    //}
}

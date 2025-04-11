using UnityEngine;

public class GroundLooper : MonoBehaviour
{
    public float speed = 10f;
    public float resetX = 0f;
    public float startX = -250f;
    public bool floor=false;

    void FixedUpdate()
    {
        if (floor)
        {
            transform.Translate(Vector3.forward * speed * MainControl.y * Time.deltaTime);

            // Zemin geri döndüðünde baþa al
            if (transform.position.x > resetX)
            {
                Vector3 newPos = transform.position;
                newPos.x = startX+MainControl.y;
                transform.position = newPos;
            }
        }
        
    }
    public void FloorMovement()=>floor = true;
}

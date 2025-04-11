using UnityEngine;
using UnityEngine.UI;


public class ObjectMovement : MonoBehaviour
{
    public int speed;
    public bool move=true;
    
    void Update()
    {
        if (move) transform.Translate(Vector3.right * speed * MainControl.y * Time.deltaTime);
        if (transform.position.x > 0)
        {
            Destroy(gameObject);
        }
    }
    public void StartGame() => move = true;
}

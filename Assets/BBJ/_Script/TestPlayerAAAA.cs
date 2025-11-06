using UnityEngine;

public class TestPlayerAAAA : MonoBehaviour
{
    
    private void FixedUpdate()
    {
        //transform.position +=  transform.up;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IBlock block))
        {
            if(block.CheckBreak(gameObject))
            {
                block.Break(gameObject);
                Debug.Log("ºÎ½¥´Ù.");
            }
            else
            {
                block.Collision(gameObject);
                Debug.Log("¸ø ºÎ½¥´Ù.");
            }
        }
    }
}

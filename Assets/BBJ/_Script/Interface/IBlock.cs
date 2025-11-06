using UnityEngine;

public interface IBlock 
{
    public bool CheckBreak(bool isDash);
    public void Break(GameObject target);
    public void Collision(GameObject target);
}

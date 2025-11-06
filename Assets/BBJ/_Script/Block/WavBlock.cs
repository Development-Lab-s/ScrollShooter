using UnityEngine;

public class WavBlock : BlockBase
{
    //필드로 사운드가 필요함

    public override void Break(GameObject target)
    {
        // 사운드 재생
        Debug.Log("노래 재생");
        Destroy(gameObject);
    }

    public override void Collision(GameObject target)
    {
        Break(target);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class WavBlock : BlockBase, IUseable
{
    public void Use(UseableInfo info)
    {
        // 사운드 재생
        Debug.Log("노래 재생");
        Destroy(gameObject);
    }
}

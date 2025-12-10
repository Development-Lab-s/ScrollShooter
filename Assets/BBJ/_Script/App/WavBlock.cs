using Unity.VisualScripting;
using UnityEngine;

public class WavBlock : BlockBase, IContactable
{
    public void TryContact(ContactInfo info) => Use();

    public void Use()
    {
        // 사운드 재생
        Debug.Log("노래 재생");
        Destroy(gameObject);
    }
}

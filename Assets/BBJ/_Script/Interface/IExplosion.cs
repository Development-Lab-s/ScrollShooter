using Mono.Cecil;
using UnityEngine;

public interface IExplosion: IContactable
{
    public void OnExplosion();
}

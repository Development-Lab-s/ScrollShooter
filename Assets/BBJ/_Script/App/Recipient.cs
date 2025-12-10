using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class Recipient : MonoBehaviour
{
    [SerializeField] private ChannelSO<Transform> channel;
    [SerializeField] private AnimationHashDataSO hash;
    [SerializeField] private Transform recipinentPos;
    public UnityEvent<int> OnAnimationTriggered;
    public void GotIt()
    {
        OnAnimationTriggered?.Invoke(hash);
    }
    private void OnInvolkCollback(Sended<Transform> sended)
    {
        sended?.Invoke(recipinentPos);
    }
    private void OnEnable()
    {
        channel.SubEevnt(OnInvolkCollback);
    }
    private void OnDisable()
    {
        channel.UnsubEvent(OnInvolkCollback);
    }
}

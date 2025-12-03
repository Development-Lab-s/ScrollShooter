using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Member.JYG._Code
{
    public class AfterEffector : MonoBehaviour
    {
        [SerializeField] private Volume changeVolume;
        [SerializeField] private int effectApplySpeed;
        [SerializeField] private float effectCloseTime;
        public void PlayPostProcessing(float duration) // Play with UnityEvent
        {
            StopAllCoroutines();
            StartCoroutine(EffectWeightUp(duration));
        }

        private IEnumerator EffectWeightDown()
        {
            changeVolume.weight = 1;
            while (changeVolume.weight > 0)
            {
                changeVolume.weight -= Time.deltaTime / effectCloseTime;
                yield return null;
            }
            changeVolume.weight = 0;
            changeVolume.enabled = false;
        }

        private IEnumerator EffectWeightUp(float duration)
        {
            changeVolume.enabled = true;
            changeVolume.weight = 0;
            while (changeVolume.weight < 1)
            {
                changeVolume.weight += Time.deltaTime * effectApplySpeed;
                yield return null;
            }
            changeVolume.weight = 1;
            yield return new WaitForSeconds(duration);
            StartCoroutine(EffectWeightDown());
        }

    }
}
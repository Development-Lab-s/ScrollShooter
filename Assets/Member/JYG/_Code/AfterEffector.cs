using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Member.JYG._Code
{
    public class AfterEffector : MonoBehaviour
    {
        [SerializeField] private Volume changeVolume;
        [SerializeField] private int effectApplySpeed;
        [SerializeField] private float effectCloseTime;
        [SerializeField] private float startDelay;
            
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
            yield return new WaitForSeconds(startDelay);
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

        public void VignettePosChangeToPlayer(Volume volume)
        {
            if (volume.profile.TryGet(out Vignette vignette))
            {
                Vector2 pos = new Vector2(Camera.main.WorldToViewportPoint(GameManager.Instance.Player.transform.position).x, 0.12f);
                vignette.center.value = pos;
            }
            if (volume.profile.TryGet(out LensDistortion lens))
            {
                Vector2 pos = new Vector2(Camera.main.WorldToViewportPoint(GameManager.Instance.Player.transform.position).x, 0.35f);
                lens.center.value = pos;
            }
        }

    }
}
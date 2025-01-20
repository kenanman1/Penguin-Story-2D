using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [Header("Text Animation Settings")]
    [SerializeField] private float textAnimationDuration = 0.2f;
    [SerializeField] private float textScaleAnimation = 0.08f;
    [SerializeField] private float textMoveAnimation = 0.4f;

    private TextMeshPro enemyDamageText;
    private List<LTDescr> currentTweens = new List<LTDescr>();

    public void DamageTextEffect(TextMeshPro text)
    {
        Invoke(nameof(OnAnimationComplete), textAnimationDuration * 2);
        ResetTween(text);
        enemyDamageText = text;

        Vector3 targetScale = new Vector3(enemyDamageText.rectTransform.localScale.x * textScaleAnimation, textScaleAnimation, textScaleAnimation);

        currentTweens.Add(LeanTween.scale(enemyDamageText.rectTransform, targetScale, textAnimationDuration)
            .setLoopPingPong(1));

        currentTweens.Add(LeanTween.move(enemyDamageText.rectTransform, enemyDamageText.rectTransform.localPosition + Vector3.up * textMoveAnimation, textAnimationDuration)
            .setLoopPingPong(1));
    }

    private void OnAnimationComplete()
    {
        enemyDamageText.text = "";
        currentTweens.ForEach(tween => LeanTween.cancel(tween.id));
        currentTweens.Clear();
    }

    private void ResetTween(TextMeshPro text)
    {
        if (text.transform.localScale.x <= 0)
            text.transform.localScale = new Vector3(-1, 1, 1);
        else
            text.transform.localScale = Vector3.one;
        text.transform.localPosition = Vector3.zero;
    }
}

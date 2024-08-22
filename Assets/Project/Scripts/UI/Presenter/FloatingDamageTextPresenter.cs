using UnityEngine;

namespace Project.Scripts.UI
{
    public class FloatingDamageTextPresenter
    {
    private const string ObjectPoolDamageText = "PoolDamageText";
    private const int Count = 1;
    private const bool IsAutoExpand = true;

    private readonly ObjectPool<FloatingDamageTextView> _poolDamageText;

    public FloatingDamageTextPresenter(FloatingDamageTextView damageTextView)
    {
        _poolDamageText =
            new ObjectPool<FloatingDamageTextView>(damageTextView, Count, new GameObject(ObjectPoolDamageText).transform)
            {
                AutoExpand = IsAutoExpand
            };
    }

    public void OnChangedDamageText(float damage, Transform target)
    {
        ChangedDamageText(damage, target);
    }

    private void ChangedDamageText(float damage, Transform target)
    {
        FloatingDamageTextView damageTextView = _poolDamageText.GetFreeElement();
        damageTextView.SetDamageText(damage, target);
        damageTextView.Show();
    }
    }
}
using UnityEngine;

public class MedicineItem : Item
{
    [Header("Èú·®")]
    [SerializeField] private float _healAmount = 5f;
    protected override void TriggerEffect(Player player)
    {
        player.Heal(_healAmount);
    }
}
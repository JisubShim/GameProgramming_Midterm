using UnityEngine;

public class GunItem : Item
{
    protected override void TriggerEffect(Player player)
    {
        player.SetCanShoot(true);
    }
}
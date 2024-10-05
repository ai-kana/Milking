using System.Threading.Tasks;
using OpenMod.API.Eventing;
using OpenMod.Unturned.Players.Input.Events;
using SDG.Unturned;
using UnityEngine;

namespace Milking;

public class ItemUsedEvent : IEventListener<UnturnedPlayerPluginKeyStateChangedEvent>
{
    public async Task HandleEventAsync(object? sender, UnturnedPlayerPluginKeyStateChangedEvent @event)
    {
        if (@event.Key != 0)
        {
            return;
        }

        if (!@event.State)
        {
            return;
        }

        if (@event.Player.Player.equipment.itemID != 337)
        {
            return;
        }

        RaycastHit[] results = new RaycastHit[2];
        if (Physics.RaycastNonAlloc(new Ray(@event.Player.Player.look.aim.position, @event.Player.Player.look.aim.forward), results, 10f, RayMasks.PLAYER_INTERACT) == 0)
        {
            return;
        }

        Player? player = null;
        foreach (RaycastHit hit in results)
        {
            if (hit.transform == null)
            {
                continue;
            }

            player = DamageTool.getPlayer(hit.transform);
            if (player == null)
            {
                continue;
            }

            if (player == @event.Player.Player)
            {
                continue;
            }
        }

        if (player == null || player == @event.Player.Player)
        {
            return;
        }

        @event.Player.Player.inventory.forceAddItem(new(462, true), true);
    }
}

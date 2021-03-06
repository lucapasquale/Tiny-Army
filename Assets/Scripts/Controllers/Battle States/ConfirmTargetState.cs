﻿public class ConfirmTargetState : BattleState
{
    protected override void OnTouch(object sender, InfoEventArgs<Point> e) {
        Tile tile = Board.GetTile(e.info);
        Unit actor = RoundController.Current;

        // End turn if select itself
        if (tile == actor.Tile) {
            RoundController.EndTurn();
            owner.ChangeState<SelectUnitState>();
            return;
        }

        bool inMoveRange = RangeManager.MoveRange.Contains(tile);
        bool inAbilityRange = RangeManager.AbilityRangeAndOrigin.ContainsKey(tile);

        // Restart turn if out of range
        if (!inMoveRange && !inAbilityRange) {
            owner.ChangeState<RestartUnitState>();
            return;
        }

        // If another origin of attack tile, move there
        if (RangeManager.AbilityRangeAndOrigin[actor.turn.TargetTile].Contains(tile)) {
            actor.turn.SelectMovement(tile);
            owner.ChangeState<PerformMovement>();
            return;
        }

        // Cancel attack and move
        if (inMoveRange) {
            actor.turn.SelectMovement(tile);
            actor.turn.SelectTarget(null);
            owner.ChangeState<PerformMovement>();
            return;
        }

        // If not a target, return
        if (!actor.GetComponentInChildren<AbilityTarget>().IsTarget(tile)) {
            return;
        }

        // If in range, attack
        if (RangeManager.AbilityRangeAndOrigin[tile].Contains(actor.Tile)) {
            owner.ChangeState<PerformAbilityState>();
            return;
        }
    }
}
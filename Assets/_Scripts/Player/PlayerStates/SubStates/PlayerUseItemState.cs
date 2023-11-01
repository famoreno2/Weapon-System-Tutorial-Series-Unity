using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseItemStates : PlayerAbilityState
{
    public bool CanUseItem { get; private set; }
    private bool isHolding;
    private bool dashInputStop;

    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAIPos;

    public PlayerUseItemStates(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, PlayerInputHandler InputHandler) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        CanUseItem = false;

        // Call the UseItemInput method with a context parameter
        player.InputHandler.UseItemInput(default(InputAction.CallbackContext));

       

    }

    public override void Exit()
    {
        base.Exit();

        if (Movement?.CurrentVelocity.y > 0)
        {
            Movement?.SetVelocityY(Movement.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {

           


            
        }
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAIPos) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = player.transform.position;
    }

   // public bool CheckIfCanDash()
    //{
       // return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
  //  }

   // public void ResetCanDash() => CanDash = true;

}

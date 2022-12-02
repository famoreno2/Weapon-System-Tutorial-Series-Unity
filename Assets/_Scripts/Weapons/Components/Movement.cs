﻿using UnityEngine;
using Bardent.CoreSystem.CoreComponents;
using Bardent.Weapons.Components.ComponentData;

namespace Bardent.Weapons.Components
{
    public class Movement : WeaponComponent
    {
        private CoreSystem.CoreComponents.Movement movement;
        private CoreSystem.CoreComponents.Movement CoreMovement => movement ? movement : Core.GetCoreComponent(ref movement);

        private MovementData data;
        
        private void HandleStartMovement()
        {
            var currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
            
            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, CoreMovement.FacingDirection);

        }

        private void HandleStopMovement()
        {
            CoreMovement.SetVelocityZero();
        }

        protected override void Awake()
        {
            base.Awake();

            data = weapon.Data.GetData<MovementData>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            EventHandler.OnStartMovement += HandleStartMovement;
            EventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            EventHandler.OnStartMovement -= HandleStartMovement;
            EventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}
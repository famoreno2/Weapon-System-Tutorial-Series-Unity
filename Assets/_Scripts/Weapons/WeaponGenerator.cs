﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bardent.Weapons.Components;
using UnityEngine;

namespace Bardent.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;

        private List<WeaponComponent> componentsAlreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        private List<Type> componentDependencies = new List<Type>();

        public void GenerateWeapon(WeaponDataSO data)
        {
            InitializeListsAndDependencies(data);

            AddNewDependencies();

            RemoveOldDependencies();
        }

        private void RemoveOldDependencies()
        {
            var componentsToRemove = componentsAlreadyOnWeapon.Except(componentsAddedToWeapon);

            foreach (var component in componentsToRemove)
            {
#if UNITY_EDITOR
                DestroyImmediate(component);
                continue;
#endif

                Destroy(component);
            }
        }

        private void AddNewDependencies()
        {
            foreach (var dependency in componentDependencies)
            {
                if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                    continue;

                var weaponComponent =
                    componentsAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                if (weaponComponent == null)
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                }

                componentsAddedToWeapon.Add(weaponComponent);
            }
        }

        private void InitializeListsAndDependencies(WeaponDataSO data)
        {
            componentsAlreadyOnWeapon.Clear();
            componentsAddedToWeapon.Clear();
            componentDependencies.Clear();

            componentsAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
            componentDependencies = data.GetAllDependencies();
        }
    }
}
using MelonLoader;
using BTD_Mod_Helper;
using System.Collections.Generic;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons.Behaviors;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Behaviors;
using Il2CppAssets.Scripts.Simulation.Display;
using Il2CppAssets.Scripts.Simulation.Input;
using Il2CppAssets.Scripts.Simulation.SMath;
using GizmoUis;


namespace patch;

public static class patch
{
    [HarmonyPatch(typeof(InputManager), nameof(InputManager.GetRangeMeshes))]
    internal static class InputManager_GetRangeMeshes
    {
        [HarmonyPostfix]
        private static void Postfix(InputManager __instance, TowerModel towerModel,
            Vector3 position, ref Il2CppSystem.Collections.Generic.List<Mesh> __result)
        {
            if (towerModel != null &&
                towerModel.baseId == "MonkeyMachine-MonkeyMachine" )
                
            {
                var mesh = RangeMesh.GetMeshStatically(__instance.Sim, position, towerModel.GetAttackModel().range,
                    towerModel.ignoreBlockers);
                mesh.isValid = true;
                mesh.position = position;
                __result.Add(mesh);
            }
        }
    }


}


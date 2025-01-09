using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using System.Collections.Generic;
using UnityEngine;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Unity;
using System.Collections;
using UnityEngine.UI;
using BTD_Mod_Helper;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using System;
using Il2CppAssets.Scripts;
using MonkeyMachine;
using Il2CppAssets.Scripts.Models.Towers;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers.Filters;



namespace GizmoUis
{
    [RegisterTypeInIl2Cpp(false)]
    public class GizmoMenu : MonoBehaviour
    {
        public static GizmoMenu? instance = null;
        public ModHelperInputField? input;
        // private ModHelperImage? image;
        public ModHelperButton? Prestige;
        public static Il2CppAssets.Scripts.Simulation.Towers.Tower? selectedTower = null;

        private static Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();





            public void Close()
        {
            if (gameObject)
            {
                gameObject.Destroy();
            }
        }
        public static void CreatePanel()
        {
            if (InGame.instance != null)
            {
                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panelinvis", 0, 0, 0, 0), VanillaSprites.BrownInsertPanel);
                panel = GameObject.Find("TowerElements").AddModHelperPanel(new Info("switchpanel", 370, 640, 200, 500), ModContent.GetTextureGUID<MonkeyMachine.MonkeyMachine>("punch"));
                instance = panel.AddComponent<GizmoMenu>();
                var button = panel.AddButton(new("Button_", 0, 180, 170, 170), VanillaSprites.GreenBtnSquare, new Action(() =>
                {

                    var Towers = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-MonkeyMachine-4"));
                    var Gizmos1 = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-gizmo1"));
                    foreach (var Tower in Towers)
                    {
                        foreach (var behavior in Game.instance.model.GetTowerFromId("EngineerMonkey-100").GetAttackModels().ToArray())
                        {
                            if (behavior.name.Contains("Spawner"))
                            {
                                var towerModel = Tower.tower.rootModel.Duplicate().Cast<TowerModel>();
                                var spawner = behavior.Duplicate();
                                towerModel.RemoveBehavior<OverrideCamoDetectionModel>();
                                var onebefore = towerModel.GetAttackModel("blackholespawn");
                                var twobefore = towerModel.GetAttackModel("rocketspawn");
                                var threebefore = towerModel.GetAttackModel("beaconspawn");
                                towerModel.RemoveBehavior(onebefore);
                                towerModel.RemoveBehavior(twobefore);
                                towerModel.RemoveBehavior(threebefore);
                                spawner.weapons[0].rate = 35;
                                spawner.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                                spawner.weapons[0].projectile.ApplyDisplay<punch>();
                                spawner.name = "rocketspawn";
                                spawner.weapons[0].projectile.AddBehavior(new CreateTowerModel("Gizmorocketplace", ModContent.GetTowerModel<rocketgizmo1>().Duplicate(), 0f, true, true, false, true, false));
                                towerModel.AddBehavior(spawner);
                                foreach (var Gizmo in Gizmos1)
                                {
                                    Gizmo.tower.SellTower();
                                }
                                Tower.tower.UpdateRootModel(towerModel);
                            }
                        }
                    }
                    
                }));
                var button2 = panel.AddButton(new("Button_", 0, 0, 170, 170), VanillaSprites.GreenBtnSquare, new Action(() =>
                {
                    var Towers = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-MonkeyMachine-4"));
                    var Gizmos1 = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-gizmo1"));
                    foreach (var Tower in Towers)
                        {
                            foreach (var behavior in Game.instance.model.GetTowerFromId("EngineerMonkey-100").GetAttackModels().ToArray())
                            {
                                if (behavior.name.Contains("Spawner"))
                                {
                                
                                    var towerModel = Tower.tower.rootModel.Duplicate().Cast<TowerModel>();
                                    var spawner2 = behavior.Duplicate();
                                towerModel.RemoveBehavior<OverrideCamoDetectionModel>();
                                 var onebefore = towerModel.GetAttackModel("blackholespawn");
                                 var twobefore = towerModel.GetAttackModel("rocketspawn");
                                 var threebefore = towerModel.GetAttackModel("beaconspawn");
                                 towerModel.RemoveBehavior(onebefore);
                                 towerModel.RemoveBehavior(twobefore);
                                 towerModel.RemoveBehavior(threebefore);
                                spawner2.weapons[0].projectile.ApplyDisplay<punch>();
                                spawner2.weapons[0].rate = 35;
                                    spawner2.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                                    spawner2.name = "blackholespawn";
                                    spawner2.weapons[0].projectile.AddBehavior(new CreateTowerModel("Gizmoblackholeplace", ModContent.GetTowerModel<blackholegizmo1>().Duplicate(), 0f, true, true, false, true, false));
                                towerModel.AddBehavior(spawner2);
                                foreach (var Gizmo in Gizmos1)
                                {
                                    Gizmo.tower.SellTower();
                                }
                                    

                                    Tower.tower.UpdateRootModel(towerModel);
                                }
                            }
                        }
                }));
                var button3 = panel.AddButton(new("Button_", 0, -180, 170, 170), VanillaSprites.GreenBtnSquare, new Action(() =>
                {

                    var Towers = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-MonkeyMachine-4"));
                    var Gizmos1 = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-gizmo1"));
                    foreach (var Tower in Towers)
                    {
                        foreach (var behavior in Game.instance.model.GetTowerFromId("EngineerMonkey-100").GetAttackModels().ToArray())
                        {
                            if (behavior.name.Contains("Spawner"))
                            {

                                var towerModel = Tower.tower.rootModel.Duplicate().Cast<TowerModel>();
                                towerModel.AddBehavior(new OverrideCamoDetectionModel("CamoDetect", true));
                                var spawner3 = behavior.Duplicate();
                                var onebefore = towerModel.GetAttackModel("blackholespawn");
                                var twobefore = towerModel.GetAttackModel("rocketspawn");
                                var threebefore = towerModel.GetAttackModel("beaconspawn");
                                towerModel.RemoveBehavior(onebefore);
                                towerModel.RemoveBehavior(twobefore);
                                towerModel.RemoveBehavior(threebefore);
                                spawner3.weapons[0].projectile.ApplyDisplay<punch>();
                                spawner3.weapons[0].rate = 35;
                                spawner3.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                                spawner3.name = "beaconspawn";
                                spawner3.weapons[0].projectile.AddBehavior(new CreateTowerModel("Gizmobeaconeplace", ModContent.GetTowerModel<beacongizmo1>().Duplicate(), 0f, true, true, false, true, false));
                                towerModel.AddBehavior(spawner3);
                                foreach (var Gizmo in Gizmos1)
                                {
                                    Gizmo.tower.SellTower();
                                }


                                Tower.tower.UpdateRootModel(towerModel);
                            }
                        }
                    }
                }));
                button.AddImage(new Info("rocketsign", 0, 0, 130, 130), ModContent.GetTextureGUID<MonkeyMachine.MonkeyMachine>("rocketicon"));
                button2.AddImage(new Info("blackholesign", 0, 0, 130, 130), ModContent.GetTextureGUID<MonkeyMachine.MonkeyMachine>("blackholeicon"));
                button3.AddImage(new Info("Camodetsign", 0, 0, 130, 130), ModContent.GetTextureGUID<MonkeyMachine.MonkeyMachine>("beaconicon"));
            }
        }

        public static void CreatePanel2()
        {
            if (InGame.instance != null)
            {
                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panel_", -2150, 760, 200, 580), VanillaSprites.BrownInsertPanel);
                instance = panel.AddComponent<GizmoMenu>();
                var button = panel.AddButton(new("Button_", 0, 180, 170, 170), VanillaSprites.GreenBtnSquare, new Action(() =>
                {

                    var Towers = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-MonkeyMachine-5"));
                    var Gizmos1 = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-gizmo2"));
                    foreach (var Tower in Towers)
                    {
                        foreach (var behavior in Game.instance.model.GetTowerFromId("EngineerMonkey-100").GetAttackModels().ToArray())
                        {
                            if (behavior.name.Contains("Spawner"))
                            {
                                var towerModel = Tower.tower.rootModel.Duplicate().Cast<TowerModel>();
                                var spawner = behavior.Duplicate();
                                towerModel.RemoveBehavior<OverrideCamoDetectionModel>();
                                var onebefore = towerModel.GetAttackModel("blackholespawn");
                                var twobefore = towerModel.GetAttackModel("rocketspawn");
                                var threebefore = towerModel.GetAttackModel("beaconspawn");
                                towerModel.RemoveBehavior(onebefore);
                                towerModel.RemoveBehavior(twobefore);
                                towerModel.RemoveBehavior(threebefore);
                                spawner.weapons[0].rate = 60;
                                spawner.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                                spawner.weapons[0].projectile.ApplyDisplay<punch>();
                                spawner.name = "rocketspawn";
                                spawner.weapons[0].projectile.AddBehavior(new CreateTowerModel("Gizmorocketplace", ModContent.GetTowerModel<rocketgizmo2>().Duplicate(), 0f, true, true, false, true, false));
                                towerModel.AddBehavior(spawner);
                                foreach (var Gizmo in Gizmos1)
                                {
                                    Gizmo.tower.SellTower();
                                }
                                Tower.tower.UpdateRootModel(towerModel);
                            }
                        }
                    }

                }));
                var button2 = panel.AddButton(new("Button_", 0, 0, 170, 170), VanillaSprites.GreenBtnSquare, new Action(() =>
                {
                    var Towers = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-MonkeyMachine-5"));
                    var Gizmos1 = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-gizmo2"));
                    foreach (var Tower in Towers)
                    {
                        foreach (var behavior in Game.instance.model.GetTowerFromId("EngineerMonkey-100").GetAttackModels().ToArray())
                        {
                            if (behavior.name.Contains("Spawner"))
                            {

                                var towerModel = Tower.tower.rootModel.Duplicate().Cast<TowerModel>();
                                var spawner2 = behavior.Duplicate();
                                towerModel.RemoveBehavior<OverrideCamoDetectionModel>();
                                var onebefore = towerModel.GetAttackModel("blackholespawn");
                                var twobefore = towerModel.GetAttackModel("rocketspawn");
                                var threebefore = towerModel.GetAttackModel("beaconspawn");
                                towerModel.RemoveBehavior(onebefore);
                                towerModel.RemoveBehavior(twobefore);
                                towerModel.RemoveBehavior(threebefore);
                                spawner2.weapons[0].projectile.ApplyDisplay<punch>();
                                spawner2.weapons[0].rate = 60;
                                spawner2.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                                spawner2.name = "blackholespawn";
                                spawner2.weapons[0].projectile.AddBehavior(new CreateTowerModel("Gizmoblackholeplace", ModContent.GetTowerModel<blackholegizmo2>().Duplicate(), 0f, true, true, false, true, false));
                                towerModel.AddBehavior(spawner2);
                                foreach (var Gizmo in Gizmos1)
                                {
                                    Gizmo.tower.SellTower();
                                }


                                Tower.tower.UpdateRootModel(towerModel);
                            }
                        }
                    }
                }));
                var button3 = panel.AddButton(new("Button_", 0, -180, 170, 170), VanillaSprites.GreenBtnSquare, new Action(() =>
                {

                    var Towers = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-MonkeyMachine-5"));
                    var Gizmos1 = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("MonkeyMachine-gizmo2"));
                    foreach (var Tower in Towers)
                    {
                        foreach (var behavior in Game.instance.model.GetTowerFromId("EngineerMonkey-100").GetAttackModels().ToArray())
                        {
                            if (behavior.name.Contains("Spawner"))
                            {

                                var towerModel = Tower.tower.rootModel.Duplicate().Cast<TowerModel>();
                                towerModel.AddBehavior(new OverrideCamoDetectionModel("CamoDetect", true));
                                var spawner3 = behavior.Duplicate();
                                var onebefore = towerModel.GetAttackModel("blackholespawn");
                                var twobefore = towerModel.GetAttackModel("rocketspawn");
                                var threebefore = towerModel.GetAttackModel("beaconspawn");
                                towerModel.RemoveBehavior(onebefore);
                                towerModel.RemoveBehavior(twobefore);
                                towerModel.RemoveBehavior(threebefore);
                                spawner3.weapons[0].projectile.ApplyDisplay<punch>();
                                spawner3.weapons[0].rate = 60;
                                spawner3.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                                spawner3.name = "beaconspawn";
                                spawner3.weapons[0].projectile.AddBehavior(new CreateTowerModel("Gizmobeaconeplace", ModContent.GetTowerModel<beacongizmo2>().Duplicate(), 0f, true, true, false, true, false));
                                towerModel.AddBehavior(spawner3);
                                foreach (var Gizmo in Gizmos1)
                                {
                                    Gizmo.tower.SellTower();
                                }


                                Tower.tower.UpdateRootModel(towerModel);
                            }
                        }
                    }
                }));
                button.AddImage(new Info("rocketsign", 0, 0, 130, 130), ModContent.GetTextureGUID<MonkeyMachine.MonkeyMachine>("rocketicon"));
                button2.AddImage(new Info("blackholesign", 0, 0, 130, 130), ModContent.GetTextureGUID<MonkeyMachine.MonkeyMachine>("blackholeicon"));
                button3.AddImage(new Info("Camodetsign", 0, 0, 130, 130), ModContent.GetTextureGUID<MonkeyMachine.MonkeyMachine>("beaconicon"));
            }
        }
    }
}

using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using GizmoUis;
using Il2Cpp;
using Il2CppAssets.Scripts.Data;
using Il2CppAssets.Scripts.Data.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity.Display.Animation;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNinjaKiwi.Common;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using MelonLoader;
using MonkeyMachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static looks.Displays;

[assembly: MelonInfo(typeof(MonkeyMachine.MonkeyMachine), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace MonkeyMachine
{
    public class MonkeyMachine : BloonsTD6Mod
    {
        public override void OnApplicationStart()
        {
            ModHelper.Msg<MonkeyMachine>("Monkey Machine loaded!");
        }
        public override void OnTowerSelected(Il2CppAssets.Scripts.Simulation.Towers.Tower tower)
        {
            if (tower.towerModel.name.Contains("MonkeyMachine-4"))
            {
                GizmoUis.GizmoMenu.CreatePanel();
            }
            if (tower.towerModel.name.Contains("MonkeyMachine-5"))
            {
                GizmoUis.GizmoMenu.CreatePanel2();
            }
        }
        public override void OnTowerDeselected(Il2CppAssets.Scripts.Simulation.Towers.Tower tower)
        {
            if (tower.towerModel.name.Contains("MonkeyMachine-4"))
            {
                if (GizmoMenu.instance != null)
                {
                    GizmoMenu.instance?.Close();
                }
            }
            if (tower.towerModel.name.Contains("MonkeyMachine-5"))
            {
                if (GizmoMenu.instance != null)
                {
                    GizmoMenu.instance?.Close();
                }
            }
        }
        public override void OnTowerUpgraded(Il2CppAssets.Scripts.Simulation.Towers.Tower tower, string upgradeName, TowerModel towerModel)
        {
            if (tower.towerModel.name.Contains("MonkeyMachine"))
            {
                if (GizmoMenu.instance != null)
                {
                    GizmoMenu.instance?.Close();
                }
            }
        }
        public override void OnWeaponFire(Weapon weapon)
        {
            if (weapon.attack.tower.towerModel.name.Contains("MonkeyMachine-MonkeyMachine"))
            {
                weapon.attack.tower.Node.graphic.GetComponent<Animator>().SetTrigger("Punch");
            }
        }
    }
    public class Monkeymachine : ModTower
    {
        public override string Portrait => "000copy";
        public override string Icon => "000copy";

        public override TowerSet TowerSet => TowerSet.Support;
        public override string BaseTower => TowerType.DartMonkey;
        public override int Cost => 1070;

        public override int TopPathUpgrades => 5;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 5;

        public override string Description => "Uses its arms for melee and a big Rocket for distanced attacks, perfect for dealing with clustered Bloons";
        public override string Name => "MonkeyMachine";
        public override string DisplayName => "Monkey Machine";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<basedisplay>();
            towerModel.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, -5, 7);
            towerModel.radius = Game.instance.model.GetTower(TowerType.SuperMonkey).radius;
            towerModel.range = 65;
            var attackModel = towerModel.GetAttackModel();
            attackModel.range = 25;
            attackModel.weapons[0].Rate = 0.9f;
            attackModel.weapons[0].projectile.pierce = 1;
            attackModel.weapons[0].projectile.ApplyDisplay<punch>();
            attackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().Lifespan = 0.05f;
            attackModel.name = "melee";
            var rocket = Game.instance.model.GetTowerFromId("MortarMonkey-030").GetAttackModel().Duplicate();
            rocket.range = 65;
            rocket.weapons[0].Rate = 5.3f;
            rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetDamageModel().damage = 3;
            rocket.weapons[0].projectile.scale += 3;
            rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Black;
            rocket.RemoveBehavior<RotateToPointerModel>();
            rocket.weapons[0].animateOnMainAttack = false;
            rocket.weapons[0].animation = 0;
            rocket.weapons[0].projectile.ApplyDisplay<target>();
            rocket.weapons[0].projectile.scale -= 2.3f;
            rocket.weapons[0].projectile.AddBehavior(new RotateModel("targetspin", -300.0f));
            var targetlock = Game.instance.model.GetTowerFromId("SniperMonkey").GetAttackModel().Duplicate();
            targetlock.weapons[0].projectile.RemoveBehavior<DamageModel>();
            targetlock.weapons[0].Rate = 5.3f;
            var marker = Game.instance.model.GetTower(TowerType.BombShooter).GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate();
            marker.projectile = Game.instance.model.GetTowerFromId("NinjaMonkey-002").GetAttackModel(1).weapons[0].projectile.Duplicate();
            marker.projectile.GetBehavior<AgeModel>().Lifespan = 0.9f;
            marker.projectile.ApplyDisplay<target>();
            marker.projectile.RemoveBehavior<DamageModel>();
            marker.projectile.AddBehavior(new RotateModel("targetspin", -300.0f));
            marker.projectile.scale += 0.6f;
            rocket.weapons[0].projectile.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, 0, 10);
            targetlock.weapons[0].projectile.AddBehavior(marker);
            targetlock.range = 65;
            targetlock.weapons[0].projectile.ignoreBlockers = true;
            targetlock.weapons[0].projectile.canCollisionBeBlockedByMapLos = false;
            targetlock.attackThroughWalls = true;
            targetlock.ApplyDisplay<punch>();
            targetlock.weapons[0].animateOnMainAttack = false;
            targetlock.weapons[0].animation = 0;
            targetlock.RemoveBehavior<RotateToTargetModel>();
            rocket.RemoveBehaviors<TargetSelectedPointModel>();
            targetlock.name = "rocket_target";
            rocket.name = "rocket_attack";
            //towerModel.AddBehavior(targetlock);
            towerModel.AddBehavior(rocket);
        }
    }
    public class target : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class teslastrike : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class bcclaser : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class punch : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class darkmatter : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class mechoop : ModBuffIcon
    {
        protected override int Order => 1;
        public override string Icon => "u002";
        public override int MaxStackSize => 1;
    }
    public class gizmorangebuff : ModBuffIcon
    {
        protected override int Order => 1;
        public override string Icon => "bufficon400";
        public override int MaxStackSize => 1;
    }
    public class gizmomegabuff : ModBuffIcon
    {
        protected override int Order => 1;
        public override string Icon => "bufficon500";
        public override int MaxStackSize => 1;
    }
    public class beacon : ModBuffIcon
    {
        protected override int Order => 1;
        public override string Icon => "beaconicon";
        public override int MaxStackSize => 1;
    }
    public class u010 : ModUpgrade<Monkeymachine>
    {

        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 360;
        public override string Portrait => "010copy";
        public override string Icon => "u010";
        public override string DisplayName => "Camo Sensor";
        public override string Description => "Rocket attacks can now detect and pop Camo Bloons";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display010>();
            foreach (var attack in towerModel.GetAttackModels())
            {
                if (attack.name.Contains("rocket_attack"))
                {
                    attack.weapons[0].projectile.SetHitCamo(true);
                    attack.weapons[0].projectile.RemoveFilter<FilterInvisibleModel>();
                    attack.RemoveFilter<FilterInvisibleModel>();
                    attack.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.SetHitCamo(true);
                    attack.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.RemoveFilter<FilterInvisibleModel>();
                }
                if (attack.name.Contains("rocket_target"))
                {
                    attack.weapons[0].projectile.SetHitCamo(true);
                    attack.weapons[0].projectile.RemoveFilter<FilterInvisibleModel>();
                    attack.RemoveFilter<FilterInvisibleModel>();
                }
            }
        }

    }
    public class u020 : ModUpgrade<Monkeymachine>
    {

        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 890;
        public override string Portrait => "020copy";
        public override string Icon => "u020";
        public override string DisplayName => "High Voltage";
        public override string Description => "Every attack now electrifies Bloons. Electrified Bloons pop other Bloons that touch them.";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display020>();
            var attackModel = towerModel.GetAttackModel();
            var electro = Game.instance.model.GetTowerFromId("IceMonkey-004").GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<AddBehaviorToBloonModel>().Duplicate();
            electro.mutationId = "Ice:Icicles";
            electro.GetBehavior<CarryProjectileModel>().projectile.ApplyDisplay<punch>();
            electro.GetBehavior<CarryProjectileModel>().projectile.GetDamageModel().damage = 1;
            electro.name = "voltage";
            electro.GetBehavior<CarryProjectileModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Purple | BloonProperties.Lead;
            electro.overlayType = ElectricShockDisplay.CustomOverlayType;
            attackModel.weapons[0].projectile.AddBehavior(electro);
            attackModel.weapons[0].projectile.collisionPasses = new int[] { -1, 0, 1 };
            var rocket = towerModel.GetAttackModel("rocket_attack");
            rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.AddBehavior(electro);
            rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.collisionPasses = new int[] { -1, 0, 1 };

        }

    }
    public class u200 : ModUpgrade<Monkeymachine>
    {

        public override int Path => TOP;
        public override int Tier => 2;
        public override int Cost => 470;
        public override string Portrait => "200copy";
        public override string Icon => "u200";
        public override string DisplayName => "Better Thrusters";
        public override string Description => "Rockets can now reach even further and launch faster";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display200>();
            foreach (var attack in towerModel.GetAttackModels())
            {
                if (attack.name.Contains("rocket"))
                {
                    attack.range += 2.5f;
                    attack.weapons[0].Rate -= 0.4f;
                    towerModel.range += 2.5f;

                }
            }
        }

    }
    public class u001 : ModUpgrade<Monkeymachine>
    {

        public override int Path => BOTTOM;
        public override int Tier => 1;
        public override int Cost => 250;
        public override string Portrait => "001copy";
        public override string Icon => "u001";
        public override string DisplayName => "High-Impact";
        public override string Description => "Melee attacks can now pop all Bloon Types";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display001>();
            foreach (var attack in towerModel.GetAttackModels())
            {
                if (attack.name.Contains("melee"))
                {
                    attack.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                }
            }
        }

    }
    public class u100 : ModUpgrade<Monkeymachine>
    {

        public override int Path => TOP;
        public override int Tier => 1;
        public override int Cost => 350;
        public override string Portrait => "100copy";
        public override string Icon => "u100";
        public override string DisplayName => "Tightened Screws";
        public override string Description => "Melee attacks are now 25% faster";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display100>();
            towerModel.GetAttackModel().weapons[0].Rate -= 0.3f;
        }

    }
    public class u002 : ModUpgrade<Monkeymachine>
    {

        public override int Path => BOTTOM;
        public override int Tier => 2;
        public override int Cost => 650;
        public override string Portrait => "002copy";
        public override string Icon => "u002";
        public override string DisplayName => "Mechooperation";
        public override string Description => "Gives 40% more Range to all Sentries nearby";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display002>();
            TowerFilterModel sentry = new FilterInBaseTowerIdModel("BaseTowerFilter", new string[] { TowerType.Sentry, TowerType.SentryBoom, TowerType.SentryEnergy, TowerType.SentryCrushing, TowerType.SentryCold, TowerType.SentryParagon });
            var buff = new RangeSupportModel("Mechooperation", true, 0.4f, 0f, "mechoop", new Il2CppReferenceArray<TowerFilterModel>(new TowerFilterModel[] { sentry }), false, "", "");
            buff.ApplyBuffIcon<mechoop>();
            towerModel.AddBehavior(buff);
        }

    }
    public class u030 : ModUpgrade<Monkeymachine>
    {

        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 1280;
        public override string Portrait => "030copy";
        public override string Icon => "u030";
        public override string DisplayName => "Tesla Strike";
        public override string Description => "Instead of firing a Rocket, Strikes Bloons with a powerful lightning bolt which deals triple damage to MOAB class Bloons";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display030>();
            var rocket = towerModel.GetAttackModel("rocket_attack");
            rocket.weapons[0].projectile.RemoveBehavior<CreateEffectOnExhaustFractionModel>();
            rocket.weapons[0].RemoveBehavior<EjectEffectModel>();
            var stun = Game.instance.model.GetTowerFromId("BombShooter-400").GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<SlowModel>();
            stun.Lifespan = 0.12f;
            var bolt = Game.instance.model.GetTower(TowerType.MortarMonkey).GetWeapon().projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().Duplicate();
            rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.AddBehavior(new DamageModifierForTagModel("Moabsbd", "Moabs", 3, 1, false, false));
            rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.AddBehavior(stun);
            rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Purple;
            bolt.projectile = Game.instance.model.GetTowerFromId("ObynGreenfoot 3").GetAbility().GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile.Duplicate();
            bolt.projectile.ApplyDisplay<teslastrike>();
            bolt.projectile.RemoveBehavior<CreateEffectOnExhaustedModel>();
            bolt.projectile.GetBehavior<AgeModel>().Lifespan = 0.14f;
            bolt.projectile.pierce = 9999;
            bolt.projectile.RemoveBehavior<DamageModel>();
            bolt.projectile.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, -10, 400);
            bolt.projectile.GetBehavior<DisplayModel>().ignoreRotation = true;
            bolt.projectile.GetBehavior<DisplayModel>().scale /= 3.3f;
            rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.AddBehavior(bolt);
        }

    }
    public class u003 : ModUpgrade<Monkeymachine>
    {

        public override int Path => BOTTOM;
        public override int Tier => 3;
        public override int Cost => 960;
        public override string Portrait => "003copy";
        public override string Icon => "u003";
        public override string DisplayName => "Heavy-Duty Machine";
        public override string Description => "Melee attacks are now slower but deal damage in an Area. Deals extra Damage to Fortified and Ceramic Bloons";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display003>();
            towerModel.GetAttackModel().weapons[0].rate += .25f;
            var aoe = Game.instance.model.GetTowerFromId("BombShooter-300").GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate();
            aoe.projectile.RemoveBehavior<PushBackModel>();
            aoe.projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            towerModel.GetAttackModel().weapons[0].projectile.AddBehavior(aoe);
            towerModel.GetAttackModel().weapons[0].projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Fortified", "Fortified", 1, 4, false, false));
            towerModel.GetAttackModel().weapons[0].projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Ceramic", "Ceramic", 1, 4, false, false));
        }

    }
    public class u300 : ModUpgrade<Monkeymachine>
    {

        public override int Path => TOP;
        public override int Tier => 3;
        public override int Cost => 2300;
        public override string Portrait => "300copy";
        public override string Icon => "u300";
        public override string DisplayName => "Space Cadet";
        public override string Description => "Uses a Laser Cutter for maximized Bloon popping";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display300>();
            towerModel.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, -5, 9);
            towerModel.GetAttackModel().range += 4;
            towerModel.GetAttackModel().weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Purple;
            var cutter = Game.instance.model.GetTowerFromId("BallOfLightTower").GetAttackModel().Duplicate();
            cutter.weapons[0].projectile.GetDamageModel().damage = 1;
            cutter.weapons[0].projectile.pierce = 1;
            cutter.weapons[0].projectile.maxPierce = 1;
            cutter.name = "melee_lasercutter";
            cutter.range = towerModel.GetAttackModel().range;
            cutter.weapons[0].projectile.RemoveBehavior<DamageModifierForTagModel>();
            cutter.weapons[0].rate *= 2.1f;
            cutter.weapons[0].projectile.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, 0, 500);
            cutter.weapons[0].animateOnMainAttack = false;
            cutter.weapons[0].animation = 0;
            cutter.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Purple | BloonProperties.Lead;
            towerModel.AddBehavior(cutter);
            if (towerModel.appliedUpgrades.Contains("MonkeyMachine-u001"))
            {
                foreach (var attack in towerModel.GetAttackModels())
                {
                    if (attack.name.Contains("melee"))
                    {
                        attack.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                    }
                }
            }
        }

    }
    public class u400 : ModUpgrade<Monkeymachine>
    {

        public override int Path => TOP;
        public override int Tier => 4;
        public override int Cost => 13400;
        public override string Portrait => "400copy";
        public override string Icon => "u400";
        public override string DisplayName => "Modular Disassembly";
        public override string Description => "Bring in and Switch between one of 3 Craft Parts, each one specialized for different purposes.(Reselect the tower to show the buttons)";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display400>();

        }

    }
    public class u500 : ModUpgrade<Monkeymachine>
    {

        public override int Path => TOP;
        public override int Tier => 5;
        public override int Cost => 128000;
        public override string Portrait => "500copy";
        public override string Icon => "u500";
        public override string DisplayName => "Bloonar Command Centre";
        public override string Description => "Manages mayhem from orbit... Significantly upgrades all Craft Parts";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display500>();
            var lasers = Game.instance.model.GetTowerFromId("MonkeySub").GetAttackModel().Duplicate();
            lasers.weapons[0].projectile.GetBehavior<TravelStraitModel>().lifespan = 21f;
            lasers.weapons[0].projectile.GetBehavior<TravelStraitModel>().speed *= 2f;
            lasers.weapons[0].projectile.ApplyDisplay<bcclaser>();
            lasers.weapons[0].emission = new ArcEmissionModel("ArcEmissionModel_", 4, 0, 270, null, false, false);
            lasers.weapons[0].projectile.pierce = 10;
            lasers.weapons[0].projectile.maxPierce = 10;
            lasers.weapons[0].animateOnMainAttack = false;
            lasers.weapons[0].animation = 0;
            lasers.weapons[0].rate = 6f;
            lasers.range = 999999;
            lasers.weapons[0].projectile.GetDamageModel().damage = 50;
            towerModel.AddBehavior(lasers);
        }

    }
    public class u040 : ModUpgrade<Monkeymachine>
    {

        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 7200;
        public override string Portrait => "040copy";
        public override string Icon => "u040";
        public override string DisplayName => "Portable Bloonsday Device";
        public override string Description => "Bloonsday Ability: Summon a devastatingly powerful orbital strike that will follow your cursor to disintegrate most Bloons and do serious damage to MOAB-class Bloons.";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display040>();
            var Ability = Game.instance.model.GetTowerFromId("ObynGreenfoot 3").GetAbility().Duplicate();
            AttackModel[] support = { Game.instance.model.GetTowerFromId("Etienne 10").GetAttackModel(1).Duplicate() };
            support[0].weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
            support[0].weapons[0].projectile.AddBehavior(new CreateTowerModel("CreateTowerInAbility", GetTowerModel<bloonsday>(), 0, true, false, false, true, false));
            Ability.GetBehavior<ActivateAttackModel>().attacks = support;
            Ability.RemoveBehavior<CreateSoundOnAbilityModel>();
            Ability.GetBehavior<ActivateAttackModel>().attacks[0].range = 9999;
            Ability.maxActivationsPerRound = 9999999;
            Ability.canActivateBetweenRounds = true;
            Ability.resetCooldownOnTierUpgrade = true;
            Ability.name = "bloonsday";
            Ability.displayName = "Bloonsday";
            Ability.AddBehavior(Game.instance.model.GetTowerFromId("Psi 10").GetAbilities()[1].GetBehavior<CreateSoundOnAbilityModel>().Duplicate());
            Ability.cooldown = 40;
            Ability.icon = GetSpriteReference("deviceability");

            towerModel.AddBehavior(Ability);
        }

    }
    public class u050 : ModUpgrade<Monkeymachine>
    {

        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 31700;
        public override string Portrait => "050copy";
        public override string Icon => "u050";
        public override string DisplayName => "The Collider";
        public override string Description => "Designed for one Purpose... To obliterate every Bloon there is...";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display050>();
            towerModel.RemoveBehavior<AbilityModel>();
            var Ability = Game.instance.model.GetTowerFromId("ObynGreenfoot 3").GetAbility().Duplicate();
            AttackModel[] support = { Game.instance.model.GetTowerFromId("Etienne 10").GetAttackModel(1).Duplicate() };
            support[0].weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
            support[0].weapons[0].projectile.AddBehavior(new CreateTowerModel("CreateTowerInAbility", GetTowerModel<megabloonsday>(), 0, true, false, false, true, false));
            Ability.GetBehavior<ActivateAttackModel>().attacks = support;
            Ability.RemoveBehavior<CreateSoundOnAbilityModel>();
            Ability.GetBehavior<ActivateAttackModel>().attacks[0].range = 9999;
            Ability.maxActivationsPerRound = 9999999;
            Ability.canActivateBetweenRounds = true;
            Ability.resetCooldownOnTierUpgrade = true;
            Ability.name = "megabloonsday";
            Ability.displayName = "MegaBloonsday";
            Ability.AddBehavior(Game.instance.model.GetTowerFromId("Psi 20").GetAbilities()[1].GetBehavior<CreateSoundOnAbilityModel>().Duplicate());
            Ability.cooldown = 52;
            Ability.icon = GetSpriteReference("douglasability");

            towerModel.AddBehavior(Ability);
        }

    }
    public class u004 : ModUpgrade<Monkeymachine>
    {

        public override int Path => BOTTOM;
        public override int Tier => 4;
        public override int Cost => 4600;
        public override string Portrait => "004copy";
        public override string Icon => "u004";
        public override string DisplayName => "Bloon Compactor";
        public override string Description => "Slowly grabs up to 13 Bloons slowly compacting them leaving only their Children behind. Instead of launching rockets, Compacted Bloons are launched as a Projectile.";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display004>();
            towerModel.GetAttackModel().weapons[0].projectile.RemoveBehavior<DamageModel>();
            towerModel.GetAttackModel().weapons[0].projectile.RemoveBehavior<CreateProjectileOnContactModel>();
            towerModel.GetAttackModel().weapons[0].Rate += 0.75f;
            towerModel.GetAttackModel().range = 60;
            towerModel.range = 35;
            var omnom = Game.instance.model.GetTowerFromId("PatFusty 20").GetAbility(1);
            var squeeze = omnom.GetBehavior<ActivateAttackModel>().attacks[0].Duplicate();
            squeeze.weapons[0].projectile.filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] {
                    new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze","Moabs",new Il2CppStringArray(0)),
                    new FilterInvisibleModel("FilterInvisibleModel_",true,false)
                });
            squeeze.weapons[0].projectile.GetBehavior<ProjectileFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] {
                    new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze","Moabs",new Il2CppStringArray(0)),
                    new FilterInvisibleModel("FilterInvisibleModel_",true,false)
                });
            squeeze.GetBehavior<AttackFilterModel>().filters = new Il2CppReferenceArray<FilterModel>(new FilterModel[] {
                    new FilterOutTagModel("FilterOutTagModel_ProjectileSqueeze","Moabs",new Il2CppStringArray(0)),
                    new FilterInvisibleModel("FilterInvisibleModel_",true,false),
                });
            squeeze.GetBehavior<TargetStrongModel>().isSelectable = true;
            squeeze.AddBehavior<TargetFirstModel>(new TargetFirstModel("TargetFirstModel_", true, false));
            squeeze.range = towerModel.GetAttackModel().range;
            squeeze.weapons[0].projectile.pierce = 13;
            squeeze.weapons[0].projectile.maxPierce = 9999;
            squeeze.weapons[0].projectile.CapPierce(999999);
            squeeze.name = "grab";
            squeeze.weapons[0].projectile.scale *= 10;
            var compact = squeeze.weapons[0].projectile.GetBehavior<DelayBloonChildrenSpawningModel>();
            compact.lifespan = 1.5f;
            squeeze.weapons[0].rate = towerModel.GetAttackModel().weapons[0].rate;
            squeeze.weapons[0].projectile.RemoveBehavior<CreateSoundOnDelayedCollisionModel>();
            var rocket = towerModel.GetAttackModel("rocket_attack");
            var marker = towerModel.GetAttackModel("rocket_target");
            towerModel.RemoveBehavior(rocket);
            towerModel.RemoveBehavior(marker);
            towerModel.AddBehavior(squeeze);

            //bigprojectile
            var block = Game.instance.model.GetTowerFromId("DartMonkey-100").GetAttackModel().Duplicate();
            var bounce = Game.instance.model.GetTowerFromId("DartMonkey-300").GetAttackModel().weapons[0].projectile.GetBehavior<ProjectileBlockerCollisionReboundModel>().Duplicate();
            block.weapons[0].projectile.pierce = 40;
            block.weapons[0].projectile.maxPierce = 40;
            block.weapons[0].projectile.GetBehavior<TravelStraitModel>().lifespan = 4;
            block.weapons[0].projectile.GetBehavior<TravelStraitModel>().speed += 2;
            block.weapons[0].rate = 7;
            block.range = towerModel.range;
            block.weapons[0].projectile.AddBehavior(new RotateModel("targetspin", -300.0f));
            block.name = "rocket_block";
            block.weapons[0].projectile.AddBehavior(bounce);
            block.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-100").GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate());
            block.weapons[0].projectile.scale *= 3;
            block.weapons[0].projectile.GetDamageModel().damage = 3;
            block.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            block.weapons[0].projectile.ApplyDisplay<rubberblock>();
            //crosspaths

            var electro = Game.instance.model.GetTowerFromId("IceMonkey-004").GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<AddBehaviorToBloonModel>().Duplicate();
            electro.mutationId = "Ice:Icicles";
            electro.GetBehavior<CarryProjectileModel>().projectile.ApplyDisplay<punch>();
            electro.GetBehavior<CarryProjectileModel>().projectile.GetDamageModel().damage = 1;
            electro.GetBehavior<CarryProjectileModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Purple | BloonProperties.Lead;
            electro.overlayType = ElectricShockDisplay.CustomOverlayType;

            if (towerModel.appliedUpgrades.Contains("MonkeyMachine-u200"))
            {
                towerModel.range += 2.5f;
                block.range += 2.5f;
                block.weapons[0].rate -= .4f;
            }
            if (towerModel.appliedUpgrades.Contains("MonkeyMachine-u010"))
            {
                block.weapons[0].projectile.SetHitCamo(true);
                block.weapons[0].projectile.RemoveFilter<FilterInvisibleModel>();
                block.RemoveFilter<FilterInvisibleModel>();
            }
            if (towerModel.appliedUpgrades.Contains("MonkeyMachine-u020"))
            {
                block.weapons[0].projectile.AddBehavior(electro);
                block.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(electro);
                block.weapons[0].projectile.collisionPasses = new int[] { -1, 0, 1 };
                block.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.collisionPasses = new int[] { -1, 0, 1 };
            }
            towerModel.AddBehavior(block);
        }

    }
    public class u005 : ModUpgrade<Monkeymachine>
    {

        public override int Path => BOTTOM;
        public override int Tier => 5;
        public override int Cost => 49400;
        public override string Portrait => "005copy";
        public override string Icon => "u005";
        public override string DisplayName => "Moab-Flattener";
        public override string Description => "Can now Grab and Compact even more Bloons, and MOAB class Bloons, up to a ZOMG";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<display005>();
            towerModel.GetAttackModel().range += 5;
            var grab = towerModel.GetAttackModel("grab");
            var block = towerModel.GetAttackModel("rocket_block");
            var omnom = Game.instance.model.GetTowerFromId("PatFusty 20").GetAbility(1);
            var squeeze = omnom.GetBehavior<ActivateAttackModel>().attacks[0].Duplicate();
            squeeze.GetBehavior<TargetStrongModel>().isSelectable = true;
            squeeze.AddBehavior<TargetFirstModel>(new TargetFirstModel("TargetFirstModel_", true, false));
            squeeze.range = towerModel.GetAttackModel().range;
            squeeze.weapons[0].projectile.pierce = 6;
            squeeze.weapons[0].projectile.maxPierce = 9999;
            squeeze.weapons[0].projectile.CapPierce(999999);
            squeeze.name = "grabbigtarget";
            squeeze.weapons[0].projectile.scale *= 10;
            var compact = squeeze.weapons[0].projectile.GetBehavior<DelayBloonChildrenSpawningModel>();
            compact.lifespan = 2f;
            squeeze.weapons[0].rate = towerModel.GetAttackModel().weapons[0].rate;
            squeeze.weapons[0].rate += 2f;
            squeeze.weapons[0].projectile.RemoveBehavior<CreateSoundOnDelayedCollisionModel>();
            grab.weapons[0].projectile.pierce = 45;
            towerModel.AddBehavior(squeeze);

            var shard = Game.instance.model.GetTowerFromId("DartMonkey-102").GetAttackModel().weapons[0].projectile.Duplicate();
            shard.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            shard.scale *= 3;
            shard.pierce = 10;
            shard.GetBehavior<TravelStraitModel>().lifespan = 3;
            shard.GetDamageModel().damage = 10;
            shard.AddBehavior(new RotateModel("targetspin", -300.0f));
            shard.ApplyDisplay<rubberblock>();
            block.weapons[0].projectile.GetBehavior<TravelStraitModel>().lifespan = 3f;
            block.weapons[0].projectile.ApplyDisplay<moablock>();
            block.weapons[0].projectile.GetDamageModel().damage += 23;
            block.weapons[0].projectile.AddBehavior(new CreateProjectileOnExpireModel("rubbershards", shard, new ArcEmissionModel("", 5, 0, 360, null, false, false), false));
        }
    }






    public class ElectricShockDisplay : ModDisplay
    {
        // Credit Bergbauer22: DirkTheDino
        private const string BaseOverlayType = "LaserShock";
        public static readonly string CustomOverlayType = "ElectricShock";
        private static SerializableDictionary<string, BloonOverlayScriptable> OverlayTypes => GameData.Instance.bloonOverlays.overlayTypes;
        public override string Name => base.Name + "-" + overlayClass;
        public override PrefabReference BaseDisplayReference => OverlayTypes[BaseOverlayType].assets[overlayClass];
        protected readonly BloonOverlayClass overlayClass;

        public ElectricShockDisplay() { }

        public ElectricShockDisplay(BloonOverlayClass overlayClass)
        {
            this.overlayClass = overlayClass;
        }

        public override IEnumerable<ModContent> Load() => System.Enum.GetValues(typeof(BloonOverlayClass))
            .Cast<BloonOverlayClass>()
            .Select(bc => new ElectricShockDisplay(bc));

        public override void Register()
        {
            base.Register();
            BloonOverlayScriptable electricShock;
            if (!OverlayTypes.ContainsKey(CustomOverlayType))
            {
                electricShock = OverlayTypes[CustomOverlayType] = ScriptableObject.CreateInstance<BloonOverlayScriptable>();
                electricShock.assets = new SerializableDictionary<BloonOverlayClass, PrefabReference>();
                electricShock.displayLayer = OverlayTypes[BaseOverlayType].displayLayer;
            }
            else
            {
                electricShock = OverlayTypes[CustomOverlayType];
            }
            electricShock.assets[overlayClass] = CreatePrefabReference(Id);
        }

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            if (node.GetComponentInChildren<CustomSpriteFrameAnimator>())
            {
                Il2CppSystem.Collections.Generic.List<Sprite> frames = new Il2CppSystem.Collections.Generic.List<Sprite>();
                frames.Add(GetSprite("Electro1"));
                frames.Add(GetSprite("Electro2"));
                frames.Add(GetSprite("Electro3"));
                frames.Add(GetSprite("Electro4"));
                node.GetComponentInChildren<CustomSpriteFrameAnimator>().frames = frames;
            }
            if (node.GetComponentInChildren<MeshRenderer>())
            {
                node.GetComponentInChildren<MeshRenderer>().SetMainTexture(GetTexture(CustomOverlayType));
            }
        }
    }
}
    public class rocketgizmo1 : ModTower
    {
        public override string Portrait => "400gizmo3";
        public override string Name => "gizmo1rocket";
        public override TowerSet TowerSet => TowerSet.Support;
        public override string BaseTower => TowerType.DartMonkey;

        public override bool DontAddToShop => true;
        public override int Cost => 0;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;


        public override string DisplayName => "Rocket Silo";
        public override string Description => " ";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
        towerModel.ApplyDisplay<gizmorocket>();
        towerModel.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(10, 3, 11);
        towerModel.RemoveBehaviors<AttackModel>();
        towerModel.range = 80;
            towerModel.isSubTower = true;
            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 35f, 9, false, false));
            var attackModel = towerModel.GetAttackModel();
            towerModel.AddBehavior(new CreditPopsToParentTowerModel("DamageForMainTower"));
            towerModel.showBuffs = false;
        var rocket = Game.instance.model.GetTowerFromId("MortarMonkey-100").GetAttackModel().Duplicate();
        rocket.range = 80;
        rocket.weapons[0].Rate = 5f;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetDamageModel().damage = 55;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.AddBehavior(new DamageModifierForTagModel("Moabsb", "Moabs", 7, 1, false, false));
        rocket.weapons[0].projectile.GetBehavior<CreateEffectOnExhaustFractionModel>().effectModel.scale *= 1.9f;
        rocket.weapons[0].GetBehavior<EjectEffectModel>().effectModel.scale *= 1.9f;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Black;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.RemoveBehavior<RemoveBloonModifiersModel>();
        rocket.RemoveBehavior<RotateToPointerModel>();
        rocket.weapons[0].animateOnMainAttack = false;
        rocket.weapons[0].animation = 0;
        var targetlock = Game.instance.model.GetTowerFromId("SniperMonkey").GetAttackModel().Duplicate();
        targetlock.weapons[0].projectile.RemoveBehavior<DamageModel>();
        targetlock.weapons[0].Rate = 5f;
        var marker = Game.instance.model.GetTower(TowerType.BombShooter).GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate();
        marker.projectile = Game.instance.model.GetTowerFromId("NinjaMonkey-002").GetAttackModel(1).weapons[0].projectile.Duplicate();
        marker.projectile.GetBehavior<AgeModel>().Lifespan = 0.9f;
        marker.projectile.ApplyDisplay<target>();
        marker.projectile.RemoveBehavior<DamageModel>();
        marker.projectile.AddBehavior(new RotateModel("targetspin", -300.0f));
        marker.projectile.scale += 0.8f;
        marker.projectile.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, 0, 10);
        targetlock.weapons[0].projectile.AddBehavior(marker);
        targetlock.range = 80;
        targetlock.ApplyDisplay<punch>();
        targetlock.weapons[0].animateOnMainAttack = false;
        targetlock.weapons[0].animation = 0;
        targetlock.RemoveBehavior<RotateToTargetModel>();
        rocket.RemoveBehaviors<TargetSelectedPointModel>();
        targetlock.name = "rocketgizmo1_target";
        rocket.name = "rocketgizmo1_attack";
        towerModel.AddBehavior(targetlock);
        towerModel.AddBehavior(rocket);
    }

    }
    public class blackholegizmo1 : ModTower
{
    public override string Portrait => "400gizmo1";
    public override string Name => "gizmo1blackhole";
    public override TowerSet TowerSet => TowerSet.Support;
    public override string BaseTower => TowerType.DartMonkey;

    public override bool DontAddToShop => true;
    public override int Cost => 0;

    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;


    public override string DisplayName => "Black Hole Generator";
    public override string Description => " ";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.ApplyDisplay<gizmoblackhole>();
        towerModel.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(10, 3, 11);
        towerModel.RemoveBehaviors<AttackModel>();
        towerModel.isSubTower = true;
        towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 35f, 9, false, false));
        var attackModel = towerModel.GetAttackModel();
        towerModel.AddBehavior(new CreditPopsToParentTowerModel("DamageForMainTower"));
        towerModel.showBuffs = false;
        var aura = Game.instance.model.GetTowerFromId("TackShooter-004").GetAttackModel().Duplicate();
        aura.weapons[0].projectile.ApplyDisplay<punch>();
        aura.weapons[0].projectile.pierce = 10;
        aura.weapons[0].projectile.scale *= 2;
        aura.weapons[0].rate -= 0.2f;
        aura.weapons[0].projectile.GetDamageModel().damage = 1;
        aura.weapons[0].projectile.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, 0, -100);
        aura.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Purple;
        aura.weapons[0].animateOnMainAttack = false;
        aura.weapons[0].animation = 0;
        towerModel.AddBehavior(aura);
        var orbit = Game.instance.model.GetTower(TowerType.BoomerangMonkey, 5).GetBehavior<OrbitModel>().Duplicate();
        orbit.projectile.ApplyDisplay<darkmatter>();
        orbit.range = aura.range;
        orbit.range -= 24;
        orbit.count = 1;
        orbit.projectile.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, 0, 15);
        orbit.projectile.scale *= 1.2f;
        towerModel.AddBehavior(orbit);
    }

}
    public class beacongizmo1 : ModTower
{
    public override string Portrait => "400gizmo2";
    public override string Name => "gizmo1beacon";
    public override TowerSet TowerSet => TowerSet.Support;
    public override string BaseTower => TowerType.DartMonkey;

    public override bool DontAddToShop => true;
    public override int Cost => 0;

    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;


    public override string DisplayName => "Monkey Beacon";
    public override string Description => " ";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.ApplyDisplay<gizmobeacon>();
        towerModel.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(10, 3, 12);
        towerModel.range += 30;
        towerModel.RemoveBehaviors<AttackModel>();
        towerModel.isSubTower = true;
        towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 35f, 9, false, false));
        var attackModel = towerModel.GetAttackModel();
        towerModel.AddBehavior(new CreditPopsToParentTowerModel("DamageForMainTower"));
        towerModel.showBuffs = false;
        var spikes = new VisibilitySupportModel("", true, "VisionSupportZone", false, null, "", "");
        spikes.ApplyBuffIcon<beacon>();
        var rangebuff = new RangeSupportModel("", true, 0, 10, "duparange", null, false, "", "");
        rangebuff.ApplyBuffIcon<gizmorangebuff>();
        towerModel.AddBehavior(spikes);
        towerModel.AddBehavior(rangebuff);
    }

}

    public class rocketgizmo2 : ModTower
{
    public override string Portrait => "500gizmo3";
    public override string Name => "gizmo2rocket";
    public override TowerSet TowerSet => TowerSet.Support;
    public override string BaseTower => TowerType.DartMonkey;

    public override bool DontAddToShop => true;
    public override int Cost => 0;

    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;


    public override string DisplayName => "Megarocket";
    public override string Description => " ";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.ApplyDisplay<gizmorocketbig>();
        towerModel.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(10, 3, 11);
        towerModel.RemoveBehaviors<AttackModel>();
        towerModel.range = 150;
        towerModel.isSubTower = true;
        towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 60f, 9, false, false));
        var attackModel = towerModel.GetAttackModel();
        towerModel.AddBehavior(new CreditPopsToParentTowerModel("DamageForMainTower"));
        towerModel.showBuffs = false;
        var rocket = Game.instance.model.GetTowerFromId("MonkeySub-040").GetAbility().GetBehavior<ActivateAttackModel>().attacks[0].Duplicate();
        rocket.range = 150;
        rocket.weapons[0].Rate = 5f;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExpireModel>().projectile.GetDamageModel().damage = 1600;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExpireModel>().projectile.GetDamageModel().distributeToChildren = false;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExpireModel>().projectile.GetDamageModel().overrideDistributeBlocker = false;
        rocket.weapons[0].projectile.RemoveBehavior<DamageModel>();
        rocket.weapons[0].projectile.scale += 2;
        rocket.weapons[0].projectile.ApplyDisplay<target>();
        rocket.weapons[0].projectile.AddBehavior(new RotateModel("targetspin", -300.0f));
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExpireModel>().projectile.pierce = 9034;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExpireModel>().projectile.maxPierce = 9050;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExpireModel>().projectile.AddBehavior(new DamageModifierForTagModel("Moabsbe", "Moabs", 11, 1, false, false));
        rocket.weapons[0].projectile.GetBehavior<CreateEffectProjectileAfterTimeModel>().effectModel.scale *= 3.9f;
        rocket.weapons[0].GetBehavior<EjectEffectModel>().effectModel.scale *= 3.9f;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExpireModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Black;
        rocket.weapons[0].projectile.GetBehavior<CreateProjectileOnExpireModel>().projectile.RemoveBehavior<RemoveBloonModifiersModel>();
        rocket.RemoveBehavior<RotateToTargetModel>();
        rocket.GetBehavior<TargetStrongModel>().isSelectable = true;
        rocket.AddBehavior<TargetFirstModel>(new TargetFirstModel("TargetFirstModel_", true, false));
        rocket.weapons[0].animateOnMainAttack = false;
        rocket.weapons[0].animation = 0;
        rocket.name = "rocketgizmo1_attack";
        towerModel.AddBehavior(rocket);
    }

}
    public class blackholegizmo2 : ModTower
{
    public override string Portrait => "500gizmo1";
    public override string Name => "gizmo2blackhole";
    public override TowerSet TowerSet => TowerSet.Support;
    public override string BaseTower => TowerType.DartMonkey;

    public override bool DontAddToShop => true;
    public override int Cost => 0;

    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;


    public override string DisplayName => "Black Hole Accelerator";
    public override string Description => " ";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.ApplyDisplay<gizmoblackholebig>();
        towerModel.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(10, 3, 11);
        towerModel.RemoveBehaviors<AttackModel>();
        towerModel.range += 30; 
        towerModel.isSubTower = true;
        towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 60f, 9, false, false));
        var attackModel = towerModel.GetAttackModel();
        towerModel.AddBehavior(new CreditPopsToParentTowerModel("DamageForMainTower"));
        towerModel.showBuffs = false;
        var aura = Game.instance.model.GetTowerFromId("TackShooter-004").GetAttackModel().Duplicate();
        aura.weapons[0].projectile.ApplyDisplay<punch>();
        aura.weapons[0].projectile.pierce = 37;
        aura.weapons[0].projectile.scale *= 5;
        aura.weapons[0].rate -= 0.325f;
        aura.range += 43;
        aura.weapons[0].projectile.GetDamageModel().damage = 6;
        aura.weapons[0].projectile.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, 0, -100);
        aura.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Purple;
        aura.weapons[0].animateOnMainAttack = false;
        aura.weapons[0].animation = 0;
        towerModel.AddBehavior(aura);
        var orbit = Game.instance.model.GetTower(TowerType.BoomerangMonkey, 5).GetBehavior<OrbitModel>().Duplicate();
        orbit.projectile.ApplyDisplay<darkmatter>();
        orbit.range = aura.range;
        orbit.range -= 64;
        orbit.count = 1;
        orbit.projectile.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, 0, 15);
        orbit.projectile.scale *= 1.5f;
        towerModel.AddBehavior(orbit);
    }

}
    public class beacongizmo2 : ModTower
{
    public override string Portrait => "500gizmo2";
    public override string Name => "gizmo2beacon";
    public override TowerSet TowerSet => TowerSet.Support;
    public override string BaseTower => TowerType.DartMonkey;

    public override bool DontAddToShop => true;
    public override int Cost => 0;

    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;


    public override string DisplayName => "Planetary Probe";
    public override string Description => " ";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.ApplyDisplay<gizmobeaconbig>();
        towerModel.GetBehavior<DisplayModel>().positionOffset = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(10, 3, 39);
        towerModel.range += 3000;
        towerModel.RemoveBehaviors<AttackModel>();
        towerModel.isSubTower = true;
        towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 60f, 9, false, false));
        var attackModel = towerModel.GetAttackModel();
        towerModel.AddBehavior(new CreditPopsToParentTowerModel("DamageForMainTower"));
        towerModel.showBuffs = false;
        var camo = new VisibilitySupportModel("", true, "VisionSupportZone", true, null, "", "");
        camo.ApplyBuffIcon<beacon>();
        var rangebuff = new RangeSupportModel("", true, 0, 20, "duparange2", null, true, "", "");
        rangebuff.ApplyBuffIcon<gizmomegabuff>();
        towerModel.AddBehavior(new RateSupportModel("", 0.7f, true, "duparate2", true, 1, null, "", ""));
        towerModel.AddBehavior(camo);
        towerModel.AddBehavior(rangebuff);
    }

}

public class bloonsday : ModTower
{
        public override TowerSet TowerSet => TowerSet.Support;
      public override string BaseTower => "HeliPilot";
      public override int Cost => 0;
        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 0;
        public override int BottomPathUpgrades => 0;

        public override string Name => "ignorethis";
        public override bool DontAddToShop => true;
        public override string Description => "A";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.isSubTower = true;
        towerModel.radius = Game.instance.model.GetTower(TowerType.Piranha).radius;
        towerModel.AddBehavior(Game.instance.model.GetTowerFromId("Marine").GetBehavior<TowerExpireModel>().Duplicate());
        towerModel.GetBehavior<TowerExpireModel>().lifespan = 10;
        towerModel.display = new PrefabReference() { guidRef = "" };
        towerModel.AddBehavior(new CreditPopsToParentTowerModel("DamageForMainTower"));
        var tower = towerModel.GetBehavior<AirUnitModel>();
        var attackairunitmodel = towerModel.GetAttackModel();
        attackairunitmodel.RemoveBehavior<RotateToTargetAirUnitModel>();
        var weapons = attackairunitmodel.weapons[0];
        var projectile = weapons.projectile;
        attackairunitmodel.range = 1;
        var rotate = tower.GetBehavior<HeliMovementModel>();
        rotate.rotationSpeed = 0f;
        rotate.tiltAngle = 0f;
        towerModel.RemoveBehavior<CreateSoundOnTowerPlaceModel>();
        projectile.ApplyDisplay<punch>();
        var bombbehavior = Game.instance.model.GetTowerFromId("MonkeyAce-030").GetAttackModel(1).Duplicate();
        var bomb = bombbehavior.weapons[0];
        bomb.rate = .55f;
        bomb.RemoveBehavior<CheckAirUnitOverTrackModel>();
        bomb.projectile.RemoveBehavior<CreateEffectOnExhaustFractionModel>();
        bomb.projectile.ApplyDisplay<punch>();
        bomb.projectile.GetBehavior<FallToGroundModel>().timeToTake = .2f;
        var explosion = bomb.projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile;
        explosion.GetDamageModel().damage = 7;
        explosion.GetDamageModel().immuneBloonProperties = BloonProperties.None;
        explosion.RemoveBehavior<CreateSoundOnProjectileExpireModel>();
        explosion.AddBehavior(new DamageModifierForTagModel("Moabsb", "Moabs", 3, 1, false, false));
        explosion.collisionPasses = new[] { -1, 0 };
        bomb.projectile.GetBehavior<DisplayModel>().positionOffset += new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, 0, 5);
        towerModel.AddBehavior(bombbehavior);
        towerModel.GetBehavior<AirUnitModel>().display = CreatePrefabReference<bloonsdaybeam>();
    }

}
public class megabloonsday : ModTower
{
    public override TowerSet TowerSet => TowerSet.Support;
    public override string BaseTower => "HeliPilot-020";
    public override int Cost => 0;
    public override int TopPathUpgrades => 0;
    public override int MiddlePathUpgrades => 0;
    public override int BottomPathUpgrades => 0;

    public override string Name => "ignorethistoo";
    public override bool DontAddToShop => true;
    public override string Description => "A";

    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.isSubTower = true;
        towerModel.radius = Game.instance.model.GetTower(TowerType.Piranha).radius;
        towerModel.AddBehavior(Game.instance.model.GetTowerFromId("Marine").GetBehavior<TowerExpireModel>().Duplicate());
        towerModel.GetBehavior<TowerExpireModel>().lifespan = 23;
        towerModel.AddBehavior(new CreditPopsToParentTowerModel("DamageForMainTower"));
        towerModel.display = new PrefabReference() { guidRef = "" };
        var tower = towerModel.GetBehavior<AirUnitModel>();
        var attackairunitmodel = towerModel.GetAttackModel();
        attackairunitmodel.RemoveBehavior<RotateToTargetAirUnitModel>();
        towerModel.RemoveBehavior<CreateSoundOnTowerPlaceModel>();
        var weapons = attackairunitmodel.weapons[0];
        var projectile = weapons.projectile;
        attackairunitmodel.range = 1;
        var rotate = tower.GetBehavior<HeliMovementModel>();
        rotate.rotationSpeed = 0f;
        rotate.tiltAngle = 0f;
        projectile.ApplyDisplay<punch>();
        var bombbehavior = Game.instance.model.GetTowerFromId("MonkeyAce-030").GetAttackModel(1).Duplicate();
        var bomb = bombbehavior.weapons[0];
        bomb.rate = .25f;
        bomb.RemoveBehavior<CheckAirUnitOverTrackModel>();
        bomb.projectile.RemoveBehavior<CreateEffectOnExhaustFractionModel>();
        bomb.projectile.ApplyDisplay<punch>();
        bomb.projectile.GetBehavior<FallToGroundModel>().timeToTake = .2f;
        var explosion = bomb.projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile;
        explosion.GetDamageModel().damage = 15;
        explosion.GetDamageModel().immuneBloonProperties = BloonProperties.None;
        explosion.RemoveBehavior<CreateSoundOnProjectileExpireModel>();
        explosion.AddBehavior(new DamageModifierForTagModel("Moabsb", "Moabs", 6, 1, false, false));
        explosion.collisionPasses = new[] { -1, 0 };
        bomb.projectile.GetBehavior<DisplayModel>().positionOffset += new Il2CppAssets.Scripts.Simulation.SMath.Vector3(0, 0, 5);
        towerModel.AddBehavior(bombbehavior);
        towerModel.GetBehavior<AirUnitModel>().display = CreatePrefabReference<bloonsdaybeammega>();
    }

}

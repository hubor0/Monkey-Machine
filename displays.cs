using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using UnityEngine;
using static Il2CppAssets.Scripts.Simulation.SMath.Vector3;
using MelonLoader.Utils;
using MonkeyMachine;
using BTD_Mod_Helper.Extensions;


namespace looks
{
  public class Displays
  {

        public class basedisplay : ModCustomDisplay
        {
            public override string AssetBundleName => "monkeymachine";
            public override string PrefabName => "MM";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display003 : ModCustomDisplay
        {
            public override string AssetBundleName => "monkeymachine";
            public override string PrefabName => "MM003";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display030 : ModCustomDisplay
        {
            public override string AssetBundleName => "monkeymachine";
            public override string PrefabName => "MM030";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display040 : ModCustomDisplay
        {
            public override string AssetBundleName => "monkeymachine";
            public override string PrefabName => "MM040";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display300 : ModCustomDisplay
        {
            public override string AssetBundleName => "monkeymachine";
            public override string PrefabName => "MM300";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 64;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    if (!meshRenderer.name.Contains("FlatSkin_MortarMonkey_MortarMonkey.004"))
                    {
                        meshRenderer.ApplyOutlineShader();

                        meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                    }
                }
            }
        }
        public class display004 : ModCustomDisplay
        {
            public override string AssetBundleName => "monkeymachine";
            public override string PrefabName => "MM004";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 65;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display005 : ModCustomDisplay
        {
            public override string AssetBundleName => "monkeymachine";
            public override string PrefabName => "MM005";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 68;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display050 : ModCustomDisplay
        {
            public override string AssetBundleName => "monkeymachine";
            public override string PrefabName => "MM050";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 73;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display400 : ModCustomDisplay
        {
            public override string AssetBundleName => "monkeymachine";
            public override string PrefabName => "MM400";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 68;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    if (!meshRenderer.name.Contains("FlatSkin_MortarMonkey_MortarMonkey.004"))
                    {
                        meshRenderer.ApplyOutlineShader();

                        meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                    }
                }
            }
        }
        public class display500 : ModCustomDisplay
        {
            public override string AssetBundleName => "monkeymachine";
            public override string PrefabName => "MM500";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    if (!meshRenderer.name.Contains("FlatSkin_MortarMonkey_MortarMonkey.004"))
                    {
                        meshRenderer.ApplyOutlineShader();

                        meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                    }
                }
            }
        }
        public class gizmoblackhole : ModCustomDisplay
        {
            public override string AssetBundleName => "gizmo";
            public override string PrefabName => "gizmo1";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 66;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(33 / 255f, 33 / 255f, 33 / 255f));
                }
            }
        }
        public class gizmobeacon : ModCustomDisplay
            {
                public override string AssetBundleName => "gizmo";
                public override string PrefabName => "gizmo2";

                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    node.transform.GetChild(0).transform.localScale *= 65;
                    foreach (var meshRenderer in node.GetMeshRenderers())
                    {
                        meshRenderer.ApplyOutlineShader();

                      meshRenderer.SetOutlineColor(new Color(33 / 255f, 33 / 255f, 33 / 255f));
                    }
                }
            }
        public class gizmorocket : ModCustomDisplay
        {
            public override string AssetBundleName => "gizmo";
            public override string PrefabName => "gizmo3";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 65;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(33 / 255f, 33 / 255f, 33 / 255f));
                }
            }
        }
        public class gizmorocketbig : ModCustomDisplay
        {
            public override string AssetBundleName => "gizmo";
            public override string PrefabName => "gizmo5";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 67;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(33 / 255f, 33 / 255f, 33 / 255f));
                }
            }
        }
        public class gizmoblackholebig : ModCustomDisplay
        {
            public override string AssetBundleName => "gizmo";
            public override string PrefabName => "gizmo6";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 54;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(33 / 255f, 33 / 255f, 33 / 255f));
                }
            }
        }
        public class gizmobeaconbig : ModCustomDisplay
        {
            public override string AssetBundleName => "gizmo";
            public override string PrefabName => "gizmo4";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 26;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(33 / 255f, 33 / 255f, 33 / 255f));
                }
            }
        }
        public class bloonsdaybeam : ModCustomDisplay
        {
            public override string AssetBundleName => "gizmo";
            public override string PrefabName => "bloonsday";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 87;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(0 / 255f, 240 / 255f, 255 / 255f));
                }
            }
        }
        public class bloonsdaybeammega : ModCustomDisplay
        {
            public override string AssetBundleName => "gizmo";
            public override string PrefabName => "megabloonsday";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 125;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(0 / 255f, 240 / 255f, 255 / 255f));
                }
            }
        }
        public class rubberblock : ModCustomDisplay
        {
            public override string AssetBundleName => "t2s";
            public override string PrefabName => "projectileblock";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 17;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class moablock : ModCustomDisplay
        {
            public override string AssetBundleName => "t2s";
            public override string PrefabName => "gigablock";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 38;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }

        public class display001 : ModCustomDisplay
        {
            public override string AssetBundleName => "t2s";
            public override string PrefabName => "MM001";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display002 : ModCustomDisplay
        {
            public override string AssetBundleName => "t2s";
            public override string PrefabName => "MM002";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display010 : ModCustomDisplay
        {
            public override string AssetBundleName => "t2s";
            public override string PrefabName => "MM010";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display020 : ModCustomDisplay
        {
            public override string AssetBundleName => "t2s";
            public override string PrefabName => "MM020";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display100 : ModCustomDisplay
        {
            public override string AssetBundleName => "t2s";
            public override string PrefabName => "MM100";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
        public class display200 : ModCustomDisplay
        {
            public override string AssetBundleName => "t2s";
            public override string PrefabName => "MM200";

            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                node.transform.GetChild(0).transform.localScale *= 69;
                foreach (var meshRenderer in node.GetMeshRenderers())
                {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(73 / 255f, 36 / 255f, 14 / 255f));
                }
            }
        }
    }

}


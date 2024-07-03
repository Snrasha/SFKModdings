
using HarmonyLib;
using SuperFantasyKingdom;
using SuperFantasyKingdom.Tavern;
using SuperFantasyKingdom.TitleScreen;
using SuperFantasyKingdom.UI;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Linq;

namespace TextureReplacement.Patches
{
    public class Patch_Hero
    {


        [HarmonyPatch(typeof(TitleScreenHeroSelectionManager), nameof(TitleScreenHeroSelectionManager.Generate), new[] { typeof(Hero) })]
        static class Patch_TitleScreenHeroSelectionManager_Generate
        {
            [HarmonyPostfix]
            static void Postfix(TitleScreenHeroSelectionManager __instance, Hero hero)
            {
                string name = hero.entityType.ToString();
                
                Sprite icon = TextureReplacement.GetSprite(TextureReplacement.SpritesIcons, name);


                if (icon != null)
                {
                    __instance.heroIcon.sprite = icon;
                }
            }
        }

        [HarmonyPatch(typeof(TitleScreenManager), nameof(TitleScreenManager.NewGame))]
        static class Patch_TitleScreenManager_NewGame
        {

            [HarmonyPostfix]
            static void Postfix(TitleScreenManager __instance)
            {
                TitleScreenSelectHero[] array = TitleScreenHeroSelectionManager.Instance.heroButtons;
                foreach (TitleScreenSelectHero titleScreenSelectHero in array)
                {
                    string name = titleScreenSelectHero.GetHero().ToString();
                    Sprite icon = TextureReplacement.GetSprite(TextureReplacement.SpritesGrid, name);


                    if (icon != null)
                    {
                        titleScreenSelectHero.GetButton().image.sprite= icon;
                        //__instance.heroIcon.sprite = icon;
                    }
                }

            }
        }
        [HarmonyPatch(typeof(UnitBase), nameof(UnitBase.Init), new[] { typeof(int), typeof(bool) })]
        static class Patch_UnitBase_Init
        {

            [HarmonyPostfix]
            static void Postfix(UnitBase __instance, int experience, bool exists)
            {

                string entityname = __instance.entityType.ToString();
                Texture2D entitytext = TextureReplacement.GetTexture(TextureReplacement.SpritesCharacter, entityname);
                if (entitytext)
                {
                    TextureReplacement.SetUnitAnimatorSprite(__instance.unitAnimator, entityname, entitytext);
                }

            }
        }
        [HarmonyPatch(typeof(UIUnitButton), nameof(UIUnitButton.FillUnitData), new[] { typeof(UnitBase) })]
        static class Patch_UIUnitButton_FillUnitData
        {

            [HarmonyPostfix]
            static void Postfix(UIUnitButton __instance, UnitBase unit )
            {
                string name = unit.entityType.ToString();
                Sprite icon = TextureReplacement.GetSprite(TextureReplacement.SpritesIcons, name);


                if (icon != null)
                {
                    __instance.avatar.sprite = icon;
                }
            }
        }
        [HarmonyPatch(typeof(UIUnitButton), nameof(UIUnitButton.OnUnitUpgrade), new[] { typeof(UnitBase) })]
        static class Patch_UIUnitButton_OnUnitUpgrade
        {

            [HarmonyPostfix]
            static void Postfix(UIUnitButton __instance, UnitBase unit)
            {
                if (unit.GetEntityType() == __instance.m_Unit.GetEntityType())
                {
                    string name = unit.entityType.ToString();
                    Sprite icon = TextureReplacement.GetSprite(TextureReplacement.SpritesIcons, name);


                    if (icon != null)
                    {
                        __instance.avatar.sprite = icon;
                    }
                }
            }
        }
        [HarmonyPatch(typeof(UIOverlayGameOverUnit), nameof(UIOverlayGameOverUnit.Init), new[] { typeof(UnitBase) })]
        static class Patch_UIOverlayGameOverUnit_Init
        {

            [HarmonyPostfix]
            static void Postfix(UIOverlayGameOverUnit __instance, UnitBase unit)
            {
                string name = unit.entityType.ToString();
                Sprite icon = TextureReplacement.GetSprite(TextureReplacement.SpritesIcons, name);

                if (icon != null)
                {
                    __instance.icon.sprite = icon;
                }

            }
        }
        [HarmonyPatch(typeof(TavernLevelUp), nameof(TavernLevelUp.Init), new[] { typeof(Unit) })]
        static class Patch_TavernLevelUp_Init
        {

            [HarmonyPostfix]
            static void Postfix(TavernLevelUp __instance, Unit unit)
            {
                string name = unit.entityType.ToString();
                Sprite icon = TextureReplacement.GetSprite(TextureReplacement.SpritesIcons, name);

                if (icon != null)
                {
                    __instance.transform.Find("LevelUp/Icon/Image").GetComponent<Image>().sprite = icon;
                }

            }
        }


        [HarmonyPatch(typeof(TavernStatistics), nameof(TavernStatistics.GenerateDamageMeter))]
        static class Patch_TavernStatistics_GenerateDamageMeter
        {

            [HarmonyPostfix]
            static void Postfix(TavernStatistics __instance)
            {

                Transform transform = __instance.transform.Find("DamageMeterContainer/DamageMeter/ViewPort/Content");

                foreach (Transform child in transform)
                {
                    Transform nameTrans = child.Find("Name");
                    Transform iconTrans = child.Find("Icon");

                    if (nameTrans != null && iconTrans!=null)
                    {
                        TextMeshProUGUI textMeshProUGUI = nameTrans.GetComponent<TextMeshProUGUI>();
                        if(textMeshProUGUI != null)
                        {
                            string[] splits=textMeshProUGUI.text.Split(new string[] { ". " },StringSplitOptions.RemoveEmptyEntries);
                            string unit=splits[splits.Length - 1];
                            Sprite icon = TextureReplacement.GetSprite(TextureReplacement.SpritesIcons, unit);

                            if (icon != null)
                            {
                                iconTrans.GetComponent<Image>().sprite = icon;
                            }
                        }
                    }
                }

            }
        }









    }
}

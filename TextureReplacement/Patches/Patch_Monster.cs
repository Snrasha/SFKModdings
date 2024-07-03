
using HarmonyLib;
using SuperFantasyKingdom;
using UnityEngine;

namespace TextureReplacement.Patches
{
    public class Patch_Monster
    {
        [HarmonyPatch(typeof(Monster), nameof(Monster.SetAnimator))]
        static class Patch_Monster_SetAnimator
        {

            [HarmonyPostfix]
            static void Postfix(Monster __instance)
            {
                //string entityname = __instance.entityType.ToString();
                //Texture2D bunnytext = TextureReplacement.GetTexture(TextureReplacement.SpritesCharacter, "Bunny");
                //if (bunnytext)
                //{
                //    TextureReplacement.SetMonsterAnimatorSprite(__instance.animator, "Bunny", bunnytext);
                //}
            }
        }
    }
}



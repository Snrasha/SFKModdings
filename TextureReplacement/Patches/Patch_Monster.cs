
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
                string entityname = __instance.entityType.ToString();
                Texture2D text = TextureReplacement.GetTexture(TextureReplacement.SpritesCharacter, entityname);
                if (text)
                {
                    TextureReplacement.SetMonsterAnimatorSprite(__instance.animator, entityname, text);
                }
            }
        }
    }
}



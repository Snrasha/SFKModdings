
using HarmonyLib;
using SuperFantasyKingdom;
using UnityEngine;

namespace TextureReplacement.Patches
{
    //public class Patch_Pet
    //{

    //    [HarmonyPatch(typeof(Pet), nameof(Pet.AfterGameStarting), new[] { typeof(int) })]
    //    static class Patch_Pet_AfterGameStarting
    //    {

    //        [HarmonyPostfix]
    //        static void Postfix(Pet __instance, int day)
    //        {
    //            if (__instance.GetSpriteRenderer().gameObject.GetComponent<WorkerSwapTextureSlow>() == null)
    //            {
    //                Texture2D pettext = TextureReplacement.GetTexture(TextureReplacement.SpritesCharacter, "Pet");

    //                if (pettext != null)
    //                {
    //                    WorkerSwapTextureSlow swapTextureSlow = __instance.GetSpriteRenderer().gameObject.AddComponent<WorkerSwapTextureSlow>();
    //                    swapTextureSlow.texture = pettext;
    //                    swapTextureSlow.type = 1;
    //                }
    //            }
    //        }

    //    }
    //}
}

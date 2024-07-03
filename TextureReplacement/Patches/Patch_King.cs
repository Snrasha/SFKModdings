
using HarmonyLib;
using SuperFantasyKingdom;
using UnityEngine;

namespace TextureReplacement.Patches
{
    public class Patch_King
    {


        [HarmonyPatch(typeof(DialogueBox), nameof(DialogueBox.Init), new[] { typeof(string), typeof(Actor), typeof(bool), typeof(bool) })]
static class Patch_DialogueBox_Init
{

    [HarmonyPostfix]
    static void Postfix(DialogueBox __instance, string boxText, Actor actor, bool allowPlayerControl, bool portraitRight)
    {
        if (actor.title.Equals("King"))
        {
            Sprite kingicon = TextureReplacement.GetSprite(TextureReplacement.SpritesIcons, actor.title);


            if (kingicon != null)
            {
                __instance.avatar.sprite = kingicon;
            }

        }

    }
}


        //[HarmonyPatch(typeof(King), nameof(King.Awake))]
        //static class Patch_King_Awake
        //{

        //    [HarmonyPostfix]
        //    static void Postfix(King __instance)
        //    {
        //        foreach (Transform child in __instance.gameObject.transform)
        //        {
        //            if (child.name.Equals("Icon"))
        //            {
        //                Sprite kingicon = TextureReplacement.GetSprite(TextureReplacement.SpritesIcons, __instance.gameObject.name);


        //                if (kingicon != null)
        //                {
        //                    child.GetComponent<SpriteRenderer>().sprite = kingicon;
        //                }
        //            }
        //        }
        //        if (__instance.spriteRenderer.gameObject.GetComponent<WorkerSwapTextureSlow>() == null)
        //        {

        //            Texture2D kingtext = TextureReplacement.GetTexture(TextureReplacement.SpritesCharacter, __instance.gameObject.name);

        //            if (kingtext != null)
        //            {

        //                WorkerSwapTextureSlow swapTextureSlow = __instance.spriteRenderer.gameObject.AddComponent<WorkerSwapTextureSlow>();
        //                swapTextureSlow.texture = kingtext;
        //                swapTextureSlow.type = 1;
        //            }
        //        }
        //    }
        //}
    }
}

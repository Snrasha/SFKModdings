using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static UnityEngine.RectTransform;
using SuperFantasyKingdom;

namespace TextureReplacement
{

    class TextureReplacement
    {

        public static Dictionary<string, Sprite> SpritesIcons;
        public static Dictionary<string, Sprite> SpritesGrid;

        //public static Dictionary<string, Sprite> SpritesCharacter;
        public static Dictionary<string, Texture2D> SpritesCharacter;
        public static Dictionary<string, Sprite[][]> SpritesCharacterCalculated;


        private BepInEx.Logging.ManualLogSource _logger;
        public static Texture2D GetTexture(Dictionary<string, Texture2D> dict, string text)
        {
            if (dict.ContainsKey(text))
            {
                //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
                //  Sprite sprite = Sprite.Create(spriteb.texture, spriteb.rect, spriteb.pivot, 16);
                return dict[text];
            }
            return null;
        }
        public static Sprite[][] GetSpriteSheet(Dictionary<string, Sprite[][]> dict, string text)
        {
            if (dict.ContainsKey(text))
            {
                //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
                //  Sprite sprite = Sprite.Create(spriteb.texture, spriteb.rect, spriteb.pivot, 16);
                return dict[text];
            }
            return null;
        }


        public static Sprite GetSprite(Dictionary<string, Sprite> dict, string text)
        {
            if (dict.ContainsKey(text))
            {
                Sprite spriteb = dict[text];
                //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
                //  Sprite sprite = Sprite.Create(spriteb.texture, spriteb.rect, spriteb.pivot, 16);
                return spriteb;
            }
            return null;
        }
        public TextureReplacement(BepInEx.Logging.ManualLogSource logger)
        {
            _logger = logger;
        }




        public void LoadAllTextures()
        {
            SpritesIcons = new Dictionary<string, Sprite>();
            SpritesGrid = new Dictionary<string, Sprite>();

            
            //SpritesCharacter = new Dictionary<string, Sprite>();
            SpritesCharacter = new Dictionary<string, Texture2D>();
            SpritesCharacterCalculated = new Dictionary<string, Sprite[][]>();



            string appdataPath = Application.dataPath + "/../";

            appdataPath += "/ModsAssets";
            if (!Directory.Exists(appdataPath))
            {
                Directory.CreateDirectory(appdataPath);
            }
            appdataPath += "/Replacements";
            string appdataPathUnits = appdataPath + "/Units";

            if (!Directory.Exists(appdataPath))
            {
                Directory.CreateDirectory(appdataPath);
            }
            if (!Directory.Exists(appdataPathUnits))
            {
                Directory.CreateDirectory(appdataPathUnits);
            }
            string[] units = Directory.GetFiles(appdataPathUnits);
            _logger.LogInfo("Load Units " + units.Length);
            Debug.Log("Load Units " + units.Length);
            foreach (string file in units)
            {
                Debug.Log("Load " + file);
                _logger.LogInfo("Load " + file);

                string[] splut = file.Split('\\');
                string splut2 = splut[splut.Length - 1];
                string[] split2 = splut2.Split('.');
                if (!split2[split2.Length - 1].Equals("png"))
                {
                    continue;
                }
                string[] split = split2[0].Split('_');
                //   Log("TextureReplacement monsters files " + string.Join(",",split));
                if (split.Length > 1)
                {
                    string merge = "";
                    for (int i = 0; i < split.Length - 1; i++)
                    {
                        merge += split[i];
                        if (i < split.Length - 2)
                        {
                            merge += "_";
                        }
                    }
                    Debug.Log("Load " + merge);
                    _logger.LogInfo("Load " + merge);
                    string end = split[split.Length - 1];
                    if ("Icon".Equals(end))
                    {
                        SpritesIcons.Add(  merge, CreateSpriteFromFile(file));
                    }
                    if ("Grid".Equals(end))
                    {
                        SpritesGrid.Add(merge, CreateSpriteFromFile(file));
                    }
                    
                    if ("SpriteSheet".Equals(end))
                    {
                        SpritesCharacter.Add(  merge, CreateTextureFromFile(file,merge));
                    }
                    //SpritesCharacter.Add("SpriteSheet/" + merge, CreateTextureFromFile(file, merge));
                }

                //SpritesCharacter.Add("SpriteSheet/" + merge, CreateSpriteFromFile(file));

            }



            //     AnimatorOverrideController test= new AnimatorOverrideController();



        }

        //public static AnimatorOverrideController CreateAnimationKing(Animator anim)
        //{
        //    //RuntimeAnimatorController clone = runtimeAnimatorController.Clone();
        //    //AnimatorOverrideController newone = new AnimatorOverrideController();
        //    //newone.



        //AnimatorOverrideController overrideController;
        //    if (anim.runtimeAnimatorController.GetType() == typeof(AnimatorOverrideController))
        //    {  //I changed this after the port to C#.
        //        overrideController = anim.runtimeAnimatorController as AnimatorOverrideController;
        //        overrideController.GetOriginalClip(0).
        //        overrideController.SetClip(overrideController.GetOriginalClip(0),
        //         overrideController[oldClipName] = clip;
        //        anim.runtimeAnimatorController = overrideController;
        //    }
        //    else
        //    {
        //        overrideController = new AnimatorOverrideController();

        //        overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
        //        overrideController[oldClipName] = clip;
        //        anim.runtimeAnimatorController = overrideController;
        //    }

        //    return newone;
        //}


        public void CreateFile(string path, string to)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("TextureReplacement.Assets." + path))
                {
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);
                    File.WriteAllBytes(to, bytes);
                }
            }
            catch
            {
            }
        }
        public static Texture2D CreateTextureFromFile(string path,string filename)
        {

            Texture2D tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            try
            {
                using (Stream stream = File.OpenRead(path))
                {
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);
                    tex.filterMode = FilterMode.Point;  // Thought maybe this would help 
                    tex.LoadImage(bytes);

                    //var bytes = File.ReadAllBytes(path);
                    //tex.LoadImage(bytes);
                }
            }
            catch
            {
            }

            // Log("TextureReplacement v tex " + tex.isReadable + " " + tex.width + " " + tex.height);

            //  tex = tex.ToReadable();

            tex.filterMode = FilterMode.Point;
            tex.anisoLevel = 0;
            tex.wrapMode = TextureWrapMode.Clamp;
            tex.name = filename;
            
            tex.Apply();
            return tex;
        }
        public static Sprite CreateSpriteFromFile(string path)
        {

            Texture2D tex = CreateTextureFromFile(path, path);//path.Substring(path.LastIndexOf("/")+1,path.Length));
            Vector2 standardPivot = new Vector2(0.5f, 0.5f);
            //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), standardPivot, 16);
            //Log("TextureReplacement v tex " + tex.isReadable, true);
            sprite.name = tex.name;
            return sprite;
        }
        

        public static void SetUnitAnimatorSprite(UnitAnimator unitAnimator, string namekey, Texture2D texture2D)
        {
            Sprite[][] sprites = GetSpriteSheet(SpritesCharacterCalculated, namekey);
            if (sprites == null)
            {
                //Sprite sprite = monsterAnimatorInline.m_Sprites[0];
                Vector2 standardPivot = new Vector2(0.5f, 0.5f);
                float width = texture2D.width/4;
                float height = texture2D.height / 5;
                sprites = new Sprite[5][];
                for (int i = 0; i < 5; i++)
                {
                    sprites[i] = new Sprite[4];
                    for (int k = 0; k < 4; k++)
                    {
                        Rect rect2 = new Rect((int)(k * width), (int)((i) * height), (int)width, (int)height);
                        sprites[i][k] = Sprite.Create(texture2D, rect2, standardPivot, 16);

                        sprites[i][k].name = namekey + "_" + (i * 4 + k);
                    }
                }
                SpritesCharacterCalculated.Add(namekey, sprites);
            }

            unitAnimator.idle = sprites[4];
            unitAnimator.m_Sprites = sprites[4];
            unitAnimator.walk = sprites[3];
            Sprite[] beforeattack = new Sprite[unitAnimator.beforeAttack.Length];
            Array.Copy(sprites[2],beforeattack, beforeattack.Length);
            unitAnimator.beforeAttack = beforeattack;
            Sprite[] afterattack = new Sprite[unitAnimator.afterAttack.Length];
            Array.Copy(sprites[2], sprites[2].Length- afterattack.Length, afterattack,0, afterattack.Length);
            unitAnimator.afterAttack = afterattack;
            Sprite[] attack = new Sprite[unitAnimator.attack.Length];
            Array.Copy(sprites[2], beforeattack.Length, attack, 0, attack.Length);

            unitAnimator.attack = attack;
            unitAnimator.hit = sprites[1];
            unitAnimator.death = sprites[0];
           


        }

        public static void SetMonsterAnimatorSprite(MonsterAnimatorInline monsterAnimatorInline,string namekey, Texture2D texture2D)
        {


            Sprite[][] sprites = GetSpriteSheet(SpritesCharacterCalculated, namekey);
            if (sprites == null)
            {
                //Sprite sprite = monsterAnimatorInline.m_Sprites[0];
                Vector2 standardPivot = new Vector2(0.5f, 0.5f);
                float width = texture2D.width / 4;
                float height = texture2D.height / 5;
                sprites = new Sprite[5][];
                for (int i = 0; i < 5; i++)
                {
                    sprites[i] = new Sprite[4];
                    for (int k = 0; k < 4; k++)
                    {
                        Rect rect2 = new Rect((int)(k * width), (int)((i) * height),(int)width,(int) height);
                        //spriteSheet.Add(nameused + "_" + inc, Sprite.Create(texture, rect2, 0.5f * Vector2.one, 16f));
                        //sprites[i][k] = Sprite.Create(texture2D, rect2, 0.5f * Vector2.one, 16f);
                        sprites[i][k] = Sprite.Create(texture2D, rect2, standardPivot, 16);
                        
                        sprites[i][k].name= namekey+"_"+(i*4+k);
                    }
                }
                SpritesCharacterCalculated.Add(namekey, sprites);
            }
            monsterAnimatorInline.walk = sprites[3];
            monsterAnimatorInline.walkEvolve = sprites[3];

            monsterAnimatorInline.hit = sprites[1];
            monsterAnimatorInline.hitEvolve = sprites[1];
            monsterAnimatorInline.death = sprites[0];
            monsterAnimatorInline.deathEvolve = sprites[0];
            monsterAnimatorInline.m_Sprites= sprites[3];


        }


        public static Sprite CreateSprite(string path)
        {

            Texture2D tex = new Texture2D(1, 1);
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream("TextureReplacement.Assets." + path))
                {
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, bytes.Length);
                    tex.filterMode = FilterMode.Point;  // Thought maybe this would help 
                    tex.LoadImage(bytes);

                    //var bytes = File.ReadAllBytes(path);
                    //tex.LoadImage(bytes);
                }
            }
            catch
            {
            }

            tex.filterMode = FilterMode.Point;
            tex.anisoLevel = 0;
            tex.wrapMode = TextureWrapMode.Clamp;

            tex.Apply();
            tex.name = path;
            //Log("TextureReplacement v tex " + tex.isReadable, true);
            Vector2 standardPivot = new Vector2(0.5f, 0.5f);
            //Sprite sprite = Sprite.Create(text,new Rect(0,0,text.width,text.height), standardPivot,16);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), standardPivot, 16);
            sprite.name = tex.name;
            //Log("TextureReplacement v tex " + tex.isReadable, true);
            return sprite;
        }
    }
}
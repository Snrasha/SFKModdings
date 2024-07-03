using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TextureReplacement
{
    public class SwapTextureSlow : MonoBehaviour
    {
        public Texture2D texture;
        //public string nameused;

        public int type = 0;//0 worker, 1 king, 2 hero

        // The dictionary containing all the sliced up sprites in the sprite sheet
        private Dictionary<string, Sprite> spriteSheet;
        private Dictionary<string, Sprite> spriteSheetUnused;
        private int nb = 0;

        // The Unity sprite renderer so that we don't have to get it multiple times
        private SpriteRenderer spriteRenderer;

        // Use this for initialization
        private void Start()
        {
            spriteSheetUnused = new Dictionary<string, Sprite>();

            spriteSheet = new Dictionary<string, Sprite>();
            // Get and cache the sprite renderer for this game object
            this.spriteRenderer = GetComponent<SpriteRenderer>();

            this.LoadSpriteSheet();
        }

        // Runs after the animation has done its work
        private void LateUpdate()
        {
            // Swap out the sprite to be rendered by its name
            // Important: The name of the sprite must be the same!
            if (texture == null)
            {
                if (!this.spriteSheet.ContainsKey(this.spriteRenderer.sprite.name))
                {
                    string[] split = this.spriteRenderer.sprite.name.Split('_');
                    string number = split[split.Length - 1];
                    if (number.Length > 0)
                    {
                        if (this.spriteSheetUnused.ContainsKey(number))
                        {
                            this.spriteSheet.Add(this.spriteRenderer.sprite.name, this.spriteSheetUnused[number]);
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                this.spriteRenderer.sprite = this.spriteSheet[this.spriteRenderer.sprite.name];
            }
        }

        // Loads the sprites from a sprite sheet
        private void LoadSpriteSheet()
        {
            //string nameused = spriteRenderer.sprite.name;
            //string[] split = nameused.Split('_');
            float width = spriteRenderer.sprite.rect.width;
            float height = spriteRenderer.sprite.rect.height;

            //nameused = split[0];


            if (type == 1)
            {
                int[] li = new int[] { 0, 1, 4 };
                nb = 0;

                for (int k = 0; k < 4; k++)
                {
                    foreach (int i in li)
                    {
                        Rect rect2 = new Rect(k * width, (i) * height, width, height);
                        //spriteSheet.Add(nameused + "_" + inc, Sprite.Create(texture, rect2, 0.5f * Vector2.one, 16f));

                        spriteSheetUnused.Add(nb + "", Sprite.Create(texture, rect2, 0.5f * Vector2.one, 16f));
                        nb++;
                    }
                }

            }
            texture = null;

        }
    }
}


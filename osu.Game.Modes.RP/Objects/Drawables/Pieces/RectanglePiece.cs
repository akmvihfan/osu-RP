﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Transforms;
using OpenTK;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;

namespace osu.Game.Modes.RP.Objects.Drawables.Pieces
{
    /// <summary>
    /// 用來顯示矩形元素
    /// 可以設定長寬，還有顏色
    /// </summary>
    class RectanglePiece : Container
    {
        /// <summary>
        /// 設定物件texture
        /// </summary>
        public String Resource= SkinManager.RPSkinManager.GetRectangleTexture();

        /// <summary>
        /// 物件的大小剛好是100*100
        /// </summary>
        private Sprite rectangle;

        /// <summary>
        /// 會根據初始設定的 width Height 改變物件的scale
        /// </summary>
        /// <param name="width"></param>
        /// <param name="Height"></param>
        public RectanglePiece(float width,float Height)
        {
            Size = new Vector2(width, Height);
            CornerRadius = DrawSize.X / 2;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Children = new Drawable[]
            {
                rectangle = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size=new Vector2(100,100),
                },
            };

            //改變大小
            ChangeSize(width, Height);
        }

        /// <summary>
        /// 改變大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="Height"></param>
        public void ChangeSize(float width, float Height)
        {
            rectangle.Scale = new Vector2(width / 100, Height / 100);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            rectangle.Texture = textures.Get(Resource);
        }
    }
}

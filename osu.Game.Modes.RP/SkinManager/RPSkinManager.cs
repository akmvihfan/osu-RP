﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Modes.RP.Objects.type;
using osu.Game.Modes.RP.Objects;
using OpenTK.Graphics;
using osu.Game.Modes.RP.Objects.Drawables;
using OpenTK;

namespace osu.Game.Modes.RP.SkinManager
{
    /// <summary>
    /// 負責管理RP 模式底下 Skin相關路徑位置
    /// </summary>
    public static class RPSkinManager
    {
        private const string RP_HIT_EFFECT_FOLDER = @"Play/RP/HitEffect/";
        private const string RP_LOAD_EFFECT_FOLDER = @"Play/RP/LoadEffect/";
        private const string RP_OBJECT_FOLDER = @"Play/RP/HitObject/";
        private const string RP_NUMBER_FOLDER = @"Play/RP/Number/";
        private const string RP_SCORE_FOLDER = @"Play/RP/Score/";
        private const string RP_KEYCOUNTER_FOLDER = @"Play/RP/KeyCounter/";
        private const string RP_PLAYFIELD_FOLDER = @"Play/RP/Playfield/";

        private const string RP_CONFIG_FOLDER = @"Play/RP/Config/";

        private const string JPG_FORMAT = @".jpg";
        private const string PNG_FORMAT = @".png";

        /// <summary>
        /// RP指標
        /// </summary>
        public static string RPPointer = RP_OBJECT_FOLDER + @"Common/" + @"RP-Pointer@2x_remove";

        /// <summary>
        /// 取得數字
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetNumber(int number)
        {
            return "";
        }

        /// <summary>
        /// 按鈕要用的icon
        /// </summary>
        /// <returns></returns>
        public static string GetKeyLayoutButtonIcon(RpBaseHitObjectType.Shape Type)
        {
            return RP_KEYCOUNTER_FOLDER + Type.ToString();
        }

        /// <summary>
        /// 按鈕顏色
        /// </summary>
        /// <returns></returns>
        public static Vector4 GetKeyLayoutButtonShage(RpBaseHitObjectType.Shape Type)
        {
            switch (Type)
            {
                case RpBaseHitObjectType.Shape.Circle:
                    return new Vector4(255, 255, 255, 255);
                case RpBaseHitObjectType.Shape.Cross:
                    return new Vector4(255, 255, 255, 255);
                case RpBaseHitObjectType.Shape.Square:
                    return new Vector4(255, 255, 255, 255);
                case RpBaseHitObjectType.Shape.Triangle:
                    return new Vector4(255, 255, 255, 255);
                case RpBaseHitObjectType.Shape.ContainerPress:
                    return new Vector4(255, 255, 255, 255);
            }
            return new Vector4(255, 255, 255, 255);
        }

        /// <summary>
        /// RP物件聲音
        /// </summary>
        /// <param name="baseHitObject"></param>
        /// <returns></returns>
        public static string GetRPHitSound(BaseHitObject baseHitObject)
        {
            return "";
        }

        public static string GetRPHitEffect(RPScoreResult result,string resource)
        {
            return RP_HIT_EFFECT_FOLDER + result.ToString() + @"/" + resource;
        }

        public static string GetDecisionLineTexture()
        {
            return RP_PLAYFIELD_FOLDER + "DecisionLine";
        }


        public static string GetBeatLineTexture()
        {
            return RP_PLAYFIELD_FOLDER + "DecisionLine";
        }

        public static string GetRectangleTexture()
        {
            return RP_PLAYFIELD_FOLDER + "Background";
        }

        /// <summary>
        /// 打擊特效
        /// </summary>
        /// <returns></returns>
        public static string GetRPLoadEffect()
        {
            return RP_LOAD_EFFECT_FOLDER + @"Load";
        }

        /// <summary>
        /// 取得一個Note的開頭物件
        /// </summary>
        /// <param name="baseHitObject"></param>
        /// <returns></returns>
        public static string GetStartObjectImageNameByType(BaseHitObject baseHitObject)
        {
            return GetObjectImagePathByType(baseHitObject, false) + GetImageNameByType(baseHitObject);
        }

        /// <summary>
        /// 取得一個Note的結尾物件
        /// </summary>
        /// <param name="baseHitObject"></param>
        /// <returns></returns>
        public static string GetEndObjectImageNameByType(BaseHitObject baseHitObject)
        {
            return GetObjectImagePathByType(baseHitObject, true) + GetImageNameByType(baseHitObject);
        }

        /// <summary>
        /// 取得 開始物件的遮罩
        /// </summary>
        /// <param name="baseHitObject"></param>
        /// <returns></returns>
        public static string GetStartObjectMaskByType(BaseHitObject baseHitObject)
        {
            return GetObjectImagePathByType(baseHitObject, false) + "Mask";
        }

        /// <summary>
        /// 取得 結束物件的遮罩
        /// </summary>
        /// <param name="baseHitObject"></param>
        /// <returns></returns>
        public static string GetEndObjectMaskByType(BaseHitObject baseHitObject)
        {
            return GetObjectImagePathByType(baseHitObject, true) + "Mask";
        }

        /// <summary>
        /// 取得 開始物件的遮罩
        /// </summary>
        /// <param name="baseHitObject"></param>
        /// <returns></returns>
        public static string GetStartObjectBackgroundByType(BaseHitObject baseHitObject)
        {
            string str= GetObjectImagePathByType(baseHitObject, false) + "background-" + GetImageNameByType(baseHitObject);
            return str;
        }

        /// <summary>
        /// 取得 結束物件的遮罩
        /// </summary>
        /// <param name="baseHitObject"></param>
        /// <returns></returns>
        public static string GetEndObjectBackgroundByType(BaseHitObject baseHitObject)
        {
            return GetObjectImagePathByType(baseHitObject, true) + "background-" + GetImageNameByType(baseHitObject);
        }

        /// <summary>
        /// 根據物件取得對應的顏色
        /// 會在之後實作
        /// </summary>
        /// <returns></returns>
        public static Color4 GetColor(BaseHitObject baseHitObject)
        {
            Color4 colour = new Color4(255, 255, 255, 255);
            return colour;
        }

        /// <summary>
        /// 根據型態取得物件圖片路徑
        /// </summary>
        /// <param name="baseHitObject"></param>
        /// <returns></returns>
        private static string GetObjectImagePathByType(BaseHitObject baseHitObject, bool end = false)
        {
            string fileName = "";

            //物件是開頭還是尾巴
            if (end)
            {
                fileName = @"End/" + fileName;
            }
            else
            {
                fileName = @"Start/" + fileName;
            }

            //根據模式去命名資料夾
            fileName = baseHitObject.ObjectType.ToString() + @"/" + fileName;

            //如果是黃金模式(家分模式)
            if (baseHitObject.Special == RpBaseHitObjectType.Special.Normal)
            {
                fileName = @"Normal/" + fileName;
            }
            else
            {
                fileName = @"Special/" + fileName;
            }
            
            return RP_OBJECT_FOLDER + fileName;
        }

        /// <summary>
        /// 圖片名稱
        /// </summary>
        /// <param name="baseHitObject"></param>
        /// <param name="border"></param>
        /// <returns></returns>
        private static string GetImageNameByType(BaseHitObject baseHitObject)
        {
            string fileName = null;
            switch (baseHitObject.Shape)
            {
                case RpBaseHitObjectType.Shape.Triangle:
                    fileName = @"Up";
                    break;
                case RpBaseHitObjectType.Shape.Cross:
                    fileName = @"Down";
                    break;
                case RpBaseHitObjectType.Shape.Square:
                    fileName = @"Left";
                    break;
                case RpBaseHitObjectType.Shape.Circle:
                    fileName = @"Right";
                    break;
                case RpBaseHitObjectType.Shape.Special:
                    fileName = @"Star";
                    break;
                case RpBaseHitObjectType.Shape.ContainerPress:
                    fileName = @"Left";
                    break;
                default:
                    return @"RP_Unknown";
            }
            return fileName;
        }

    }
}

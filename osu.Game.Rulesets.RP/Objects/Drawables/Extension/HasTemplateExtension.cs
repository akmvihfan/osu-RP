﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.MathUtils;
using osu.Game.Rulesets.RP.Objects.Drawables.Calculator;
using osu.Game.Rulesets.RP.Objects.Drawables.Component.Interface;
using osu.Game.Rulesets.RP.Objects.Drawables.Interface;
using OpenTK;

namespace osu.Game.Rulesets.RP.Objects.Drawables.Extension
{
    public static class HasTemplateExtension
    {
        private static PathPrecentageCounter PathPrecentageCounter=new PathPrecentageCounter();

        public static void InitialTemplate(this IHasTemplate drawableObject)
        {

            //set all attribute form object to drawable component
            drawableObject.UpdateObjectToDrawable();

            //adding all component into template
            InitialChild(drawableObject);
        }

        //adding all component into template
        private static void InitialChild(this IHasTemplate drawableObject)
        {
            (drawableObject as Container).Children = drawableObject.Components.Select(s => s as Container).ToArray();
        }

        //Delay time
        public static double DelayTime => 0;

        //update progress
        public static void UpdateTemplate(this IHasTemplate drawableObject,double currentTime)
        {
            //start progress
            var startProgress = PathPrecentageCounter.CalculatePrecentage(drawableObject.RpObject.StartTime - currentTime + DelayTime, 1);
            //end progress
            var endProgress = PathPrecentageCounter.CalculatePrecentage(drawableObject.RpObject.StartTime - currentTime + DelayTime, 1);

            //影響程度
            var CurveEasingTypesPrecentage = 0;

            //修正
            startProgress = MathHelper.Clamp(startProgress, 0, 1);
            endProgress = MathHelper.Clamp(endProgress, 0, 1);
            //fix precentage by EasingTypes
            startProgress = Interpolation.ApplyEasing(Easing.None, startProgress, 0, 1, 1) * CurveEasingTypesPrecentage + startProgress * (1 - CurveEasingTypesPrecentage);
            endProgress = Interpolation.ApplyEasing(Easing.None, endProgress, 0, 1, 1) * CurveEasingTypesPrecentage + endProgress * (1 - CurveEasingTypesPrecentage);

            //update all 
            foreach (IComponentUpdateEachFrame single in drawableObject.Components.Where(n => n is IComponentUpdateEachFrame))
            {
                single.UpdateProgress(startProgress, endProgress);
            }
        }

        //Fade in
        public static void FadeInComponents(this IHasTemplate drawableObject,double time = 0)
        {
            foreach (IComponentBase single in drawableObject.Components)
            {
                single.FadeIn(time);
            }
        }

        //fade out
        public static void FadeOutComponents(this IHasTemplate drawableObject,double time = 0)
        {
            foreach (IComponentBase single in drawableObject.Components)
            {
                single.FadeOut(time);
            }
        }

        /*
        /// <summary>
        /// AddTemplateToChild
        /// TODO : remove
        /// </summary>
        /// <param name="drawableObject"></param>
        private static void AddTemplateToChild(this IHasTemplate drawableObject)
        {
            if(drawableObject!=null)
                drawableObject.Dispose();
            drawableObject.Template = null;

            //要重新new 一個
            drawableObject.Template = new Template.Template(drawableObject.RpObject, drawableObject.Components);

            //TODO : add or remove only template
            (drawableObject as Container).Children = new Drawable[]
            {
                drawableObject.Template
            };
        }

        /// <summary>
        /// RemoveTemplateFromChild
        /// </summary>
        /// <param name="drawableObject"></param>
        private static void RemoveTemplateFromChild(this IHasTemplate drawableObject)
        {
            //TODO : add or remove only template
            (drawableObject as Container).Children = new Drawable[]
            {
                //drawableObject.Template
            };

            // (drawableObject as Container).Children.ToList().Remove(drawableObject.Template);

            if (drawableObject.Template != null)
                drawableObject.Template.Dispose();
            drawableObject.Template = null;

            //要重新new 一個
            drawableObject.Template = new Template.Template(drawableObject.RpObject, drawableObject.Components);
            drawableObject.Template.Initial();
        }
        */

    }
}

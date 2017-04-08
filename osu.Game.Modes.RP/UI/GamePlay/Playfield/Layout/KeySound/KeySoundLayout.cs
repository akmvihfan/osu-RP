﻿using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Input;
using osu.Game.Modes.RP.KeyManager;
using osu.Game.Modes.RP.Objects.type;
using OpenTK.Input;

namespace osu.Game.Modes.RP.UI.GamePlay.Playfield.Layout.KeySound
{
    /// <summary>
    ///     放置顯示在打擊物件之前的東西
    ///     例如DecisionLine
    /// </summary>
    internal class KeySoundLayout : BaseGamePlayLayout
    {
        protected List<SampleChannel> ShapeSample = new List<SampleChannel>();
        protected List<SampleChannel> ContainerPressSample = new List<SampleChannel>();
        private readonly List<Key> _listShapeKeys = new List<Key>();
        private readonly List<Key> _containerPressKeys = new List<Key>();

        private InputState _lastState;

        public KeySoundLayout()
        {
            var keyList = RpKeyManager.GetCurrentKeyConfig();

            foreach (var singleKey in keyList.KeyDictionary)
                if (singleKey.Type == RpBaseHitObjectType.Shape.ContainerPress)
                    _containerPressKeys.Add(singleKey.Key);
                else
                    _listShapeKeys.Add(singleKey.Key);
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if (args.Repeat)
                return false;
            var hitIndex = 0;
            if (_listShapeKeys.Contains(args.Key))
                for (var i = 0; i < _listShapeKeys.Count; i++)
                    if (_listShapeKeys[i] == args.Key)
                        PlayShapeSample(i);
            else if (_containerPressKeys.Contains(args.Key))
                for (var i = 0; i < _containerPressKeys.Count; i++)
                    if (_containerPressKeys[i] == args.Key)
                        PlayContainerPressSample(i);
            return base.OnKeyDown(state, args);
        }

        protected virtual void PlayShapeSample(int keyIndex)
        {
            ShapeSample[keyIndex]?.Play();
        }

        protected virtual void PlayContainerPressSample(int keyIndex)
        {
            ShapeSample[keyIndex]?.Play();
        }

        [BackgroundDependencyLoader]
        private void load(AudioManager audio)
        {
            foreach (var single in _listShapeKeys)
                ShapeSample.Add(audio.Sample.Get($@"RPKey/Key-Shape"));
            foreach (var single in _containerPressKeys)
                ContainerPressSample.Add(audio.Sample.Get($@"RPKey/Key-ContainerPress"));
        }
    }
}

﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;
using osu.Game.Modes.RP.Beatmaps.OsuBeatmap.Parameter;

namespace osu.Game.Modes.RP.Beatmaps.OsuBeatmap.Convertor.Syntax.Beat
{
    internal class CalRpBeat
    {
        //目前當作兩拍
        private readonly int _beatNumber = 2; //
        //目前是第幾拍
        private int _nowBeat; //0 1 0 1
        //目前是不是新的一拍
        private bool _isNewCombo;
        //要不要出現多個物件
        private readonly bool _multiObject = false;
        //目前出現的音符要不要有規律
        private readonly bool _regular = false;

        //參數
        private int _index;
        //丟進來的參數
        private List<HitObjectConvertParameter> _scannParameter;
        //返回參數
        private List<HitObjectConvertParameter> _returnParameter;

        public void SetIndex(int index)
        {
            _index = index;
        }

        public void SetParameter(List<HitObjectConvertParameter> scannParameter)
        {
            _scannParameter = scannParameter;
        }

        public void SetComvertedParameter(List<HitObjectConvertParameter> returnParameter)
        {
            _returnParameter = returnParameter;
        }

        /// <summary>
        ///     處理
        /// </summary>
        public void Process()
        {
            //如果是NewCombo
            if (_scannParameter[_index].ScanParameter.NewComboScannerResult > 0)
            {
                _nowBeat = 0;
            }
            else
            {
                _nowBeat++;
                if (_nowBeat >= _beatNumber)
                    _nowBeat = 0;
            }
        }

        /// <summary>
        ///     是不是新的Combo
        /// </summary>
        /// <returns></returns>
        public bool GetNewCombo()
        {
            return _isNewCombo;
        }

        /// <summary>
        ///     目前節拍，例如兩拍為基礎
        ///     0 1  0 1
        /// </summary>
        /// <returns></returns>
        public int nowBeat()
        {
            return _nowBeat;
        }

        /// <summary>
        ///     如過是Multi，就代表 RPMultipleHitCalculator 會生產多個物件的
        /// </summary>
        /// <returns></returns>
        public bool IsMulti()
        {
            return _multiObject;
        }

        /// <summary>
        ///     目前出現的物件要不要有規律(代表不要有過多亂數產生)
        /// </summary>
        /// <returns></returns>
        public bool IsRegular()
        {
            return _regular;
        }

        public int GetBeatNumber()
        {
            return _beatNumber;
        }
    }
}
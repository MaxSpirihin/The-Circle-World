using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.gui
{
    public class PrefSaver
    {
        private const string prefCompleteLevels = "CW_prefCompleteLevels";
        public const int AllLevels = 36;

        public static void SetCompleteLevels(int levels)
        {
            PlayerPrefs.SetInt(prefCompleteLevels, levels);
        }


        public static int GetCompleteLevels()
        {
            return PlayerPrefs.GetInt(prefCompleteLevels, 0);
        }

        public static int GetProgressPercent()
        {
            return 100 * GetCompleteLevels() / AllLevels;
        }

        public static void Clear()
        {
            SetCompleteLevels(0);
        }
    }
}

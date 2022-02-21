using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.EstUtils
{
    public static class NumberFormat
    {
        static CultureInfo cult = CultureInfo.GetCultureInfo("hu-HU");
    
        public static string Estimation(float n, int decimals=1)
        {
            if (n > 999999999)
                return (n / 1000000000).ToString("N"+decimals.ToString(), cult) + 'B';
            else if (n > 999999)
                return (n / 1000000).ToString("N"+decimals.ToString(), cult) + 'M';
            else if (n > 999)
                return (n / 1000).ToString("N"+decimals.ToString(), cult) + 'k';
            return n.ToString(cult).ToString();
        } 

        public static float ParseEstimation(string rest)
        {
            float mult = 1;

            if (rest.Contains('M')) { mult = 1000000f; rest = rest.Replace("M", ""); }
            else if (rest.Contains('k')) { mult = 1000f; rest = rest.Replace("k", ""); }

            return float.Parse(rest) * mult;
        }

        public static void GetProdException(GameObject obj, Exception ex)
        {
            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(0);

            string errmsg = $"{frame.GetFileName()} at Line {frame.GetFileLineNumber()}\n{ex.Message}";

            obj.SetActive(true);
            obj.transform.Find("Text").GetComponent<Text>().text = errmsg;
        }

        static int[] decimalValue = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        static string[] romanNumeral = new string[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        public static string ToRoman(int x)
        {
            var romanized = new StringBuilder();

            for (var index = 0; index < decimalValue.Length; index++)
            {
                while (decimalValue[index] <= x)
                {
                    romanized.Append(romanNumeral[index]);
                    x -= decimalValue[index];
                }
            }

            return romanized.ToString();
        }
    }

}
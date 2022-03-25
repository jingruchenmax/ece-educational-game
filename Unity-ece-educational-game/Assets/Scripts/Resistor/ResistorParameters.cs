using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace resistor
{
    public enum multiplierValue
    {
        x1_Black,
        x10_Brown,
        x100_Red,
        x1k_Orange,
        x10k_Yellow,
        x100k_Green,
        x1M_Blue,
        x10M_Violet,
        x10_1_Gold,
        x10_2_Silver

    }
    public enum bandValue
    {
        _0_Black,
        _1_Brown,
        _2_Red,
        _3_Orange,
        _4_Yellow,
        _5_Green,
        _6_Blue,
        _7_Violet,
        _8_Gray,
        _9_White
    }
    public enum toleranceValue
    {
        default_Black,
        F_Brown,
        G_Red,
        D_Green,
        C_Blue,
        B_Violet,
        J_Gold,
        K_Silver
    }
    [Serializable]
    public class Band
    {
        public bandValue value;
        public Color color { get { return ColorCode(); } }
        Color ColorCode()
        {
            Color tempColor = Color.black;
            switch ((int)value)
            {
                case 0: tempColor = Color.black; break;
                case 1: tempColor = new Color(144 / 255.0f, 97 / 255.0f, 25 / 255.0f); break;
                case 2: tempColor = Color.red; break;
                case 3: tempColor = new Color(255 / 255.0f, 102 / 255.0f, 0); break;
                case 4: tempColor = Color.yellow; break;
                case 5: tempColor = Color.green; break;
                case 6: tempColor = Color.blue; break;
                case 7: tempColor = new Color(204 / 255.0f, 0, 204 / 255.0f); break;
                case 8: tempColor = new Color(204 / 255.0f, 204 / 255.0f, 204 / 255.0f); break;
                case 9: tempColor = Color.white; break;
            }
            return tempColor;
        }
    }
    [Serializable]
    public class MultiplierBand
    {

        public multiplierValue value;
        public Color color { get { return ColorCode(); } }
        Color ColorCode()
        {
            Color tempColor = Color.black;
            switch ((int)value)
            {
                case 0: tempColor = Color.black; break;
                case 1: tempColor = new Color(144 / 255.0f, 97 / 255.0f, 25 / 255.0f); break;
                case 2: tempColor = Color.red; break;
                case 3: tempColor = new Color(255 / 255.0f, 102 / 255.0f, 0); break;
                case 4: tempColor = Color.yellow; break;
                case 5: tempColor = Color.green; break;
                case 6: tempColor = Color.blue; break;
                case 7: tempColor = new Color(204 / 255.0f, 0, 204 / 255.0f); break;
                case 8: tempColor = new Color(216 / 255.0f, 169 / 255.0f, 21 / 255.0f); break;
                case 9: tempColor = new Color(204 / 255.0f, 204 / 255.0f, 204 / 255.0f); break;
            }
            return tempColor;
        }
    }
    [Serializable]
    public class ToleranceBand
    {

        public toleranceValue value;
        public Color color { get { return ColorCode(); } }
        Color ColorCode()
        {
            Color tempColor = Color.black;
            switch ((int)value)
            {
                case 0: tempColor = Color.black; break;
                case 1: tempColor = new Color(144 / 255.0f, 97 / 255.0f, 25 / 255.0f); break;
                case 2: tempColor = Color.red; break;
                case 3: tempColor = Color.green; break;
                case 4: tempColor = Color.blue; break;
                case 5: tempColor = new Color(204 / 255.0f, 0, 204 / 255.0f); break;
                case 6: tempColor = new Color(216 / 255.0f, 169 / 255.0f, 21 / 255.0f); break;
                case 7: tempColor = new Color(204 / 255.0f, 204 / 255.0f, 204 / 255.0f); break;
            }
            return tempColor;
        }
    }
    [CreateAssetMenu(fileName = "ResistorParameters", menuName = "ElectonicComponents/Resistor", order = 1)]
    public class ResistorParameters: ScriptableObject
    {
        public Band band_1;
        public Band band_2;
        public MultiplierBand multiplier;
        public ToleranceBand tolerance;
    }
}


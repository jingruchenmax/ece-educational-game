using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace resistor
{
    [ExecuteInEditMode]
    public class ResistorIconRenderer : MonoBehaviour
    {
        public ResistorParameters resistorAsset;
        [Header("Resistor Bands")]
        [SerializeField]
        Image band_1;
        [SerializeField]
        Image band_2;
        [SerializeField]
        Image multiplier;
        [SerializeField]
        Image toleration;
        // Start is called before the first frame update


        void Awake()
        {
            band_1.color = resistorAsset.band_1.color;
            band_2.color = resistorAsset.band_2.color;
            multiplier.color = resistorAsset.multiplier.color;
            toleration.color = resistorAsset.tolerance.color;
        }


    }
}


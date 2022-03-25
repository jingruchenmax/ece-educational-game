using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace resistor
{
    [ExecuteInEditMode]
    public class ResistorRenderer: MonoBehaviour
    {
        public ResistorParameters resistorAsset;
        [Header("Resistor Bands")]
        [SerializeField]
        SpriteRenderer band_1;
        [SerializeField]
        SpriteRenderer band_2;
        [SerializeField]
        SpriteRenderer multiplier;
        [SerializeField]
        SpriteRenderer toleration;
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


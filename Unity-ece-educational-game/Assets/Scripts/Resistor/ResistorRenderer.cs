using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace resistor
{
    public class ResistorRenderer: MonoBehaviour
    {

        public ResistorParameters resistorAsset;
        [HideInInspector] public int resistorValue 
        { get 
            {
                if ((int)resistorAsset.multiplier.value != 8 || (int)resistorAsset.multiplier.value != 9)
                    return ((int)resistorAsset.band_1.value * 10 + (int)resistorAsset.band_2.value * 1) * (int)Mathf.Pow(10, (int)resistorAsset.multiplier.value);
                else return -1;
            } 
        }
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

        void OnValidate()
        {
            band_1.color = resistorAsset.band_1.color;
            band_2.color = resistorAsset.band_2.color;
            multiplier.color = resistorAsset.multiplier.color;
            toleration.color = resistorAsset.tolerance.color;
        }
        public void OnValueChange()
        {
            band_1.color = resistorAsset.band_1.color;
            band_2.color = resistorAsset.band_2.color;
            multiplier.color = resistorAsset.multiplier.color;
            toleration.color = resistorAsset.tolerance.color;
        }

        public void SetRendererActive(bool status)
        {
            switch (status)
            {
                case true:
                    this.GetComponent<SpriteRenderer>().enabled = true;
                    band_1.enabled = true;
                    band_2.enabled = true;
                    multiplier.enabled = true;
                    toleration.enabled = true;
                    break;

                case false:
                    this.GetComponent<SpriteRenderer>().enabled = false;
                    band_1.enabled = false;
                    band_2.enabled = false;
                    multiplier.enabled = false;
                    toleration.enabled = false;
                    break;
            }
        }

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LabelDamage: MonoBehaviour
{
    public TextMeshProUGUI txtDamage;

    void Start(){
        Destroy(gameObject, 1f);
    }
}
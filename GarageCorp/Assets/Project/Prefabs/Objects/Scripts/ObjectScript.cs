using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] AudioSource audio;

    void OnMouseDown()
    {
        anim.Play("Panelkaflex");
    }
}

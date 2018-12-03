using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEnableToggle : MonoBehaviour {

    [SerializeField]
    private GameObject gameobjectToToggle;
    [SerializeField]
    private float toggleTime;

    public void OnEnable() {
        Invoke("ToggleObject",toggleTime);
    }

    private void ToggleObject() {
        gameobjectToToggle.SetActive(!gameobjectToToggle.activeSelf);
        Invoke("ToggleObject", toggleTime);
    }
}

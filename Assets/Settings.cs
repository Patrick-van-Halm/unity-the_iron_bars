using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TMP_Text sensitivityValueText;
    public Toggle invertXToggle;
    public Toggle invertYToggle;

    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        sensitivitySlider.minValue = 0.1f;
        sensitivitySlider.maxValue = 4;
        sensitivitySlider.value = player.lookSpeed;
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        player.lookSpeed = PlayerPrefs.GetFloat("lookSpeed", 1);

        sensitivityValueText.text = player.lookSpeed.ToString("N1");

        invertXToggle.isOn = player.invertX;
        invertXToggle.onValueChanged.AddListener(InvertX);
        player.invertX = PlayerPrefs.GetInt("invertX", 0) == 1;

        invertYToggle.isOn = player.invertY;
        invertYToggle.onValueChanged.AddListener(InvertY);
        player.invertY = PlayerPrefs.GetInt("invertY", 0) == 1;
    }

    private void SetSensitivity(float value)
    {
        player.lookSpeed = value;
        PlayerPrefs.SetFloat("lookSpeed", value);
        sensitivityValueText.text = player.lookSpeed.ToString("N1");
    }

    private void InvertX(bool value)
    {
        player.invertX = value;
        PlayerPrefs.SetInt("invertX", value ? 1 : 0);
    }

    private void InvertY(bool value)
    {
        player.invertY = value;
        PlayerPrefs.SetInt("invertY", value ? 1 : 0);
    }
}

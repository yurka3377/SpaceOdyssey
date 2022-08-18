using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControll : MonoBehaviour
{
    [SerializeField] private Transform _moon;
    [SerializeField] private Transform _player;
    [SerializeField] private Slider _slider;
    private Vector3 _startPos;

    private void Awake()
    {
       _startPos = _player.position;
    }

    public void SetSliderValue()
    {
        float y = _player.position.y / (_startPos.y - _moon.position.y);
        _slider.value = y;
    }

    private void FixedUpdate()
    {
         SetSliderValue();
    }
}

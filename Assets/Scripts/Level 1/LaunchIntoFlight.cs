using UnityEngine;
using System.IO.Ports;

public enum ButtonState
{
    Idle = 0,
    Pressed
}

public class LaunchIntoFlight : MonoBehaviour
{
    private bool _flyStart = false;
    private SerialPort _port;
    private ButtonState _state;

    [SerializeField] private Animator _animator;


    private void Start()
    {
        //_port = new SerialPort("COM6", 9600);
        //_port.Open();

        //InvokeRepeating(nameof(Watching), 0.5f, 1f);
    }

    private void Update()
    {
        Watching();
    }

    private void Watching()
    {
        ButtonState btnState = CheckInput();
        if (btnState == ButtonState.Pressed)
        {
            _animator.SetTrigger("StartFly");
        }
    }

    private ButtonState CheckInput()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return ButtonState.Pressed;
        }
        return ButtonState.Idle;
#endif
        return GetArduinoButtonValue();

    }

    private ButtonState GetArduinoButtonValue()
    {
        string data = _port.ReadLine();
        print(data);
        ButtonState state = (ButtonState)int.Parse(data);
        return state;
    }
}

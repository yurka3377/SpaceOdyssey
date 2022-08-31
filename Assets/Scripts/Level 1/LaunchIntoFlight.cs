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
    private ArduinoControl ac;
    private GameObject al;

    [SerializeField] private Animator _animator;


    private void Start()
    {
        //_port = new SerialPort("COM6", 9600);
        //_port.Open();
        al = GameObject.Find("ArduinoLink"); 
        ac = al.GetComponent<ArduinoControl>();
        if (ac != null){
            ac.setLevel(1);
        }
        //InvokeRepeating(nameof(Watching), 0.5f, 1f);
    }

    private void Update()
    {
        Watching();
    }

    private void Watching()
    {
        if (al == null){
            al = GameObject.Find("ArduinoLink"); 
            ac = al.GetComponent<ArduinoControl>();
        }
        if (ac != null && ac.available() > 0){
            byte d = ac.getData();
            if (d == '1'){
                _animator.SetTrigger("StartFly");
            }
        }

        ButtonState btnState = CheckInput();
        if (btnState == ButtonState.Pressed)
        {
            _animator.SetTrigger("StartFly");
        }
    }

    private ButtonState CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            return ButtonState.Pressed;
        }
        return ButtonState.Idle;
    }

}

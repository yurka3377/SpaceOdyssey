using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ArduinoControl : MonoBehaviour
{
    UdpClient clientData;
    public int receiveBufferSize = 1024;
    Queue<byte> receivedData;

    public string arduinoIP = "127.0.0.1";
    public int arduinoPortControl = 8050;  
    public int arduinoPortData = 8051;  

    IPEndPoint ipEndPointData;
    private object obj = null;
    private System.AsyncCallback AC;

    public void Start()
    {
        receivedData = new Queue<byte>(512);
        InitializeUDPListener();
        setLevel(1);
    }

    public void InitializeUDPListener()
    {
        ipEndPointData = new IPEndPoint(IPAddress.Any, arduinoPortData);
        clientData = new UdpClient();
        clientData.Client.ReceiveBufferSize = receiveBufferSize;
        clientData.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, optionValue: true);
        clientData.ExclusiveAddressUse = false;
        clientData.EnableBroadcast = true;
        clientData.Client.Bind(ipEndPointData);
        clientData.DontFragment = true;
        AC = new System.AsyncCallback(ReceivedUDPPacket);
        clientData.BeginReceive(AC, obj);
        Debug.Log("UDP - Start Receiving..");
    }

    void ReceivedUDPPacket(System.IAsyncResult result)
    {
        byte[] receivedBytes;
        receivedBytes = clientData.EndReceive(result, ref ipEndPointData);
        for (int i=0; i < receivedBytes.Length; i++){
            receivedData.Enqueue(receivedBytes[i]);
            //Debug.Log("add byte to quieue: " + receivedBytes[i]);
        }
        
        // ParsePacket();
        clientData.BeginReceive(AC, obj);

        //stopwatch.Stop();
        //Debug.Log(stopwatch.ElapsedTicks);
        //stopwatch.Reset();
    } 

    public void flush(){
        receivedData.Clear();
    }

    public void setLevel(int level){
        byte[] data = new byte[10];
        UdpClient client;
        data[0] = 0x50;
        data[1] = (byte) level;
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(arduinoIP), arduinoPortControl);
        client = new UdpClient();
        client.Send(data, data.Length, remoteEndPoint);
        //var receivedResults = client.Receive(ref remoteEndPoint);
        //Debug.Log(">>>"+receivedResults[0] + " / "+receivedResults[1]);
        client.Close();
    }

    public void sendData(byte[] data)
    {
        UdpClient client;
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(arduinoIP), arduinoPortData);
        client = new UdpClient();
        client.Send(data, data.Length, remoteEndPoint);
        client.Close();
    }

    public int available(){
        return receivedData.Count;
    }

    public byte getData(){
        return receivedData.Dequeue();
    }

    void OnDestroy()
    {
        if (clientData != null)
        {
            clientData.Close();
        }
    }
}


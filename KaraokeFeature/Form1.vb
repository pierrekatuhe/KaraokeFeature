Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Public Class Form1
    Dim m_listener As TcpListener

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
        Form2.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        WriteData("Coba yang lain", "127.0.0.1")
    End Sub

    Public Sub WriteData(ByVal data As String, ByRef IP As String)
        Console.WriteLine("Sending message """ & data & """ to " & IP)
        Dim client As TcpClient = New TcpClient()
        client.Connect(New IPEndPoint(IPAddress.Parse(IP), 8080))
        Dim stream As NetworkStream = client.GetStream()
        Dim sendBytes As Byte() = Encoding.ASCII.GetBytes(data)
        stream.Write(sendBytes, 0, sendBytes.Length)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim localAddr As IPAddress = IPAddress.Parse("127.0.0.1")
        m_listener = New TcpListener(localAddr, 8080)
        m_listener.Start()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim bytes(300000) As Byte
        Dim data As String = ""

        Console.Write("Waiting for a connection... ")

        Dim client As TcpClient = m_listener.AcceptTcpClient()
        Dim requesterIP As String = client.Client.RemoteEndPoint.ToString().Split(New Char() {":"})(0)

        Console.WriteLine("Connected!")

        data = ""
        Dim stream As NetworkStream = client.GetStream()

        Dim BytesRead As Integer = 0
        BytesRead = stream.Read(bytes, 0, bytes.Length)
        data = Encoding.ASCII.GetString(bytes, 0, BytesRead)
        TextBox1.Text = data



    End Sub
End Class

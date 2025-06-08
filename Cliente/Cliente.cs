using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ClienteSimple
{
    private static Socket _serverSocket;

    public static void Main()
    {
        Console.Title = "Cliente";
        Console.WriteLine("¡Bienvenido, Cliente!");
        Console.Write("Por favor, ingrese la dirección IP del servidor: ");
        string serverIp = Console.ReadLine();

        int port = 11000; // El puerto debe coincidir con el del servidor

        _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            IPAddress ipAddress = IPAddress.Parse(serverIp);
            _serverSocket.Connect(new IPEndPoint(ipAddress, port));
            Console.WriteLine("Conectado con éxito.");

            // Hilo para recibir mensajes del servidor (aunque en este caso el servidor solo recibe)
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.IsBackground = true;
            receiveThread.Start();

            SendMessageLoop(); // Inicia el bucle para enviar mensajes
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: La dirección IP ingresada no es válida.");
        }
        catch (SocketException sex)
        {
            Console.WriteLine($"Error de conexión: {sex.Message}");
            if (sex.SocketErrorCode == SocketError.ConnectionRefused)
            {
                Console.WriteLine("Asegúrese de que el servidor esté en ejecución y que la IP y el puerto sean correctos.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
        }
        finally
        {
            _serverSocket?.Close();
            Environment.Exit(0);
        }
    }

    private static void SendMessageLoop()
    {
        Console.WriteLine("\nIngrese el texto a enviar al servidor.");
        // ¡Aquí está la nueva línea!
        Console.WriteLine("Para finalizar la conversación y salir, escriba 'adios'.");
        try
        {
            while (true)
            {
                Console.Write("Enviar: ");
                string message = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(message)) continue;

                byte[] buffer = Encoding.ASCII.GetBytes(message);
                _serverSocket.Send(buffer);

                if (message.ToLower() == "adios")
                {
                    Thread.Sleep(500); // Dar un momento para que el mensaje se envíe
                    break;
                }
            }
        }
        catch (SocketException)
        {
            Console.WriteLine("\nDesconectado del servidor.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError al enviar mensaje: {ex.Message}");
        }
        finally
        {
            _serverSocket?.Close();
            Environment.Exit(0);
        }
    }

    private static void ReceiveMessages()
    {
        try
        {
            while (true)
            {
                byte[] buffer = new byte[2048];
                int bytesRec = _serverSocket.Receive(buffer);
                if (bytesRec == 0)
                {
                    Console.WriteLine("\nEl servidor ha cerrado la conexión.");
                    break;
                }
                // Si el servidor enviara algo, se mostraría aquí.
            }
        }
        catch (SocketException)
        {
            Console.WriteLine("\nConexión perdida con el servidor.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError en la recepción: {ex.Message}");
        }
        finally
        {
            _serverSocket?.Close();
            Environment.Exit(0);
        }
    }
}

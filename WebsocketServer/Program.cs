using System;
using System.Net;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading.Tasks;

namespace Server
{
    public class MouseTrackingSocket : WebSocketBehavior
    {
        protected override Task OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Llego un mensaje ({0})", e.Data.ToString());
            return null;
        }

        protected override Task OnClose(CloseEventArgs e)
        {
            Console.WriteLine("Se cerro una conexion");
            return null;
        }

    }

    public class Program
    {

        static WebSocketServer wssv;
        static Thread broadcast;
        static Thread info;
        const int MessagesPerSecond = 30;
        const int Port = 9876;
        const string MouseTrackingURL = "/mousetracking";


        static void Broadcast()
        {
            while (true)
            {
                int mouseX, mouseY;
                MouseTracking.GetCursorPosition(out mouseX, out mouseY);
                wssv.WebSocketServices[MouseTrackingURL].Sessions.Broadcast(String.Format("{0} {1}", mouseX, mouseY));
                Thread.Sleep(1000 / MessagesPerSecond);
            }
        }

        static void Info()
        {
            while (true)
            {
                Console.WriteLine("Actualmente hay {0} IDs activas", wssv.WebSocketServices[MouseTrackingURL].Sessions.Count);
                Thread.Sleep(10000);
            }
        }


        public static void Main(string[] args)
        {
            wssv = new WebSocketServer(null, Port);            

            // Crea un servicio que sera accedido por localhost:PUERTO/mousetracking
            wssv.AddWebSocketService<MouseTrackingSocket>(MouseTrackingURL);                     

            wssv.Start();

            // Crea un nuevo hilo, el cual repetidamente obtiene la posicion del mouse,
            // y la manda a todos los subscriptores de este socket.
            broadcast = new Thread(Broadcast);

            // Este otro hilo es para que el servidor muestre informacion periodicamente (por consola en este caso)
            info = new Thread(Info);

            broadcast.Start();
            info.Start();

            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}

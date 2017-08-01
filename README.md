# Mouse Tracking & WebSocket

Servicio hecho en C# que le envia a sus clientes conectados por Websocket la posicion del mouse.

Tambien tiene un cliente creado en Node que recibe esos datos, y otro en Python 3.

Python necesita el siguiente modulo:

```bash
pip install websocket-client
```

El cliente en Node necesita hacer:

```bash
npm install
```

Y para el servidor en C# se puede hacer con Visual Studio haciendo click en `ejecutar` el cual automaticamente invoca a NuGet para descargar las dependencias faltantes (y si no, hacerlo manualmente con NuGet).

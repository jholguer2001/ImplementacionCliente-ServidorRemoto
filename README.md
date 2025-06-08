Implementación Cliente-Servidor (Remoto)

# Comunicación Cliente-Servidor Simple (Remota) en C#

Este proyecto demuestra una comunicación básica de cliente-servidor utilizando sockets en C#. Está diseñado para ser ejecutado en **dos máquinas diferentes**, donde el cliente se conecta al servidor ingresando la dirección IP de la máquina del servidor. El servidor solo recibe y muestra los mensajes del cliente.

## Estructura del Proyecto

El proyecto consta de dos aplicaciones de consola separadas:

1.  **`ServidorSimple.cs`**: La aplicación del servidor que escucha las conexiones.
2.  **`ClienteSimple.cs`**: La aplicación del cliente que se conecta al servidor y envía mensajes.

## Requisitos

* .NET SDK (preferiblemente .NET 6 o superior)
* Un entorno de desarrollo como Visual Studio, Visual Studio Code o cualquier editor de texto para compilar y ejecutar C#.
* **Dos computadoras** conectadas a la misma red local (LAN) o con acceso a internet y una IP pública/ruteable si deseas probarlo fuera de una LAN (esto último requiere configuración de router y firewall).

## Cómo Ejecutar

Sigue estos pasos en cada una de las dos máquinas.

### 1. En la Máquina del Servidor (PC A)

1.  Abre una terminal (CMD, PowerShell, Bash, etc.).
2.  Navega hasta el directorio donde se encuentra el archivo `ServidorSimple.cs`.
3.  **Importante**: Antes de ejecutar, necesitas conocer la **dirección IP local** de esta máquina. Puedes obtenerla abriendo el Símbolo del Sistema (CMD) y escribiendo `ipconfig` (en Windows) o `ifconfig` / `ip addr` (en Linux/macOS). Busca la "Dirección IPv4" (ej. `192.168.1.100`). **Anótala**, la necesitarás para el cliente.
4.  Asegúrate de que tu **firewall** (ej. Firewall de Windows Defender) esté configurado para permitir conexiones entrantes en el puerto `11000`. Si no sabes cómo, puedes desactivar el firewall temporalmente para las pruebas (¡solo para pruebas en una red segura!).
5.  Compila y ejecuta el servidor con el siguiente comando:
    ```bash
    dotnet run ServidorSimple.cs
    ```
6.  Verás la siguiente salida, indicando que el servidor está listo:
    ```
    Servidor iniciado.
    Esperando conexión...
    ```

### 2. En la Máquina del Cliente (PC B)

1.  Abre una terminal (CMD, PowerShell, Bash, etc.).
2.  Navega hasta el directorio donde se encuentra el archivo `ClienteSimple.cs`.
3.  Compila y ejecuta el cliente:
    ```bash
    dotnet run ClienteSimple.cs
    ```
4.  Se te pedirá la dirección IP del servidor. **Ingresa la IP que anotaste de la Máquina A** (ej. `192.168.1.100`) y presiona Enter.
    ```
    ¡Bienvenido, Cliente!
    Por favor, ingrese la dirección IP del servidor: 192.168.1.100
    ```

### 3. Verificación de la Conexión y Envío de Mensajes

* **En la Máquina del Cliente (PC B):**
    * Si la conexión es exitosa, verás:
        ```
        Conectado con éxito.
        Ingrese el texto a enviar al servidor.
        Para finalizar la conversación y salir, escriba 'adios'.
        Enviar: _
        ```
    * Ahora puedes escribir mensajes en el prompt `Enviar:` y presionar Enter.

* **En la Máquina del Servidor (PC A):**
    * Cuando el cliente se conecte, verás:
        ```
        Conectado con éxito.
        Servidor listo para recibir mensajes del cliente.
        ```
    * A medida que el cliente envíe mensajes, el servidor los mostrará:
        ```
        Cliente dice: ¡Hola desde el cliente!
        Cliente dice: Esto es una prueba.
        ```

### 4. Finalizar la Conversación

* Desde el **Cliente (PC B)**, escribe `adios` y presiona Enter en el prompt `Enviar:`. El cliente se cerrará limpiamente.
* En el **Servidor (PC A)**, verás el mensaje:
    ```
    El cliente se desconectó.
    Conexión con el cliente finalizada.
    ```
    El servidor también se cerrará automáticamente al desconectarse el cliente.

## Solución de Problemas Comunes

* **"No se pudo conectar al servidor." / "Conexión rechazada."**:
    * Asegúrate de que el servidor esté **ejecutándose** en la Máquina A.
    * Verifica que la **dirección IP** ingresada en el cliente sea exactamente la IP local de la Máquina A.
    * Comprueba que el **firewall** en la Máquina A no esté bloqueando el puerto `11000`.
    * Asegúrate de que ambas máquinas estén en la **misma red** y puedan comunicarse.
* **Mensajes de "Se perdió la conexión..." inesperados**: Si ves estos mensajes sin haber escrito 'adios', significa que hubo una interrupción inesperada en la red o que la aplicación del servidor se cerró abruptamente.

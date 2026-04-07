# TechnicaltestJCP2CALL

Instrucciones para compilar y ejecutar el proyecto.

Estructura de módulos y ejecución
- El repositorio contiene tres módulos que corresponden a las secciones de la prueba.
- Para ejecutar el `Módulo 1`:
  1. Abrir la solución/proyecto en Visual Studio.
  2. En el `Solution Explorer` localizar el proyecto `TechnicaltestJCP2CALL` (o `module1`).
  3. Hacer clic derecho sobre ese proyecto y seleccionar `Establecer como proyecto de inicio`.
  4. Ejecutar la aplicación (F5 o Ctrl+F5).
- Para ejecutar el `Módulo 2`:
  1. Localizar el proyecto correspondiente al módulo 2 en el `Solution Explorer`.
  2. Hacer clic derecho sobre ese proyecto y seleccionar `Establecer como proyecto de inicio`.
  3. Ejecutar la aplicación.
- `Módulo 3`:
  - Este módulo está contenido en una carpeta/solución separada. Dentro encontrarás un archivo `.cs` con el código ajustado según lo indicado en la prueba. Para ejecutar o revisar el código:
    1. Abrir la carpeta/solución del módulo 3 en Visual Studio.
    2. Si es una librería, abrir el proyecto que contiene el `Main` o el ejemplo ejecutable y establecerlo como proyecto de inicio.
    3. Ejecutar según corresponda.

Arquitectura del Módulo 1
- Para el `Módulo 1` se implementó una arquitectura por capas dentro del mismo proyecto: las responsabilidades están separadas en carpetas (por ejemplo `Application`, `Presentation`, `Infrastructure`, `Domain`) en vez de distribuir cada capa en proyectos separados. Esta decisión reduce la complejidad para el alcance de la prueba y facilita el desarrollo sin perder organización.

Módulo 2 - Base de datos (SQLite)
- El módulo 2 está implementado con SQLite como motor de base de datos. La base de datos se genera automáticamente al ejecutar la aplicación por primera vez mediante `EnsureCreated()` (ver `module2\Seed\DatabaseInitializer.cs` y `module2\Data\AppDbContext.cs`) y no requiere configuración manual.
- El proyecto inicializa SQLite mediante `SQLitePCL.Batteries.Init()` en el `Program.cs` del módulo.
- El archivo físico de la base de datos SQLite se crea en la carpeta del módulo 2 con el nombre `store.db`.
- Ruta relativa desde la raíz del repositorio: `module2\bin\Debug\net10.0`.
- Si desea regenerar la base de datos, cierre la aplicación y elimine `module2\bin\Debug\net10.0\store.db` antes de ejecutar de nuevo; el archivo se volverá a crear automáticamente.

Contacto
- Repo: https://github.com/EdwinGrisalesCalle/TechnicaltestJCP2CALL

Módulo 3 - Solución (resumen)
- Problema 1: la cadena de conexión se obtiene primero desde variables de entorno por seguridad y, si no existe, se usa el archivo de configuración para facilitar el desarrollo local. Esto evita exponer credenciales sensibles y permite cambios sin recompilar la aplicación.
- Problema 2: se usa `using` para asegurar la liberación de recursos y evitar conexiones abiertas.
- Problema 3: se reemplazó la concatenación por parámetros en las consultas para prevenir ataques de inyección SQL (los valores se tratan como datos literales y no como código ejecutable).
- Problema 4: se valida la entrada usando `TryParse()` en lugar de `Parse()` para evitar excepciones por datos inválidos. Si el formato es incorrecto, se lanza una excepción controlada con un mensaje claro para el usuario.
- Problema 5: la cadena de conexión se centraliza en un único campo `readonly` (`_connectionString`) accesible por todos los métodos, eliminando duplicación y facilitando el mantenimiento.
- Problema 6: se reemplazó el `catch (Exception) { }` vacío por un manejo específico de excepciones.

Notas adicionales:
- Se agregaron validaciones en los métodos para verificar que los parámetros sean válidos antes de operaciones de base de datos.
- Se comprueba `DBNull.Value` antes de convertir a `decimal` cuando aplica; es importante que la aplicación falle controladamente cuando un cliente no tiene pedidos.
- El método `EliminarPedido` realiza la eliminación a nivel de servicio correctamente. La confirmación al usuario debe implementarse en la capa de presentación (UX). Como alternativa para trazabilidad, se puede optar por una eliminación lógica en vez de física para auditorías.



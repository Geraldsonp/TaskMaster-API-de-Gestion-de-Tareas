
# TaskMaster: API de Gestión de Tareas con .NET

------------

## Descripción del Proyecto:

Este proyecto es una API web .NET 6 para la gestión de tareas que ya ha sido construida. La API ha sido desarrollada utilizando C# y .NET, y utiliza SQLLite como base de datos. Además, la API utiliza AutoMapper y EF Core para proporcionar un mapeo y persistencia eficientes de datos. Los tokens JWT se utilizan para la autenticación y autorización.

La API permite a los usuarios crear, leer, actualizar y eliminar tareas. Cada tarea tiene un título, descripción, estado (por ejemplo, en progreso, completada) y una fecha de vencimiento. Los usuarios pueden ver todas sus tareas, así como filtrar tareas según su estado o fecha de vencimiento.

La API también tiene autenticación y autorización de usuario mediante tokens JWT. Los usuarios pueden registrarse para obtener una cuenta e iniciar sesión en el sistema utilizando su correo electrónico y contraseña. Una vez autenticados, los usuarios reciben un token JWT que se utiliza para las llamadas posteriores a la API para verificar su identidad y autorización para realizar ciertas acciones.

La API está diseñada utilizando una arquitectura en capas, con capas separadas para la presentación, la lógica de negocio y el acceso a datos. La capa de presentación maneja las solicitudes y respuestas HTTP, la capa de lógica de negocio contiene las reglas y operaciones comerciales, y la capa de acceso a datos es responsable de recuperar y persistir datos de la base de datos SQLLite.

En general, este proyecto proporciona a los usuarios una forma simple y eficiente de gestionar sus tareas, al mismo tiempo que proporciona una API segura y escalable utilizando las últimas tecnologías de .NET 6.

al contruir este projecto pude ampliar mis conocimientos en cuanto a la organizacion del projecto, diferentes tipos de relaciones en la base de datos, el mappeo automatico de objetos Resfull API y la injeccion de dependencias. 

## Tecnologias Usadas
- C#
- .Net
- SQLLite
- AutoMapper
- EF Core

## Como instalar y correr el projecto

Para correr el projecto simplemente clona el repo y connecta la base de datos para eso tendras que cambiar la cadena de conneccion en el appsettings.json.

Ya luego solo ejecutas el projecto con dotnet run. 

Todo esto luego de tener instalado .NET SDK 6.1

# Upcoming
- Estare Añadiendo UnitTesting

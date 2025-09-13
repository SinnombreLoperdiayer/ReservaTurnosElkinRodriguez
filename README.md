# ReservaTurnosElkinRodriguez

# Sistema de Agendamiento de Turnos

## 📌 Descripción
Sistema de agendamiento de turnos que permite a los clientes de diferentes comercios reservar espacios de atención en servicios específicos.  
Esta API proporciona la funcionalidad de **generación automática de turnos** basada en los horarios y duración de cada servicio.

---

## ⚙️ Tecnologías Utilizadas
- **.NET 8**: Framework principal  
- **ASP.NET Core Web API**: Creación de la API REST  
- **Entity Framework Core**: ORM para acceso a datos  
- **SQL Server**: Base de datos  
- **Swagger / OpenAPI**: Documentación de la API  

---

## 🚀 Instalación y Configuración

### 🔹 Prerrequisitos
- .NET 8 SDK  
- SQL Server 2019 o superior  
- Visual Studio 2022 o Visual Studio Code  

### 🔹 Pasos de Instalación
1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/SinnombreLoperdiayer/ReservaTurnosElkinRodriguez.git
   cd ReservaTurnosElkinRodriguez

2. **Configurar la base de datos**
  - Ejecutar el script Script_ReservaTurnos_ElkinRodriguez.sql en SQL Server.
  - Modificar la cadena de conexión en appsettings.json si es necesario.

3. **Restaurar paquetes NuGet**
   ```bash
   dotnet restore

4. **Ejecutar Aplicación**
    ```bash
   cd ReservaTurnosElkinRodriguez
    dotnet run
5. **Acceder a Swagger**
  - Abrir en el navegador
    https://localhost:{puerto_configurado}

# 🗄️ Configuración de Base de Datos

## 🔹 Cadena de Conexión
Modificar en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "ReservaTurnosConnection": "Server=TU_SERVIDOR;Database=ReservaTurnos;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
  }
}

# 📡 API Endpoints

## 📋 Resumen
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| **POST** | `/api/turnos/generar` | Genera turnos automáticamente para un servicio en un rango de fechas |
| **GET** | `/api/turnos/disponibles/{idServicio}?fechaInicio=dd/MM/yyyy&fechaFin=dd/MM/yyyy` | Consulta turnos disponibles para un servicio específico |

---

## 1️⃣ Generar Turnos

**POST** `/api/turnos/generar`  
Genera turnos automáticamente para un servicio en un rango de fechas.

### 🔹 Request Body
```json
{
  "fechaInicio": "15/12/2025",
  "fechaFin": "15/12/2025",
  "idServicio": 1
}

# 📡 API Endpoints - Turnos

---

## 1️⃣ Generar Turnos

### 🔹 Endpoint
`POST /api/turnos/generar`  
Genera turnos automáticamente para un servicio en un rango de fechas.

### 🔹 Response
```json
{
  "success": true,
  "message": "Se generaron 16 turnos.",
  "data": [
    {
      "idTurno": 201,
      "idServicio": 1,
      "nombreServicio": "Consulta General",
      "nombreComercio": "Consultorio Médico",
      "fechaTurno": "15/12/2025",
      "horaInicio": "08:00",
      "horaFin": "08:30",
      "estado": "Disponible"
    },
    {
      "idTurno": 202,
      "idServicio": 1,
      "nombreServicio": "Consulta General",
      "nombreComercio": "Consultorio Médico",
      "fechaTurno": "15/12/2025",
      "horaInicio": "08:30",
      "horaFin": "09:00",
      "estado": "Disponible"
    },
    {
      "idTurno": 203,
      "idServicio": 1,
      "nombreServicio": "Consulta General",
      "nombreComercio": "Consultorio Médico",
      "fechaTurno": "15/12/2025",
      "horaInicio": "09:00",
      "horaFin": "09:30",
      "estado": "Disponible"
    },
    {
      "idTurno": 204,
      "idServicio": 1,
      "nombreServicio": "Consulta General",
      "nombreComercio": "Consultorio Médico",
      "fechaTurno": "15/12/2025",
      "horaInicio": "09:30",
      "horaFin": "10:00",
      "estado": "Disponible"
    }
  ],
  "errors": []
}


## 2️⃣ Consultar Turnos Disponibles

### 🔹 Endpoint
`GET /api/turnos/disponibles/{idServicio}?fechaInicio=dd/MM/yyyy&fechaFin=dd/MM/yyyy`  
Consulta turnos disponibles para un servicio específico.

---

### 🔹 Parámetros

| Parámetro   | Tipo   | Ubicación | Requerido | Descripción                        |
|-------------|--------|-----------|-----------|------------------------------------|
| idServicio  | int    | path      | ✅ Sí     | ID del servicio                    |
| fechaInicio | string | query     | ❌ No     | Fecha inicio en formato dd/MM/yyyy |
| fechaFin    | string | query     | ❌ No     | Fecha fin en formato dd/MM/yyyy    |

---

### 🔹 Response
```json
{
  "success": true,
  "message": "Se consultaron 4 turnos disponibles.",
  "data": [
    {
      "idTurno": 197,
      "idServicio": 2,
      "nombreServicio": "Toma de Examen",
      "nombreComercio": "Consultorio Médico",
      "fechaTurno": "15/10/2025",
      "horaInicio": "08:00",
      "horaFin": "08:30",
      "estado": "Disponible"
    },
    {
      "idTurno": 198,
      "idServicio": 2,
      "nombreServicio": "Toma de Examen",
      "nombreComercio": "Consultorio Médico",
      "fechaTurno": "15/10/2025",
      "horaInicio": "08:30",
      "horaFin": "09:00",
      "estado": "Disponible"
    },
    {
      "idTurno": 199,
      "idServicio": 2,
      "nombreServicio": "Toma de Examen",
      "nombreComercio": "Consultorio Médico",
      "fechaTurno": "15/10/2025",
      "horaInicio": "09:00",
      "horaFin": "09:30",
      "estado": "Disponible"
    },
    {
      "idTurno": 200,
      "idServicio": 2,
      "nombreServicio": "Toma de Examen",
      "nombreComercio": "Consultorio Médico",
      "fechaTurno": "15/10/2025",
      "horaInicio": "09:30",
      "horaFin": "10:00",
      "estado": "Disponible"
    }
  ],
  "errors": []
}


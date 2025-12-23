# Entrevista Fullstack (.NET 8 + React TS + PostgreSQL)

Proyecto para entrevista técnica con:
- **Backend:** API REST en **.NET 8** (arquitectura en capas: API / BLL / DAL / CORE)
- **Frontend:** **React + TypeScript** (Vite)
- **Base de datos:** PostgreSQL
- **Scripts SQL:** en la carpeta `scripts/`

## Estructura

- `backend/` → solución .NET (API + capas)
- `frontend/` → React + TypeScript (Vite)
- `scripts/` → scripts SQL para crear esquema, tabla, procedimientos y permisos

## Requisitos

- .NET SDK 8
- Node.js + npm
- PostgreSQL (y pgAdmin opcional)

---

## Base de datos

1. Ejecuta los scripts en `scripts/` en el orden indicado por sus nombres (o según tu orden).
2. Verifica que el usuario de conexión exista y tenga permisos (GRANT).

> El backend usa `ConnectionStrings:Postgres` en `appsettings.json`.

---

## Backend (.NET 8)

### Visual Studio
1. Abrir la solución en `backend/`
2. Ejecutar el proyecto API

Swagger:
- `https://localhost:7146/swagger` (puerto puede variar)

### CLI (opcional)
Dentro de `backend/`:
```bash
dotnet restore
dotnet run --project Entrevista

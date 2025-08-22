# Templanza Hybrid

**Templanza Hybrid** es una aplicación **.NET MAUI + Blazor Hybrid** que presenta un mini-sistema para una marca de blends: catálogo de productos, administración de usuarios y autenticación básica, con UI basada en **Bootstrap 5** y una paleta visual propia.

## Objetivo del proyecto
- Combinar host nativo **MAUI** con UI web declarativa (**Blazor**) en una sola app.
- Explorar un catálogo de productos con búsqueda y panel de detalle.
- Administrar usuarios y roles (Admin/User).
- Autenticar y gestionar el perfil del usuario logueado.
- Mantener una identidad visual consistente (paleta, fondos, componentes).

## Funcionalidades
- **Productos**: listado, filtro por texto, detalle lateral y alta/edición/eliminación (solo Admin).
- **Usuarios (Admin)**: listado con búsqueda, detalle y CRUD.
- **Mi perfil**: edición de datos del usuario autenticado.
- **Autenticación**: login simple en memoria y control de roles.
- **Selección de imágenes**: datalist + miniaturas clickeables leyendo archivos reales desde `wwwroot/images`.

## Arquitectura y tecnologías
- **Host**: .NET **MAUI** (aplicación nativa multiplataforma).
- **UI**: **Blazor WebView** (componentes Razor embebidos).
- **Servicios** (capa en memoria):
  - `UsuariosService`, `BlendsService`: gestión de entidades mock/in-memory.
  - `AuthService` (`IAuthService`): sesión actual, autenticación y roles.
  - `StaticAssetsService` (`IStaticAssetsService`): índice de imágenes desde `wwwroot` (por `index.json` y/o enumeración de carpetas).
  - `ConfirmService` (`IConfirmService`): confirmaciones/toasts vía JS con fallback nativo (registrado como Scoped).
- **UI/Componentes**:
  - `MainLayout.razor`: navbar con vínculos condicionales por rol y `@Body`.
  - Formularios: `BlendForm.razor`, `UsuarioForm.razor` (validaciones, datalist, miniaturas).
  - Ruteo: `Routes.razor` con `Router`, `RouteView` y `NotFound`.
- **Estilos**: `Bootstrap 5` + `Bootstrap Icons` + `app.css` (tokens de color, superficies, overlay de fondo).

## Navegación y roles
- Menú principal: Home, Productos (todos).
- Ítems condicionales:
  - **Mi perfil**: visible al estar autenticado.
  - **Usuarios (Admin)**: visible solo para rol **Admin**.
- Acciones sensibles (Crear/Editar/Eliminar) se muestran según rol.

## Gestión de imágenes
- Carpeta `wwwroot/images` con subcarpetas `productos/` y `usuarios/`.
- Opcional `wwwroot/index.json` para indexar recursos.
- Formularios con datalist y miniaturas para seleccionar la imagen/avatares.

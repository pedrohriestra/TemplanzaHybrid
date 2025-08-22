Templanza Hybrid 🌿

App .NET MAUI + Blazor Hybrid para un mini sistema de blends y usuarios. UI con Bootstrap 5 + Icons, paleta propia y fondo .webp. Pensada para mostrar en Windows (BlazorWebView).

¿Qué incluye? ✨

🏠 Home

🛍️ Productos: listado + búsqueda, detalle al costado, CRUD Admin, selección de imagen desde wwwroot/images/productos (datalist + miniaturas).

👥 Usuarios (Admin): listado + búsqueda, detalle, CRUD, selección de avatar desde wwwroot/images/usuarios.

🔐 Login simple (en memoria), roles Admin/User, Mi perfil, botón Salir en el navbar.

🎨 Paleta de colores propia + overlay para legibilidad; toasts/confirm JS con fallback nativo.

Cómo correrlo ▶️

Requisitos: .NET 9 + workload MAUI.

Abrí en VS 2022 → perfil Windows Machine → Run.

Android/iOS opcional (el TP se presenta en Windows).

Tech stack 🧱

MAUI host + Blazor WebView UI.

Servicios: Usuarios, Blends, Auth (sesión/roles), Confirm (Scoped, usa JS con fallback), StaticAssets (lista imágenes desde wwwroot o index.json).

Estructura mínima 📁

Components/ (Layout, Forms, Routes)

Pages/ (Productos, Usuarios, Login, Perfil)

Services/ (Auth, Blends, Usuarios, Confirm, StaticAssets)

wwwroot/ (css/app.css, js/templanza.js, images/, index.html, opcional index.json)

Tips 🛠️

Si ves “Loading…”: verificá que el Layout tenga @Body y que en index.html el último script sea _framework/blazor.webview.js.

Si “Salir” daba 404: ahora ejecuta logout directo (no navega a /logout).

Error “Cannot invoke JavaScript outside of a WebView context.”: asegurá ConfirmService registrado como Scoped.

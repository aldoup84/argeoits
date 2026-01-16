# ReAQ (Android)
ARGEOITS es una herramienta de aprendizaje desarrollada en **Unity 2019.4** que implementa la tecnología de realidad aumentada basada en la detección de marcadores, centrada en el tema de cuerpos y planos geométricos (prismas regulares). Esta herramienta de aprendizaje también le permite al usuario conocer 
Aprender más acerca de los cálculos de área de la base, de volumen de prismas regulares, suma de volúmenes a través de colisiones de marcadores y sobre las secciones de cortes (paralelas, perpendiculares o a la generatriz) aplicados a un cilindro o un cono.
Para trabajar con esta versión no se requiere conexión a internet, pues SQLite se ejecuta dentro del mismo dispositivo móvil. ARGeoITS está diseñada y probada en dispositivos Android 13 o superior. 

- **Plataforma:** Android (APK)
- **Versión mínima de Android:** Android 13 o superior
- **Licencia (código original):** GPL-3.0-or-later (see `LICENSE`)
- **Contenido/Dependencias de terceros:** Unity Asset Store assets y SQLite— ver `THIRD_PARTY_NOTICES.md`

---

## Requisitos
- Unity **2019.4 LTS**
- Android Build Support (SDK/NDK) instalado desde Unity Hub
- **Vuforia Engine 8.5.20**
- **SQLite 3** *(se importa vía .unitypackage y luego se ejecuta el resolver)*

## Vuforia (Package Manager)
Este proyecto usa **Vuforia Engine 8.5**.  Al importarse vía Unity Package Manager, no es neccesario versionarlo ni subirlo a Github.


Para restaurar dependencias:
1. Abrir el proyecto en Unity 2019.4
2. Unity restaurará paquetes usando `Packages/manifest.json` y `Packages/packages-lock.json`.
3. Si Vuforia no se instala automáticamente: **Window → Package Manager** e instalar la versión indicada.

## SQLite 3 
Ya va integrado en el código fuente; se descomprimen los archivos DLL (sqlite3.def, sqlite3.dll, sqlite-android-3210000.aar) en la carpeta Assets/Plugins.
En Assets/Plugins/Android se coloca el archivo libsqlite3.so.
En la carpeta Assets/SQLiteOCX se colocan los archivos sqldiff, sqlite3, y sqlite3_analyzer
Todos estos archivos ya están proporcionados en este repositorio.
En caso de algún error, se ejecuta el **resolver** (External Dependency Manager) para descargar/ajustar dependencias.


## Guía de Instalación (APK)
El APK no se incluye en el repositorio por tamaño. Descárgalo desde Zenodo o Google Drive.
Una vez descargado el archivo APK, procede con los pasos listados en `docs/install_apk.png` (es el mismo proceso que REAQ).

### Requerimientos del Sistema
- Dispositivo Android con **Android 13+**
- Suficiente espacio de almacenamiento libre para instalar el APK
- Conexión a Internet (recomendada) si se utilizan funciones que dependen de Firebase (v2)

### Descargas
- **APK (Zenodo, DOI):** https://doi.org/10.5281/zenodo.18199812
- **APK (Google Drive):** *TODO: agregar link de Drive*
- **Marcadores:** https://drive.google.com/file/d/10CqTO7aFy0gwEhpjM98GYIvG3EDNHdtU/view?usp=drive_link


### Instalación (desde APK)
Una vez descargado el APK, sigue los pasos que se muestran en esta imagen guía:
![Installation steps](docs/install_apk.png)

> **Nota:** Si Android bloquea la instalación, habilita **Instalar aplicaciones desconocidas** para la aplicación que usaste para descargar el APK (WhatsApp/Drive/Navegador) y luego vuelve a intentar la instalación.

---

## Manual de Usuario (Inicio Rápido)

### Funcionalidades principales
1. Pantalla de Inicio de sesión / registro de un nuevo usuario. Se requiere de un correo y una contraseña
2. Pantalla principal. En esta se despliegan las opciones disponibles, así como un apartado de configuración local de la aplicación
3. Antes de abrir una escena se muestran instrucciones/objetivo
4. Configuración: activar/desactivar sonido, descargar marcadores y limpiar preferencias del usuario

### Video Tutorial
Se han creado videos, mostrando cómo utilizar algunas escenas que integran esta aplicación. Este video está disponible en la carpeta docs/videos de este repositorio.
- **YouTube video:** *https://youtu.be/IBKfJ4OstJ4*

---

## Arquitectura del proyecto (Unity)
Este proyecto es una **aplicación móvil en Unity**. La estructura se organiza en `Assets/` por responsabilidad:

### Carpeta Principal (Unity `Assets/`)
- `Assets/Scripts/`  
  Scripts de C# que implementan la lógica de la aplicación (IU, interacciones, controladores, servicios).
- `Assets/Scenes/`  
  Escenas de Unity (Menús principales, actividades).
- `Assets/Prefabs/`  
  Objetos de UI/juego reutilizables (paneles, botones, elementos interactivos).
- `Assets/Animations/`  
  Clips de animación y controladores utilizados para UI/objetos.
- `Assets/StreamingAssets/`  
  Archivos que deben enviarse tal cual con la compilación (configuraciones, medios o recursos de tiempo de ejecución).
- `Assets/Plugins/`  
  Complementos y SDK nativos/administrados:
  - Integración con SQLite

### Vista conceptual de módulos
- **Presentación/UI:** Interfaz de usuario de Unity Canvas + navegación por escenas 
- **Lógica:** Controladores/servicios de C# en `Scripts/`  
- **Datos:** Integraciones de SQLite `Plugins/` 

> Esta separación modular admite el mantenimiento y la evolución (por ejemplo, a futuro, la migración de SQLite en v2 a Firebase en v3).

---

## License
El código original en este repositorio está licenciado bajo **GPL-3.0-or-later** — ver `LICENSE`.

## Third-party content
Este proyecto incluye contenido de terceros (Unity Asset Store) que **no** está cubierto por GPL-3.0-or-later y se licencia bajo la **Unity Asset Store EULA** y/o términos del publisher.  
También usa dependencias de terceros como **SQLite (v1)**  
Ver `THIRD_PARTY_NOTICES.md` para licencias, términos, detalles y atribuciones requeridas.

---

## Citation
If you use this software in research, please cite it using the metadata in `CITATION.cff`.

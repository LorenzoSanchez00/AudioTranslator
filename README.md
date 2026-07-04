# AudioTranslator

### App de traducción de Audios

**Proyecto en etapa inicial / en desarrollo.** Todavía no está completo ni listo para producción.
 
Aplicación web hecha en **ASP.NET Core MVC** que permite grabar la voz del usuario desde el navegador, transcribirla, traducirla a otro idioma y generar un nuevo audio con esa traducción usando **ElevenLabs**.
 
## ¿Qué hace hasta el momento?
 
1. El usuario graba su voz directamente desde el navegador (sin necesidad de subir archivos).
2. El audio se transcribe a texto (Speech-to-Text vía ElevenLabs Scribe).
3. El texto se traduce al idioma elegido.
4. Se genera un nuevo audio con la voz traducida (Text-to-Speech vía ElevenLabs).
5. El usuario puede escuchar el resultado final desde la misma página.
```
Grabación (navegador) → Speech-to-Text → Traducción → Text-to-Speech → Audio final
```
 
## Tecnologías utilizadas
 
- **ASP.NET Core MVC** (.NET)
- **MediaRecorder API** (JavaScript) para la grabación de audio en el navegador
- **ElevenLabs API** — transcripción (Scribe) y generación de voz (Text-to-Speech)
- **GTranslate** (paquete NuGet) para la traducción de texto, sin necesidad de API key de Google Cloud
## Requisitos previos
 
- .NET SDK instalado
- Una cuenta y API Key de [ElevenLabs](https://elevenlabs.io/)

## Configuración
 
1. Cloná el repositorio:
```bash
   git clone <url-del-repo>
   cd <nombre-del-proyecto>
```
 
2. Configurá tus credenciales de ElevenLabs con user-secrets (no las subas al repo):
```bash
   dotnet user-secrets init
   dotnet user-secrets set "ElevenLabs:ApiKey" "TU_API_KEY"
   dotnet user-secrets set "ElevenLabs:VoiceId" "ID_DE_LA_VOZ"
```
 
3. Restaurá los paquetes y corré el proyecto:
```bash
   dotnet restore
   dotnet run
```
 
4. Abrí el navegador en la URL que indique la consola (por ejemplo `https://localhost:5001`).
## Estado actual del proyecto
 
Este proyecto está en **etapa inicial**. Algunas cosas pendientes / a mejorar:
 
- [ ] Manejo de errores más robusto (audio vacío, fallas de red, límites de la API)
- [ ] Loading / spinner mientras se procesa el audio
- [ ] Selección de distintas voces de ElevenLabs desde la interfaz
- [ ] Mostrar en pantalla la transcripción y la traducción antes de generar el audio final (A priorizar)
- [ ] Validación de duración/tamaño máximo de grabación
- [ ] Estilos de interfaz (actualmente muy básica)
- [ ] Tests

## Estructura del proyecto (resumida)
 
```
├── Controllers/
│   └── VozController.cs
├── Services/
│   ├── ElevenLabsService.cs
│   └── GoogleTranslateService.cs
├── Views/
│   └── Voz/
│       └── Index.cshtml
└── README.md
```
 
## 📝 Notas
 
- La traducción utiliza el paquete **GTranslate**, que no requiere API key, en lugar de la API oficial de Google Cloud Translation.
- Las API keys nunca deben subirse al repositorio; usar `dotnet user-secrets` en desarrollo y variables de entorno o un vault en producción.
---
 
Proyecto realizado con fines de aprendizaje y práctica personal.

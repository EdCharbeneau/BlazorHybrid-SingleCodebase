# Blazor Hybrid and Web in One Solution

> Below is an excerpt from the **FREE** whitepaper [Blazor Hybrid and Web in One Solution](https://www.telerik.com/campaigns/blazor/ebook-blazor-hybrid).

> An updated version using .NET 8 RC 2 can be found at: [https://github.com/EdCharbeneau/UltimateBlazorApp](https://github.com/EdCharbeneau/UltimateBlazorApp). This version is a bit more simplified than the current repo.

**Shared Business Logic**

This library will contain non-UI code that is common to more than one project in the solution. Classes, interfaces, models, and services are common items to share between projects. With Blazor this is even more practical because the UI is built using C# and .NET instead of JavaScript, therefore we can share a lot more code across the solution.

**Shared UI**

The shared UI for this solution will use a specialized library for Blazor applications called a Razor Class Library (RCL). Blazor applications of any hosting model [Server, WebAssembly, or .NET MAUI] can easily consume shared resources through a RCL. RCL's can include Razor Components, layouts, pages, and static assets such as CSS and JavaScipt. When a Blazor application is instructed to, it can search a RCL for page routes and utilize them directly, this greatly reduces the need for duplicate UI code.

**Sever**

The server project is used for an ASP.NET Core server application. With most modern applications utilize a server for Web API endpoints serving JSON data. The server will also supply static assets such as HTML, CSS, JavaScript and images. In addition, the server may be configured for server-side rendering of the Web Client application.

**Web Client**

The web client project is for the Blazor WebAssembly application. Blazor WebAssembly bootstraps the .NET runtime and configures the runtime to load the assemblies for the app. The project itself will only contain web-specific components, pages, and service implementations as most of the logic and UI comes from shared libraries in the solution.

**Desktop/Mobile Client**

The desktop/mobile client client project is for the Blazor Hybrid. A Blazor Hybrid app uses Blazor in a native client app. Razor components run natively on the device's .NET process and render web UI to an embedded Web View control using a local interop channel. In this scenario we'll use .NET MAUI for its cross-platform capaibilites, however Blazor Hybrid apps can implemented on WinForms or WPF. 

The project itself will only contain platform-specific components, pages, and service implementations as most of the logic and UI comes from shared libraries in the solution. With Blazor Hybrid, at no point does the server render HTML for the application. Use cases for server pre-rendering are generally associated with search engine optimization, and performance tuning which don't apply here.

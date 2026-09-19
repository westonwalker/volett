Absolutely — same north star, renamed to **Volett**.

## Volett — North Star

### What we are building

**Volett is an opinionated, batteries-included application framework built on ASP.NET Core and .NET 10.**

The inspiration is Laravel: give .NET developers a cohesive framework with strong conventions, excellent developer experience, and one obvious way to accomplish common application tasks.

Volett is **not a replacement for ASP.NET Core**. It sits on top of ASP.NET Core and embraces the underlying .NET ecosystem.

The goal is:

> **Make building a production-quality .NET application feel as cohesive and productive as building a Laravel application, while remaining suitable for enterprise systems and exceptionally friendly to AI coding agents.**

---

## Core philosophy

### 1. Opinionated by default

ASP.NET Core is intentionally flexible. Volett should intentionally be less flexible.

For common application concerns, there should be a **Volett way**.

```text
HTTP request         → Controller
Validation           → Request
Business operation   → Action
Persistence          → Model / EF Core
Authorization        → Policy
Background work      → Job
Internal side effect → Event / Listener
External message     → Contract / Message
Email                → Mail
User notification    → Notification
```

Developers can always drop down to ASP.NET Core or normal .NET when necessary, but Volett provides the default path.

Avoid introducing abstractions simply for architectural purity.

Optimize for:

```text
simple
predictable
discoverable
consistent
productive
```

---

## 2. Agent-first, but not an "AI framework"

Volett should be an excellent framework even without AI.

However, its conventions should make it exceptionally effective for coding agents.

Instead of an agent deciding whether an application should use repositories, MediatR, CQRS, vertical slices, service classes, FluentValidation, or another architecture, Volett gives it an established way to build applications.

The goal is to **reduce the agent's decision space**:

```text
Strong conventions
       ↓
Less architectural guessing
       ↓
More predictable generated code
       ↓
More reliable agents
```

Long term, Volett should include a Laravel Boost-like agent layer.

Agents should be able to inspect:

```text
application
routes
models
database schema
migrations
configuration
packages
logs
exceptions
tests
architecture
Volett documentation
```

Volett should therefore be designed from the beginning to be **introspectable and machine-readable**.

---

## 3. One architecture from monolith to microservices

Volett should **not have a separate microservice architecture**.

A microservice is simply another Volett application focused on a smaller domain.

A SaaS application might be:

```text
Acme/
  Controllers/
  Models/
  Actions/
  Jobs/
  Events/
  Policies/
```

An enterprise system might instead have:

```text
identity-service/
billing-service/
orders-service/
inventory-service/
```

Each repository is simply a Volett application following the same conventions.

> **Volett scales through composition rather than changing architecture.**

One Volett application can represent an entire product.

Fifty Volett applications can represent fifty independently deployed services.

Developers and coding agents only need to learn one programming model.

---

## 4. Enterprise capability is normal Volett capability

Enterprise features should not require adopting an "enterprise architecture."

Normal Volett applications should naturally support:

```text
OpenTelemetry
distributed tracing
metrics
structured logging
health/readiness checks
correlation IDs
OpenAPI
distributed caching
message buses
service-to-service HTTP
retries/timeouts/resilience
background workers
configuration/secrets
idempotency
rate limiting
authentication/authorization
```

Not everything needs to be enabled by default.

Instead, these capabilities should naturally fit into Volett when needed.

Prefer established .NET technologies underneath Volett's conventions.

For example:

```text
Volett Observability
        ↓
OpenTelemetry
        ↓
OTLP / Application Insights / etc.
```

Volett provides **coherence and defaults**, rather than reinventing proven infrastructure.

---

# Application structure

The exact structure can evolve, but conceptually a Volett application should look something like:

```text
MyApp/
│
├── Actions/
├── Controllers/
├── Events/
├── Jobs/
├── Listeners/
├── Mail/
├── Models/
├── Notifications/
├── Policies/
├── Requests/
│
├── Database/
│   ├── Migrations/
│   ├── Seeders/
│   └── Factories/
│
├── Resources/
│   ├── Css/
│   └── Js/
│
├── Views/
│
├── Tests/
│
├── Program.cs
└── appsettings.json
```

Do not blindly copy Laravel's directory structure.

Use conventions that feel natural in C# and .NET while preserving Laravel's conceptual clarity.

A developer should immediately understand where functionality belongs.

---

# Frontend philosophy

Volett owns the backend programming model.

**The developer chooses the frontend.**

First-class options should eventually include:

```text
Razor
React
Vue
API only
```

These should not result in fundamentally different Volett backends.

For example:

```bash
volett new Acme --frontend razor
volett new Acme --frontend react
volett new Acme --frontend vue
volett new Acme --frontend api
```

The underlying application architecture remains Volett.

---

# Vite

Vite should eventually be a first-class part of the Volett frontend experience.

Volett should have its own Vite integration/plugin.

```ts
import { defineConfig } from "vite";
import volett from "@volett/vite";

export default defineConfig({
	plugins: [volett()],
});
```

React:

```ts
plugins: [volett(), react()];
```

Vue:

```ts
plugins: [volett(), vue()];
```

Volett should coordinate the ASP.NET development server and Vite.

Eventually:

```bash
volett dev
```

should replace manually managing:

```bash
dotnet watch
npm run dev
```

The desired development experience:

```text
C# change       → .NET hot reload
Razor change    → refresh/hot reload
React change    → Vite Fast Refresh
Vue change      → Vite HMR
CSS/TS change   → Vite HMR
```

Volett should automatically handle development versus production assets.

---

# React/Vue integration

Longer term, Volett should consider an Inertia-like programming model.

For example:

```csharp
public async Task<IActionResult> Show(int id)
{
    var post = await Post.FindOrFail(id);

    return Volett.Page("Posts/Show", new
    {
        Post = post
    });
}
```

paired with:

```tsx
export default function Show({ post }) {
	return <h1>{post.title}</h1>;
}
```

This allows React/Vue applications while retaining server-side Volett routing, controllers, authorization, and data loading.

Volett should also support completely independent SPA/API architectures.

---

# Core framework capabilities

Volett should eventually provide cohesive solutions for:

```text
Routing
Controllers
Models
Database / EF Core
Migrations
Seeding
Factories

Authentication
Authorization / Policies
Validation / Requests

Actions
Events / Listeners
Jobs / Queues
Scheduling

Mail
Notifications
Storage
Caching

Configuration
Secrets

Logging
Metrics
Tracing
Health checks

HTTP/service clients
Messaging

Testing
CLI/scaffolding
Agent tooling
```

These represent the destination, **not the initial implementation scope**.

---

# Use the .NET ecosystem

A critical rule:

> **Volett should wrap, configure and unify proven .NET components rather than unnecessarily replacing them.**

Conceptually:

```text
Volett
 ├─ ASP.NET Core
 ├─ EF Core
 ├─ Microsoft.Extensions.*
 ├─ OpenTelemetry
 └─ established ecosystem components
```

Volett does not need its own dependency injection container, HTTP server, ORM, logging infrastructure, etc.

Its advantage is **how these technologies fit together**.

A Volett developer shouldn't spend the beginning of every project researching and assembling a dozen packages before writing application code.

---

# Integrate first, build when valuable

Volett should use existing .NET primitives when they already provide a strong foundation.

For example:

```text
HTTP                 → ASP.NET Core controllers and routing
Dependency injection → Microsoft.Extensions.DependencyInjection
Persistence          → EF Core
Logging              → Microsoft.Extensions.Logging
Observability        → OpenTelemetry
Configuration        → Microsoft.Extensions.Configuration
```

For these capabilities, Volett's responsibility is to select sensible defaults, connect the components, establish conventions, and make them easy to use in a new application.

Volett may provide its own higher-level programming model when the .NET ecosystem does not offer a sufficiently cohesive application experience.

For example:

```text
Actions
Mail
Notifications
Scheduled tasks
Jobs
Application events
```

These Volett APIs should make common application work simpler and more consistent. They should not exist merely to place a Volett name over an equivalent .NET API.

Even when Volett provides a custom API, it should use established .NET infrastructure underneath whenever practical. A Volett mailer should not reinvent mail transport, and a Volett scheduler should not reinvent background process hosting.

The decision rule is:

> **Use .NET primitives where they are already effective. Build Volett abstractions where they materially improve cohesion, convention, or developer experience.**

---

# Escape hatches

Volett must never become a prison.

Normal ASP.NET Core should remain available:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVolett();

// normal ASP.NET Core
builder.Services.AddWhatever();

var app = builder.Build();

app.UseVolett();

// normal ASP.NET Core
app.UseWhatever();

app.Run();
```

The principle is:

> **Strong defaults without hard walls.**

---

# Volett CLI

Eventually Volett should have a first-class CLI similar to Laravel Artisan.

```bash
volett new
volett dev

volett make:model
volett make:controller
volett make:request
volett make:action
volett make:policy
volett make:migration
volett make:job
volett make:event
volett make:listener
volett make:notification
volett make:mail
volett make:test

volett migrate
volett migrate:rollback
volett seed

volett routes
volett test
volett analyze
volett inspect
```

The CLI is a core part of Volett's developer experience, not an ancillary utility.

---

# Volett Analyze

Eventually Volett should understand its own conventions.

For example:

```bash
volett analyze
```

might report:

```text
✗ PostsController contains persistence/business logic.

  Consider moving this operation to CreatePost.

✗ POST /posts has no authorization policy.

✗ SendWelcomeEmail implements a queued notification
  but is being executed synchronously.

✓ 42 Volett conventions checked.
```

This becomes particularly powerful for coding agents:

```text
Agent changes code
       ↓
dotnet build
       ↓
volett test
       ↓
volett analyze
       ↓
Agent fixes issues
       ↓
done
```

The framework itself helps keep both humans and agents on the Volett path.

---

# Agent tooling

Longer term, Volett should expose structured tools/MCP.

For example:

```text
volett.application
volett.routes
volett.models
volett.model.inspect
volett.database.schema
volett.migrations
volett.config
volett.logs
volett.exceptions
volett.tests
volett.architecture
volett.docs.search
```

The same underlying introspection system should power human commands:

```bash
volett inspect model User
```

and agent tools.

Architecturally:

```text
              Volett Introspection
                 /           \
                /             \
          Volett CLI        MCP/Agents
           Humans            Machines
```

Don't build two separate introspection systems.

Agent support should influence Volett's architecture from the beginning even if MCP support isn't implemented until later.

---

# Initial repository

Start with **.NET 10** and keep the repository deliberately small.

```text
Volett/
│
├── Volett.slnx
│
├── src/
│   └── Volett/
│       ├── Volett.csproj
│       └── ...
│
├── samples/
│   └── Volett.TestApp/
│       ├── Volett.TestApp.csproj
│       ├── Program.cs
│       ├── Controllers/
│       ├── Models/
│       └── ...
│
└── tests/
    └── Volett.Tests/
        └── Volett.Tests.csproj
```

`Volett.TestApp` should reference the local `Volett` project directly.

The two testing surfaces have different purposes:

```text
Volett.Tests
     ↓
Tests Volett itself


Volett.TestApp
     ↓
Dogfoods the experience of
building an application with Volett
```

The test application is important.

When designing APIs, don't ask only:

> "Is the framework implementation elegant?"

Also ask:

> **"Would I enjoy building an application with this?"**

---

# Initial milestone

Do **not** begin by implementing queues, mail, MCP, React, Vue, OpenTelemetry, microservice tooling, storage, etc.

First establish what it means to build a Volett application.

The first milestone should get to something like:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVolett();

var app = builder.Build();

app.UseVolett();

app.Run();
```

Then establish the first core vertical slice:

```text
Volett application conventions
        ↓
Controllers
        ↓
Requests / validation
        ↓
Actions
        ↓
Models / EF Core
        ↓
Database
        ↓
Testing
```

Dogfood every major API decision in `Volett.TestApp`.

Once those primitives feel excellent, expand outward.

The early goal is **not feature count**.

The early question is:

> **What does it feel like to build a Volett application?**

---

# Decision-making rules for Codex

Use these principles when making architectural decisions:

> **When choosing between flexibility and convention, prefer convention for common application development while preserving an escape hatch to ASP.NET Core and .NET.**
>
> **When choosing between inventing infrastructure and integrating proven .NET infrastructure, prefer integration.**
>
> **When designing an API, optimize first for the Volett application developer's experience and second for framework implementation elegance.**
>
> **When introducing a convention, make it predictable enough that both developers and coding agents can discover and follow it without guessing.**
>
> **Do not introduce abstractions merely because they are common in enterprise .NET architecture. Every abstraction must materially improve the Volett developer experience.**
>
> **Do not create separate architectures for monoliths and microservices. A microservice is simply a smaller Volett application with a narrower domain boundary.**
>
> **Keep Volett simple. The goal is not to abstract .NET away. The goal is to make .NET feel cohesive.**

### The north star

> **Volett is the opinionated application framework for .NET: Laravel-like productivity, .NET's production and enterprise capabilities, and conventions designed for both humans and coding agents.**

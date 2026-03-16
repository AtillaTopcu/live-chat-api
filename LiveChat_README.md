# LiveChat API

A real-time chat API built with ASP.NET Core and SignalR for learning purposes. This project covers real-time communication, JWT authentication, room management, and clean architecture patterns.

---

## 🚀 Technologies

- **ASP.NET Core** — Web API framework
- **SignalR** — Real-time communication over WebSockets
- **Entity Framework Core** — ORM for database operations
- **SQL Server / LocalDB** — Database
- **JWT (JSON Web Token)** — Authentication
- **BCrypt** — Password hashing

---

## 📚 Concepts Learned

- **SignalR Hubs** — Real-time bidirectional communication between server and clients using WebSockets
- **SignalR Groups** — Organizing connections into rooms so messages are only delivered to the right users
- **JWT Authentication with SignalR** — Passing tokens via query string since WebSocket connections can't use headers
- **Interface** — Decoupling controllers from service implementations for better testability and maintainability
- **Dependency Injection** — Injecting services into controllers and hubs via constructor injection
- **Async/Await** — Non-blocking database operations using EF Core's async methods
- **CORS** — Configuring cross-origin resource sharing for browser-based clients
- **Room Management** — Designing a room system with dynamic join/leave functionality

---

## ⚙️ Setup

### Requirements
- [.NET 8 SDK or later](https://dotnet.microsoft.com/download)
- SQL Server or LocalDB
- [dotnet-ef tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

### Steps

1. **Clone the repository**
   ```
   git clone https://github.com/AtillaTopcu/live-chat-api.git
   cd live-chat-api
   ```

2. **Configure the application**

   Copy `appsettings.Example.json` to `appsettings.json` and fill in your values:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "YOUR_CONNECTION_STRING"
     },
     "Jwt": {
       "Key": "YOUR_SECRET_KEY_MIN_32_CHARACTERS",
       "Issuer": "LiveChat.Api",
       "Audience": "LiveChat.Api"
     }
   }
   ```

   For LocalDB, the connection string looks like:
   ```
   Server=(localdb)\mssqllocaldb;Database=LiveChatDb;Trusted_Connection=True;
   ```

3. **Install dotnet-ef (if not already installed)**
   ```
   dotnet tool install --global dotnet-ef
   ```

4. **Apply migrations**
   ```
   cd LiveChat.Api
   dotnet ef database update
   ```

5. **Run the project**
   ```
   dotnet run
   ```

6. **Test with the included HTML client**

   Open `test.html` in your browser. Login, create or join a room, and start chatting.

---

## 📡 API Endpoints

### Auth

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register a new user | No |
| POST | `/api/auth/login` | Login and receive JWT token | No |

**Register request body:**
```json
{
  "username": "john",
  "email": "john@example.com",
  "password": "yourpassword"
}
```

**Login request body:**
```json
{
  "email": "john@example.com",
  "password": "yourpassword"
}
```

---

### Rooms

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/room` | Get all rooms | No |
| POST | `/api/room` | Create a new room | ✅ Yes |

**Create room request body:**
```json
{
  "roomName": "General"
}
```

---

### Messages

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/message?roomID={id}` | Get messages for a room | ✅ Yes |

---

### SignalR Hub

**URL:** `/chat`

JWT token must be passed as a query parameter:
```
ws://localhost:PORT/chat?access_token=YOUR_TOKEN
```

| Method | Description |
|--------|-------------|
| `JoinRoom(roomId)` | Join a chat room |
| `LeaveRoom(roomId)` | Leave a chat room |
| `SendMessage(content, roomId)` | Send a message to a room |

**Client event:**
- `receivedMessage(username, message)` — fired when a new message arrives

---

## 📁 Project Structure

```
LiveChat.Api/
├── Controllers/       # HTTP request handling
├── Services/          # Business logic
├── Models/            # Database entities
├── DTOs/              # Data Transfer Objects
├── Data/              # DbContext
├── Hubs/              # SignalR Hub
└── Migrations/        # EF Core migrations
```

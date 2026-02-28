Milk Logic

Структура проекта:
Milk-Logic
    server/ 
    ├── Application 
    ├── Domain 
    ├── Infrastructure 
    ├── WebApi 
    └── OpcEmulator
    client/
    └── SPA
    .gitignore
    docker-compose.yml
    init_db.sql
    README.md

    - WebApi — HTTP API. Backend-система сбора данных датчиков. Проект построен по принципам Clean Architecture.
    - OpcEmulator — эмулятор датчиков. Простой HttpClient 
    - PostgreSQL — база данных
    - SPA - frontend приложение отображающее таблицу с данными с датчиков.

Локальный запуск:
    1.  PostgreSQL (через Docker):
        docker run -d –name milk_logic_db -e POSTGRES_DB=milk_logic_db -e POSTGRES_USER=ml_root -e POSTGRES_PASSWORD=password -p 3232:5432 postgres:13.3

    2.  WebApi:
        Файл конфигурации: server/WebApi/appsettings.json
        Пример строки подключения:
        Host=localhost;Port=3232;Database=milk_logic_db;Username=ml_root;Password=password
        Запуск: 
            cd server/WebApi 
            dotnet run
        Swagger: http://localhost:5034/swagger

    3.  OpcEmulator:
        Файл конфигурации: server/OpcEmulator/appsettings.json
        Переменные: BaseAddress=http://localhost:5034 Timeout=5
        Запуск:
            cd server/OpcEmulator 
            dotnet run

    4.  SPA:
        Файл конфигурации: client/SPA/vite.config.js
        Переменные:   target: 'http://localhost:5034',
        Запуск:
            cd client/SPA
            npm run build
        
Docker запуск:
    docker compose up -–build

Структура docker контейнера:
    milk-logic:
        milk-logic_db - postgres база данных.
        milk-logic_webapi - webapi server app.
        milk-logic_frontend  - веб-клиент для отображения данных с датчиков.
        milk-logic_opc - веб-клиент эмулятор opc сервера.

Доступы(после docker запуска): 
    WebApi: 
        http://localhost:8080/swagger 

    PostgreSQL: 
        Host:localhost Port: 3232 
        Database: milk_logic_db 
        User: ml_root 
        Password:password

     Frontend: 
        http://localhost:3000 

Переменные среды:
    WebApi: 
        ASPNETCORE_ENVIRONMENT DefaultConnection ASPNETCORE_URLS

    Docker строка подключения:
        Host=postgres;Port=5432;Database=milk_logic_db;Username=ml_root;Password=password

    OpcEmulator: 
        BaseAddress = http://localhost:5034
        Timeout = 5 (в секундах) 
        * В Docker использовать: BaseAddress=http://webapi:5034

Важно:  
    * В Docker нельзя использовать localhost для связи между контейнерами. Используются имена сервисов: webapi, postgres. 
    * Формат DateTime — ISO 8601 UTC (пример: 2026-02-28T00:00:00Z)

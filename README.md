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
        correct.xml
        README.md

Проекты:

    - Domain - доменный слой приложения, содержит определение сущностей и определяет формат репозитория.
    - Application - слой бизнес логики приложения, определяет Dto, комманды, перехватчики и мапперы.
    - Infrastructure  - слой для подключения к внешним системам, таким как базы данных и д.р.
    - WebApi — HTTP API. Backend-система сбора данных датчиков.
    - OpcEmulator — эмулятор датчиков. Простой HttpClient 
    - PostgreSQL — база данных
    - SPA - frontend приложение отображающее таблицу с данными с датчиков.

Файлы:

    - .gitignore - файл с перечислением элементов для исключения из git индексирования.
    - docker-compose.yml - файл с настройками для развёртки docker - контейнера.
    - init_db.sql - sql скрипт для создания таблицы в БД при использовании docker.
    - correct.xml - пример xml файла корректного формата для загрузки на webApi.
    - README.md - документация.

Локальный запуск:

    1.  PostgreSQL (через Docker):
        docker run -d –name milk_logic_db -e POSTGRES_DB=milk_logic_db -e POSTGRES_USER=ml_root -e POSTGRES_PASSWORD=password -p 3232:5432 postgres:13.3
        Без Docker : установить PostgerSQL, создать пользователя : ml_root  создать новую БД milk_logic_db  и выполнить sql скрипт init_db.sql.

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
            npm install
            npm run dev
        
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
    * По задаче требовалось развернуть 3 контейнера, но мной было принято решение вывести эмулятор OPC сервера в отдельный контейнер, так как это было бы на настоящем производстве.
    * Проект построен по принципам Clean Architecture.

API:

    - Создание записи данных сенсора:
       Тип: POST
       URI: /api/data
       Параметры: sensorId: int, value: float
       
    - Создание записи данных сенсора с помощью XML:
       Тип: POST
       URI: /api/data/xml
    
     - Получение данных с определённого сенсора за определённый период:
       Тип: GET
       URI: /api/data
       Параметры: sensorId: int, start: DateTime, end: DateTime
       
     - Получение агрегированных данных с определённого сенсора за определённый период:
       Тип: GET
       URI: /api/sensors/summury
       Параметры: sensorId: int, start: DateTime, end: DateTime

    

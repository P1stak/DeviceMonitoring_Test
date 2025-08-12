DeviceMonitoring (Web API + SPA)

1.	Описание:

Система мониторинга устройств с возможностью:
- Добавления/просмотра устройств
- Создания резервных копий данных
- Удаления старых записей


2.	Подготовка

- Рзархивируйте проект в нужной вам директории
- Скопируйте путь к проекту, вставьте в cmd\PowerShell

```
cd "Ваша директория/DeviceMonitoring"
```

3.	Запуск

Соберите и запустите контейнеры:


```
docker-compose build --no-cash
```

затем

```
docker-compose up
```

После сборки откройте:

Web API: http://localhost:5223/api/Device

SPA: http://localhost:4200


тестовые данные устройств добавлять через Postman:
```
    {
        "Id": "a1b2c3d4-e5f6-7890-1234-567890abcdef",
        "Name": "Office Workstation",
        "StartTime": "2025-08-08T09:00:00Z",
        "EndTime": "2025-08-08T17:30:00Z",
        "Version": "2.1.3.15"
    }
	
    {
        "Id": "b5c6d7e8-f9a0-1234-5678-901234567890",
        "Name": "Android Phone",
        "StartTime": "2025-08-08T08:15:00Z",
        "EndTime": "2025-08-08T23:45:00Z",
        "Version": "3.0.0.8"
    }
	
    {
        "Id": "a1b2c3d4-e5f6-7890-1234-567890123456",
        "Name": "Умная колонка Amazon Echo",
        "StartTime": "2025-08-08T09:00:00Z",
        "EndTime": "2025-08-08T22:30:00Z",
        "Version": "4.1.2"
    }
	
    {
        "Id": "a7b8c9d0-e1f2-3456-7890-123456789012",
        "Name": "Умная лампочка Philips Hue",
        "StartTime": "2025-08-04T15:00:00Z",
        "EndTime": "2025-08-05T04:30:00Z",
        "Version": "3.1.8"
    }

```

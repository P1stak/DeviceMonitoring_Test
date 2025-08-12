# 🚀 DeviceMonitoring (Web API + SPA)

---

## 🔹 Описание

**DeviceMonitoring** — это современная система мониторинга устройств, объединяющая **Web API** и **Single Page Application (SPA)**.

### Возможности системы:
- ✅ **Добавление и просмотр устройств**
- ✅ **Создание резервных копий данных**
- ✅ **Удаление устаревших записей**

---

## 🛠️ Подготовка к запуску

1. **Распакуйте проект** в выбранную директорию.
2. Откройте командную строку (cmd или PowerShell) и выполните:

```bash
cd ДИРЕКТОРИЯ_КУДА ВЫ_СОХРАНИЛИ_ПРОЕКТ/DeviceMonitoring
```

⚙️ Запуск проекта
Соберите контейнеры Docker:

```bash
docker-compose build --no-cache
```

Запустите контейнеры:

```bash
docker-compose up
```

После запуска откройте в браузере:

Web API:
http://localhost:5223/api/Device

SPA (Frontend):
http://localhost:4200


📋 Тестовые данные устройств
Добавляйте устройства через Postman, отправляя POST-запросы с телом:

Пример тела запроса:

```json
{
    "Id": "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    "Name": "Office Workstation",
    "StartTime": "2025-08-08T09:00:00Z",
    "EndTime": "2025-08-08T17:30:00Z",
    "Version": "2.1.3.15"
}
```

```json
{
    "Id": "b5c6d7e8-f9a0-1234-5678-901234567890",
    "Name": "Android Phone",
    "StartTime": "2025-08-08T08:15:00Z",
    "EndTime": "2025-08-08T23:45:00Z",
    "Version": "3.0.0.8"
}
```

```json
{
    "Id": "a1b2c3d4-e5f6-7890-1234-567890123456",
    "Name": "Умная колонка Amazon Echo",
    "StartTime": "2025-08-08T09:00:00Z",
    "EndTime": "2025-08-08T22:30:00Z",
    "Version": "4.1.2"
}
```

```json
{
    "Id": "a7b8c9d0-e1f2-3456-7890-123456789012",
    "Name": "Умная лампочка Philips Hue",
    "StartTime": "2025-08-04T15:00:00Z",
    "EndTime": "2025-08-05T04:30:00Z",
    "Version": "3.1.8"
}
```

# README.md

## Система мониторинга состояния объектов (Web-версия)

### Назначение системы

Система предназначена для обеспечения общественного контроля и мониторинга состояния многоквартирных домов, обслуживаемых управляющими компаниями. Она создана для улучшения качества жилищно-коммунальных услуг и повышения уровня прозрачности работы управляющих компаний.

### Технологический стек

Для разработки использовался подход "DataBase first", поскольку уже существует десктопная версия.
В разработке данного проекта использовались следующие технологии:

- **ASP.NET MVC 5**: Использовался для создания веб-приложения с использованием модели MVC.
- **Entity Framework**: Использовался как ORM для работы с базой данных.
- **MSSQL**: Использовался как система управления базами данных для хранения и обработки данных.
- **Visual Studio**: Использовался как интегрированная среда разработки для написания кода, компиляции, отладки и тестирования приложения.
- **SQL Server Management Studio**: Использовался для управления базой данных MSSQL, включая создание, изменение, удаление и запрос данных.

### Возможности системы

Система предоставляет следующие возможности:

1. Просмотр, добавление, изменение и удаление информации о звонках, обслуживаемых объектах, их технических и экономических паспортах, зарегистрированных пользователях (кроме администратора), отключениях и планируемых работах в МКД.
2. Создание рейтинга компаний на основе данных в системе.
3. Регистрация и вход в систему.
4. Отправка сообщений о проблемах.
5. Получение информации о домах и компаниях.
6. Отслеживание и получение отчетов.
7. Оставление отзывов.
8. Фильтрация данных.
9. Экспорт данных в Excel.

Система многопользовательская, веб-ориентированная, с простой в использовании базой данных.

### Запуск

1. Скачайте проект с GitHub.
2. Настройте подключение к базе данных. Вы можете создать базу данных локально или использовать удаленную базу данных. Убедитесь, что у вас есть все необходимые данные для подключения: имя сервера, имя базы данных, имя пользователя и пароль.
3. Откройте папку проекта на вашем компьютере.
4. Запустите проект через Visual Studio, используя IIS Express.
5. Готово! Вы можете использовать систему.

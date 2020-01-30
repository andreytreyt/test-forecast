Необходимо реализовать систему, которая будет предоставлять сведения о погоде в выбранном городе на указанную дату.

Система должна состоять из:
- База данных MySql 5.5 или 5.6 (хранит сведения о погоде, для работы с mysql рекомендую использовать dbForge Studio for MySql);
- Парсер (граббер) сайта http://www.gismeteo.ru/ (консольное приложение или виндослужба, которое периодически забирает данные о погоде и сохраняет в базе данных; граббер должен забирать данные о погоде по всем городам, представленным на главной странице сайта в блоке «Популярные пункты России» за 10 дней);
- WCF сервис (снабжает клиентов данными о погоде, прослойка между клиентским приложением и базой данных), хостится на IIS;
- Клиентское приложение WPF/WinForms (отображает сведения о погоде в выбранном городе на указанную дату; клиентское приложение взаимодействует только c WCF сервисом).

Решение должно быть реализовано в Visual Studio 2017 и выше с использованием .NET Framework 4.5 и выше.
К решению приложить дамп базы данных.

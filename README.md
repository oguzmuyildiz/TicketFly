## TicketFly

Gelen e-postalardan otomatik görev üreten, bunları çalışanlara atayan ve süreç takibini yöneten SaaS görev yönetim platformu.

### Hedeflenen Özellikler  
📥 E-Postadan Görev Oluşturma	Kullanıcılardan gelen e-postalar okunur ve uygun çalışana görev atanır.  
👨‍💼 Rol Tabanlı Yetkilendirme	Çalışan, Yönetici, Admin rollerine göre görev yetkileri farklılaşır.  
⏰ Görev Takibi	Geciken, yanıtlanmayan, süresi dolan görevler otomatik olarak yöneticilere rapor edilir.  
📊 Raporlama	Görevlerin durumu, tamamlanma süresi ve istatistikleri görüntülenebilir.  
🔔 Bildirim Sistemi	Geciken görevler için yöneticilere sistem içi ve mail bildirimleri, tamamlanan görevler için ticket sahibine bildirim ve mail gönderilir  
📱 Mobil Destek	Native Android & iOS uygulama ile görev takibi, bildirim alma ve yanıt verme.

🏗️ Mimarî Tasarım  
📦 Clean Architecture, CQRS, Mediator Pattern

- Gateway API (API Gateway - Yarp)
- Auth Service (.NET Core + JWT)
- Task Management Service (.NET Core)
- Notification Service (gRPC / REST + Background Worker)
- Email Parser Service (POP3/IMAP Listener)
- Reporting Service (Raporlama ve Yönetici Dashboard)
- Mobile API Layer (Kullanıcı ve görev etkileşimleri için)

🧱 Kullanılan Teknolojiler   
Backend;  .NET Core 9, REST API, gRPC, Entity Framework, AutoMapper, Fluentvalidation, Mediatr  
Frontend;	--  
Database;	MSSQL, Redis (cache), MongoDB (loglama için)  
DevOps; Docker, Docker Compose, GitHub Actions (CI/CD)  
Mobile;	--
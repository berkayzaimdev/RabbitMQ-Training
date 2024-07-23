# Message Queue

### Message Queue nedir?

- Yazılım sistemlerinde iletişim için kullanılan bir yapıdır. Message Broker sisteminin bir bileşenidir.
- Birbirinden bağımsız sistemler arasında veri alışverişi yapmak için kullanılır.
- MQ, gönderilen mesajları kuyrukta saklar ve sonradan bu mesajların işlenmesini sağlar.
- Kuyruğa mesaj gönderene Producer, kuyruktan mesaj alana Consumer isimleri verilir.

### Message Queue'nun amacı nedir?

- Mesaj gönderme-almada senkron bir davranış, kullanıcı deneyimi açısından pek uygun bir yaklaşım değildir.
- Bir e-ticaret uygulamasında sipariş yaptığımızı varsayalım. Siparişin tamamlanıp faturanın oluşturulması sonucunda kullanıcıya sipariş raporunu göndermek istiyor olabiliriz. Böylesi bir senaryoda rapor gönderimini senkron olarak bekletmek verimsiz bir yaklaşım olacaktır.
- Böylesine bir durumu çözmek için siparişin başarıyla gerçekleştirildiğine dair sonuç döndürülürken, bir yandan da MQ'ye rapor ile ilgili bir mesaj gönderilmelidir.
- Senkron iletişim modelinde istek neticesinde sonuç beklenirken, asenkron modelde sonuç beklenmez.
- Örneğin nutrihub projemde order kısmında e-mail gönderme işlemi command içerisinde senkron bir şekilde gerçekleştiriliyordu. Bunun yerine MQ kullanmak daha verimli bir yöntem olur.
- Son kullanıcı e-mail alma safhasını asenkron bir şekilde beklemelidir; bu süreç siparişin kendisinden bağımsız olmalıdır.

### Message Broker nedir?

- İçerisinde message queue'u barındıran ve bu queue üzerinden producer-consumer arasındaki iletişimi sağlayan sistemdir. (sistemin adı message broker, bileşenin adı message queue)
---
# RabbitMQ

### RabbitMQ nedir?

- Açık kaynaklı bir message queuing sistemidir.
- Erland dili ile geliştirilmiştir.
- Cross platform desteği sayesinde farklı işletim sistemlerinde de kurulabilir ve kullanılabilir.
- Zengin bir dokümantasyona sahiptir.
- Cloud'ta hizmeti mevcuttur.
- Producer -> Exchange -> Routes -> Queue -> Consumer

### RabbitMQ'yu neden kullanmalıyız?

- Yazılım uygulamalarında ölçeklendirilebilir bir ortam sağlamak istiyorsak
- Gelen isteklere anlık cevap veremiyorsak ya da anlık olmayan, zaman gereken işlemleri devreye sokmamız gerekiyorsa kullanıcıyı bekletmek yerine bu tarz süreçleri asenkron bir şekilde işleyip uygulama yoğunluğunu düşürmemiz gerekmektedir. Çünkü bu durum gereksiz bir response time süresi oluşturur.
- İşte bu tarz durumlarda asenkron süreci kontrol edecek olan yapı RabbitMQ'dur.
- RabbitMQ aracılığı ile, response'u geciktirecek operasyonların ana uygulamadan bağımsız olarak farklı bir uygulama tarafından üstlenilmesini sağlayacak olan bir mekanizma kullanılır.
- Bu mekanizmada kuyruğa gönderilen işlemler, farklı bir uygulama tarafından işlenerek sonucun asenkron olarak elde edilmesi sağlanır. Böylece ana uygulamadaki yoğunluk bir miktar düşürülür.

### Exchange nedir?

- Producer tarafından gönderilen mesajların yönetimini ve hangi route'lara yönlendirileceği noktasında görevli olan yapıdır.
- Route ise mesajların Exchange üzerinden kuyruklara nasıl gönderileceğini tanımlayan mekanizmadır.
- Bu süreçte exchange'de bulunan **routing key** değeri kullanılır. Bu değer ile mesajların hangi kuyruklara gönderileceğinin bilgisi tutulur.
- Route ise genel olarak mesajların yolunu ifade eder.
  - Direct Exchange: Mesajların direkt olarak belirli bir kuyruğa gönderilmesini sağlar (routing key ne ise oraya). Örnek senaryolar: hata mesajlarının işlenmesi, sipariş süreçleri(onaylandı, iptal edildi, iade edildi)
  - Topic Exchange: Routing key'in formatına göre, ilgili kuyruklara mesaj gönderilmesini sağlar. Örn; ```routing-key: "first.green.fast" -> ("* .green. *") && (" * . *.fast")``` Örnek senaryo: log sistemi 
  - Fanout Exchange: Mesajların bu exchange'e bind olmuş tüm kuyruklara gönderilmesini sağlar. Kuyruk isimleri dikkate alınmaz. Örnek senaryo: bir mikroservis mimarisinde tüm servislere ortak bir bildirim sağlama
  - Header Exchange: Routing key yerine header verisi kullanılarak mesaj gönderilmesini sağlar. 

### Binding nedir?

- Exchange ile kuyruk arasında bağlantı oluşturmayı ifade eder. Bir exchange birden fazla kuyruğa bağlanabilir.
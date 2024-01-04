namespace EventManagementSystem
{
    class Program
    {
        public static void Main(string[] args) {
            // Etkinliklerin tutulacağı listeyi oluştur.
            List<Event> Events = new List<Event>();

            // Event nesnesi oluştur
            Event event1 = new Event(
                "Etkinlik1",
                new DateTime(2023, 12, 31, 18, 30, 0),
                "Açıklama1",
                "Konum1",
                "Konferans",
                true,
                "Organizasyon 1",
                "organizasyon1@gmail.com",
                3
            );

            // Event nesnesi oluştur
            Event event2 = new Event(
                "Etkinlik2",
                new DateTime(2023, 12, 31, 18, 30, 0), 
                "Açıklama2",
                "Konum2",
                "Spor",
                false,
                "Organizasyon 2",
                "organizasyon2@gmail.com",
                1
            );

            Event event3 = new Event(
                "Etkinlik3",
                new DateTime(2023, 12, 31, 18, 30, 0), 
                "Açıklama3",
                "Konum3",
                "Söyleşi",
                false,
                "Organizasyon 3",
                "organizasyon3@gmail.com",
                5
            );


            Events.Add(event1);
            Events.Add(event2);
            Events.Add(event3);

            AuthObject authObject = new AuthObject(false); // IsAuthenticated: false

            Menu.HomepageMenu(authObject, Events);
        }
    }

    class Menu
    { 
        public static void HomepageMenu(AuthObject authObject, List<Event> Events)
        {
            int choice = 0;

            do
            {
                //Console.WriteLine($"Oturum Sahibi Hesap : {(authObject.IsAuthenticated ? $"\n{authObject.Username} / {authObject.Email} / {authObject.Password} / {authObject?.OrganizationName}" : "")}");
                Console.WriteLine("\n//////////////////////////////////////");
                Console.WriteLine($"{(authObject.IsAuthenticated ? "1- Hesap Değiştir" : "1- Giriş Yap")}");
                Console.WriteLine("2- Etkinlik Sayfasına Git");
                Console.WriteLine("3- Uygulamadan Çık");
                Console.WriteLine("//////////////////////////////////////");
                Console.Write("\nTercih: ");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            LoginPageMenu(ref authObject, Events);
                            break;
                        case 2:
                            EventPageMenu(ref authObject, Events);
                            break;
                        case 3:
                            Console.WriteLine("\nİyi Günler!");
                            break;
                        default:
                            Console.Error.WriteLine("\nHATA: Geçersiz bir değer girildi, tekrar deneyin.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("\nHATA: " + ex.Message.ToString() + "\n");
                }
            } while (choice != 3);
        }

        public static void LoginPageMenu(ref AuthObject authObject, List<Event> Events)
        {
            Console.WriteLine("\nGİRİŞ EKRANI --------------------------");
            int loginChoice = 0;

            do
            {
                Console.WriteLine("\n//////////////////////////////////////");
                Console.WriteLine("1- Katılımcı Girişi");
                Console.WriteLine("2- Organizasyon Girişi");
                Console.WriteLine("3- Geri Dön");
                Console.WriteLine("//////////////////////////////////////");
                Console.Write("\nTercih: ");
                try
                {
                    loginChoice = Convert.ToInt32(Console.ReadLine());
                    switch (loginChoice)
                    {
                        case 1:
                            Console.WriteLine("\nKATILIMCI GİRİŞİ --------------------------");

                            authObject = new AuthObject(null, null, null, false, null);
                            // Daha önceden organizasyon hesabıyla giriş sağlandıysa objecti temizleyerek oluşabilecek hatayı önledim.

                            Console.Write("Kullanıcı Adı: ");
                            string username = Convert.ToString(Console.ReadLine());
                            Console.Write("Email: ");
                            string email = Convert.ToString(Console.ReadLine());
                            Console.Write("Şifre: ");
                            string password = Convert.ToString(Console.ReadLine());
                            Participant newParticipant = new Participant(username, email, password, isUserAuthenticated: true);
                            Console.WriteLine("\nKullanıcı Hesabı Oluşturuldu : " + newParticipant.Id + "," + newParticipant.Username + "," + newParticipant.Email + "," + newParticipant.Password);
                            // AUTHOBJECT'İ DOLDUR.
                            authObject.Username = username;
                            authObject.Email = email;
                            authObject.Password = password;
                            authObject.IsAuthenticated = true;
                            //HomepageMenu(authObject, Events);
                            break;
                        case 2:
                            Console.WriteLine("\nORGANİZASYON GİRİŞİ --------------------------");

                            authObject = new AuthObject(null, null, null, false, null);
                            // Daha önceden katılımcı hesabıyla giriş sağlandıysa objecti temizleyerek oluşabilecek hatayı önledim.

                            Console.Write("Organizasyon Adı: ");
                            string organizationName = Convert.ToString(Console.ReadLine());
                            Console.Write("Çalışan Adı: ");
                            string organizationWorkerUsername = Convert.ToString(Console.ReadLine());
                            Console.Write("Email: ");
                            string organizationEmail = Convert.ToString(Console.ReadLine());
                            Console.Write("Şifre: ");
                            string organizationPassword = Convert.ToString(Console.ReadLine());
                            Organization newOrganization = new Organization(organizationWorkerUsername, organizationEmail, organizationPassword, organizationName, isUserAuthenticated: true);
                            Console.WriteLine("\nOrganizasyon Hesabı Oluşturuldu : " + newOrganization.Id + "," + newOrganization.OrganizationName + "," + newOrganization.Username + "," + newOrganization.Email + "," + newOrganization.Password);
                            // AUTHOBJECT'İ DOLDUR
                            authObject.Username = organizationWorkerUsername;
                            authObject.Email = organizationEmail;
                            authObject.Password = organizationPassword;
                            authObject.OrganizationName = organizationName;
                            authObject.IsAuthenticated = true;
                            break;
                        case 3:
                            break;
                        default:
                            Console.WriteLine("\nHATA: Geçersiz bir değer girildi, tekrar deneyin.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("\nHATA: " + ex.Message.ToString() + "\n");
                }
            } while (loginChoice != 3 && !authObject.IsAuthenticated);
        }

        public static void EventPageMenu(ref AuthObject authObject, List<Event> Events)
        {
            if(authObject.IsAuthenticated)
            {
                if(authObject.OrganizationName != null)
                {
                    OrganizationEventPageMenu(ref authObject, Events);
                } 
                else
                {
                    ParticipantEventPageMenu(ref authObject, Events);
                }
            }
            else
            {
                Console.WriteLine("\nUYARI: Etkinlikleri Görebilmek İçin Öncelikle Giriş Yapmalısınız.");
            }
        }

        public static void OrganizationEventPageMenu(ref AuthObject authObject, List<Event> Events)
        {
            Console.WriteLine("\nETKİNLİK EKRANI (ORGANİZASYON) --------------------------");
            int choice = 0;
            Organization organization = new Organization(authObject.Username, authObject.Email, authObject.Password, authObject.OrganizationName, authObject.IsAuthenticated);

            while (choice != 6)
            {
                Console.WriteLine("\n//////////////////////////////////////");
                Console.WriteLine("1- Tüm Etkinlikleri Görüntüle");
                Console.WriteLine("2- Yeni Bir Etkinlik Oluştur");
                Console.WriteLine("3- Oluşturduğun Etkinlikleri Görüntüle");
                Console.WriteLine("4- Oluşturduğun Bir Etkinliği Sil");
                Console.WriteLine("5- Oluşturduğun Bir Etkinlikteki Katılımcıları Görüntüle");
                Console.WriteLine("6- Geri Dön");
                Console.WriteLine("//////////////////////////////////////");
                Console.Write("\nTercih: ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            organization.DisplayAllEvents(Events);
                            break;
                        case 2:
                            try
                            {
                                Console.Write("Etkinlik Adı: ");
                                string eventName = Convert.ToString(Console.ReadLine());
                                Console.Write("Etkinlik Açıklaması: ");
                                string eventDescription = Convert.ToString(Console.ReadLine());
                                Console.Write("Etkinlik Türü: ");
                                string eventType = Convert.ToString(Console.ReadLine());
                                Console.Write("Etkinlik Tarihi (yıl, ay, gün, saat, dakika) / Virgül bırakmaya dikkat edin: ");
                                string date = Console.ReadLine();
                                string[] dateItems = date.Split(","); // Virgüllere göre ayır.
                                DateTime eventDate = new DateTime();
                                if (dateItems.Length >= 5)
                                {
                                    int year = int.Parse(dateItems[0].Trim());
                                    int month = int.Parse(dateItems[1].Trim());
                                    int day = int.Parse(dateItems[2].Trim());
                                    int hour = int.Parse(dateItems[3].Trim());
                                    int minute = int.Parse(dateItems[4].Trim());
                                    eventDate = new DateTime(year, month, day, hour, minute, 0); // Tarihi uygun formatta girdim.
                                }
                                Console.Write("Etkinlik Yeri: ");
                                string eventLocation = Convert.ToString(Console.ReadLine());
                                Console.Write("Etkinlik Sertifikalı mı? (true / false): ");
                                string isCertified = Convert.ToString(Console.ReadLine());
                                bool isEventCertified = false;
                                if (bool.TryParse(isCertified, out bool certified))
                                {
                                    isEventCertified = certified;
                                }
                                Console.Write("Etkinlik Kontenjanı: ");
                                string capacity = Convert.ToString(Console.ReadLine());
                                int eventCapacity = 0;
                                if (int.TryParse(capacity, out int capacityValue))
                                {
                                    eventCapacity = capacityValue;
                                }

                                Event newEvent = new Event(eventName, eventDate, eventDescription, eventLocation, eventType, isEventCertified, organization.OrganizationName, organization.Email, eventCapacity);
                                organization.CreateNewEvent(newEvent, Events);
                            }
                            catch (Exception ex)
                            {
                                Console.Error.WriteLine("\nHATA: " + ex.Message.ToString() + "\n");
                            }
                            break;
                        case 3:
                            organization.DisplayEventsOfTheOrganization(organization.EventListOfTheOrganization);
                            break;
                        case 4:
                            if(organization.EventListOfTheOrganization.Count > 0)
                            {
                                Console.Write("Silmek İstediğiniz Etkinliğin Adını veya Id'sini Girin: ");
                                try
                                {
                                    var value = Console.ReadLine(); // string EventName veya int Id 

                                    if (int.TryParse(value, out int eventId)) // integer'a çevrilebildiyse id'ye göre işlem yap
                                    {
                                        organization.RemoveEvent(eventId, Events);
                                    }
                                    else // çevrilemediyse name'e göre işlem yap
                                    {
                                        organization.RemoveEvent(value, Events);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.Error.WriteLine("\nHATA: " + ex.Message.ToString() + "\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nUYARI: Daha Önce Düzenlediğiniz Bir Etkinlik Bulunamadı");
                            }
                            break;
                        case 5:
                            if (organization.EventListOfTheOrganization.Count > 0)
                            {
                                Console.Write("Katılımcı Listesini Görüntülemek İstediğiniz Etkinliğin Adını veya Id'sini Girin: ");
                                try
                                {
                                    var value = Console.ReadLine(); // string EventName veya int Id 

                                    if (int.TryParse(value, out int eventId)) // integer'a çevrilebildiyse id'ye göre işlem yap
                                    {
                                        organization.getAllParticipantsToEvent(eventId);
                                    }
                                    else // çevrilemediyse name'e göre işlem yap
                                    {
                                        organization.getAllParticipantsToEvent(value);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.Error.WriteLine("\nHATA: " + ex.Message.ToString() + "\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nUYARI: Daha Önce Düzenlediğiniz Bir Etkinlik Bulunamadı");
                            }
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("\nHATA: Geçersiz bir değer girildi, tekrar deneyin.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("\nHATA: " + ex.Message.ToString() + "\n");
                }
            } 
        }

        public static void ParticipantEventPageMenu(ref AuthObject authObject, List<Event> Events)
        {
            Console.WriteLine("\nETKİNLİK EKRANI (KATILIMCI)--------------------------");
            int choice = 0;
            Participant participant = new Participant(authObject.Username, authObject.Email, authObject.Password, authObject.IsAuthenticated);

            while (choice != 5)
            {
                Console.WriteLine("\n//////////////////////////////////////");
                Console.WriteLine("1- Tüm Etkinlikleri Görüntüle");
                Console.WriteLine("2- Bir Etkinliğe Katıl");
                Console.WriteLine("3- Bir Etkinlik Kaydını iptal Et");
                Console.WriteLine("4- Kayıt Yaptırdığın Etkinlikler");
                Console.WriteLine("5- Geri Dön");
                Console.WriteLine("//////////////////////////////////////");
                Console.Write("\nTercih: ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            participant.DisplayAllEvents(Events);
                            break;
                        case 2:
                            Console.Write("Katılım Sağlamak İstediğiniz Etkinliğin Adını veya Id'sini Girin: ");
                            try
                            {
                                var value = Console.ReadLine(); // string EventName veya int Id 

                                if (int.TryParse(value, out int eventId)) // integer'a çevrilebildiyse id'ye göre işlem yap
                                {
                                    participant.AttendEvent(eventId, participant, Events);
                                }
                                else // çevrilemediyse name'e göre işlem yap
                                {
                                    participant.AttendEvent(value, participant, Events);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.Error.WriteLine("\nHATA: " + ex.Message.ToString() + "\n");
                            }
                            break;
                        case 3:
                            if (participant.AttendedEvents.Count > 0) // Katıldığı eventler varsa
                            {
                                Console.Write("İptal Etmek İstediğiniz Etkinliğin Adını veya Id'sini Girin: ");
                                try
                                {
                                    var value = Console.ReadLine(); // string EventName veya int Id 

                                    if (int.TryParse(value, out int eventId)) // integer'a çevrilebildiyse id'ye göre işlem yap
                                    {
                                        participant.CancelAttendingTheEvent(eventId, participant, Events);
                                    }
                                    else // çevrilemediyse name'e göre işlem yap
                                    {
                                        participant.CancelAttendingTheEvent(value, participant, participant.AttendedEvents);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Console.Error.WriteLine("\nHATA: " + ex.Message.ToString() + "\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nUYARI: Kayıt Yapmış Olduğunuz Bir Etkinlik Bulunamadı.");
                            }
                            break;
                        case 4:
                            if (participant.AttendedEvents.Count > 0) // Katıldığı eventler varsa
                            {
                                participant.DisplayAttendedEvents(participant.AttendedEvents);
                            }
                            else
                            {
                                Console.WriteLine("\nUYARI: Kayıt Yapmış Olduğunuz Bir Etkinlik Bulunamadı.");
                            }
                            break;
                        case 5:
                            break;
                        default:
                            Console.WriteLine("\nHATA: Geçersiz bir değer girildi, tekrar deneyin.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("\nHATA: " + ex.Message.ToString() + "\n");
                }
            }
        }
    }

    class AuthObject
    {
        public int Id { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string OrganizationName { get; set; }

        public AuthObject(string Username, string Email, string Password, bool IsAuthenticated)
        {
            Id = new Random().Next();
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
            this.IsAuthenticated = IsAuthenticated;
        }

        // overload : Organizasyon Adını da alan bir yapıcı blok
        public AuthObject(string Username, string Email, string Password, bool IsAuthenticated, string OrganizationName)
        {
            Id = new Random().Next();
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
            this.IsAuthenticated = IsAuthenticated;
            this.OrganizationName = OrganizationName;
        }

        // overload : sadece IsAuthenticated bilgisini alan bir yapıcı blok
        public AuthObject(bool IsAuthenticated)
        {
            this.IsAuthenticated = IsAuthenticated;
        }

    }

    class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isUserAuthenticated { get; set; } // kullanıcı oturum açmış mı?

        // yapıcı blok
        public User(string Username, string Email, string Password,bool isUserAuthenticated) 
        {
            // C# Random kütüphanesinden random bir sayı alıp Id değişkenine atıyor.
            Id = new Random().Next();
            this.Username = Username;   
            this.Email = Email;
            this.Password = Password;
            this.isUserAuthenticated = isUserAuthenticated;
        }
    }


    class Participant : User, IParticipantEvent
    {
        // katıldığı eventlerin listesi
        public List<Event> AttendedEvents { get; set; } 

        // yapıcı blok
        public Participant(string Username, string Email, string Password, bool isUserAuthenticated)
            :base(Username, Email, Password, isUserAuthenticated)
        {
            AttendedEvents = new List<Event>(); // nesne oluşturulurken listeyi de oluştur.
        }
        
        // implemente edilen metotlar
        public void AttendEvent(int eventId, Participant participantBody, List<Event> Events)
        {
            Event foundedEvent = Events.Find((eventItem) => eventId.Equals(eventItem.Id));

            if (foundedEvent != null)
            {
                participantBody.AttendedEvents.Add(foundedEvent); // katılımcının event listesine eventi ekle
                foundedEvent.EventParticipantList.Add(participantBody); // eventin katılımcı listesine katılımcıyı ekle
                Console.WriteLine($"\nKatılım sağlandı: {participantBody.Username} - {foundedEvent.EventName}");
            }
            else
            {
                Console.WriteLine($"\nEtkinlik bulunamadı: {eventId}");
            }
        }

        public void AttendEvent(string eventName, Participant participantBody, List<Event> Events)
        {
            Event foundedEvent = Events.Find((eventItem) => eventName.Equals(eventItem.EventName));

            if (foundedEvent != null)
            {
                participantBody.AttendedEvents.Add(foundedEvent); // Add the event to the participant's event list
                foundedEvent.EventParticipantList.Add(participantBody); // Add the participant to the event's participant list
                Console.WriteLine($"\nKatılım sağlandı: {participantBody.Username} - {foundedEvent.EventName}");
            }
            else
            {
                Console.WriteLine($"\nEtkinlik bulunamadı: {eventName}");
            }
        }

        public void CancelAttendingTheEvent(string eventName, Participant participantBody, List<Event> AttendedEvents)
        {
            Event foundedEvent = AttendedEvents.Find((eventItem) => eventName.Equals(eventItem.EventName)); // katıldığı eventlerin arasında ara

            if (foundedEvent != null)
            {
                participantBody.AttendedEvents.Remove(foundedEvent); // katılımcının event listesinden çıkar
                foundedEvent.EventParticipantList.Remove(participantBody); // eventin katılımcı listesinden çıkar
                Console.WriteLine($"\nKatılım İptal Edildi: {participantBody.Username} - {foundedEvent.EventName}");
            }
            else
            {
                Console.WriteLine($"\nEtkinlik bulunamadı: {eventName}");
            }
        }

        public void CancelAttendingTheEvent(int eventId, Participant participantBody, List<Event> AttendedEvents)
        {
            Event foundedEvent = AttendedEvents.Find((eventItem) => eventId.Equals(eventItem.Id)); // katıldığı eventlerin arasında ara

            if (foundedEvent != null)
            {
                participantBody.AttendedEvents.Remove(foundedEvent); // katılımcının event listesinden çıkar
                foundedEvent.EventParticipantList.Remove(participantBody); // eventin katılımcı listesinden çıkar
                Console.WriteLine($"\nKatılım İptal Edildi: {participantBody.Username} - {foundedEvent.EventName}");
            }
            else
            {
                Console.WriteLine($"\nEtkinlik bulunamadı: {eventId}");
            }
        }

        public void DisplayAllEvents(List<Event> Events)
        {
            ConsoleDesign.TextInTheMiddleOfRectangleConsoleDesign("ETKİNLİKLER");
            foreach (var eventItem in Events)
            {
                Console.WriteLine("   " + eventItem.EventName + " (" + eventItem.Id + ")");
                Console.WriteLine("   -Açıklama : " + eventItem.EventDescription);
                Console.WriteLine("   -Tarih    : " + eventItem.EventDate);
                Console.WriteLine("   -Konum    : " + eventItem.EventLocation);
                Console.WriteLine("   -Tür      : " + eventItem.EventType);
                Console.WriteLine("   -Sertifika: " + (eventItem.isEventCertified ? "VAR" : "YOK"));
                Console.WriteLine("   -İletişim : " + eventItem.EventContact);
                Console.WriteLine("   -Kontenjan: " + eventItem.EventCapacity);
                Console.WriteLine("   -Katılımcı Sayısı: " + eventItem.EventParticipantList.Count);
                Console.WriteLine(" ----------------------------------------------------------------------");
            }
        }

        public void DisplayAttendedEvents(List<Event> AttendedEvents)
        {
            ConsoleDesign.TextInTheMiddleOfRectangleConsoleDesign("KAYIT OLDUĞUN ETKİNLİKLER");
            foreach (var eventItem in AttendedEvents)
            {
                Console.WriteLine("   " + eventItem.EventName + " (" + eventItem.Id + ")");
                Console.WriteLine("   -Açıklama : " + eventItem.EventDescription);
                Console.WriteLine("   -Tarih    : " + eventItem.EventDate);
                Console.WriteLine("   -Konum    : " + eventItem.EventLocation);
                Console.WriteLine("   -Tür      : " + eventItem.EventType);
                Console.WriteLine("   -Sertifika: " + (eventItem.isEventCertified ? "VAR" : "YOK"));
                Console.WriteLine("   -İletişim : " + eventItem.EventContact);
                Console.WriteLine("   -Kontenjan: " + eventItem.EventCapacity);
                Console.WriteLine("   -Katılımcı Sayısı: " + eventItem.EventParticipantList.Count);
                Console.WriteLine(" ----------------------------------------------------------------------");
            }
        }
    }

    // Organizasyon hesapları için bir sınıf
    class Organization : User, IOrganizationEvent
    {
        // özellikler
        public string OrganizationName { get; set; }
        public List<Event> EventListOfTheOrganization { get; set; } // organizasyonun etkinliklerinin listesi

        // yapıcı blok
        public Organization(string Username, string Email, string Password, string OrganizationName, bool isUserAuthenticated)
            : base(Username, Email, Password, isUserAuthenticated) // User sınıfından aldığımız özellikler
        {
            this.OrganizationName = OrganizationName;
            EventListOfTheOrganization = new List<Event>(); // nesne oluşturulurken listeyi de oluştur.
        }
        
        // interface'ten implemente edilen metotlar
        public void DisplayAllEvents(List<Event> Events)
        {
            ConsoleDesign.TextInTheMiddleOfRectangleConsoleDesign("ETKİNLİKLER");
            // Events listesindeki elemanları dolaşıyor. Her bir etkinlik için ekrana bilgileri yazdırıyor.
            foreach(var eventItem in Events)
            {
                Console.WriteLine("   "+eventItem.EventName + " (" + eventItem.Id + ")");
                Console.WriteLine("   -Açıklama : " + eventItem.EventDescription);
                Console.WriteLine("   -Tarih    : " + eventItem.EventDate);
                Console.WriteLine("   -Konum    : " + eventItem.EventLocation);
                Console.WriteLine("   -Tür      : " + eventItem.EventType);
                Console.WriteLine("   -Sertifika: " + (eventItem.isEventCertified ? "VAR" : "YOK"));
                Console.WriteLine("   -İletişim : " + eventItem.EventContact);
                Console.WriteLine("   -Kontenjan: " + eventItem.EventCapacity);
                Console.WriteLine("   -Katılımcı Sayısı: " + eventItem.EventParticipantList.Count);
                Console.WriteLine(" ----------------------------------------------------------------------");
            }
        }

        public void CreateNewEvent(Event eventBody, List<Event> Events)
        {
            Events.Add(eventBody); // eventlere kaydet
            this.EventListOfTheOrganization.Add(eventBody); // organizasyonun eventlerine kaydet
            Console.WriteLine("\nYeni Etkinlik Oluşturuldu : "+eventBody.EventName+ " / "+eventBody.EventHostOrganization);
        }

        public void DisplayEventsOfTheOrganization(List<Event> EventListOfTheOrganization)
        {
            if(EventListOfTheOrganization.Count>0)
            {
                ConsoleDesign.TextInTheMiddleOfRectangleConsoleDesign("DÜZENLENEN ETKİNLİKLER");
                foreach (var eventItem in EventListOfTheOrganization)
                {
                    Console.WriteLine("   " + eventItem.EventName + " (" + eventItem.Id + ")");
                    Console.WriteLine("   -Açıklama : " + eventItem.EventDescription);
                    Console.WriteLine("   -Tarih    : " + eventItem.EventDate);
                    Console.WriteLine("   -Konum    : " + eventItem.EventLocation);
                    Console.WriteLine("   -Tür      : " + eventItem.EventType);
                    Console.WriteLine("   -Sertifika: " + (eventItem.isEventCertified ? "VAR" : "YOK"));
                    Console.WriteLine("   -İletişim : " + eventItem.EventContact);
                    Console.WriteLine("   -Kontenjan: " + eventItem.EventCapacity);
                    Console.WriteLine("   -Katılımcı Sayısı: " + eventItem.EventParticipantList.Count);
                    Console.WriteLine(" ----------------------------------------------------------------------");
                }
            } 
            else
            {
                Console.WriteLine("\nUYARI: Düzenlemiş Olduğunuz Bir Etkinlik Bulunamadı.");
            }
        }

        public void RemoveEvent(int eventId, List<Event> Events)
        {
            Event foundedEvent = EventListOfTheOrganization.Find((eventItem) => eventItem.Id == eventId);
            
            if(foundedEvent != null)
            {
                this.EventListOfTheOrganization.Remove(foundedEvent);
                Events.Remove(foundedEvent);
                Console.WriteLine("Etkinlik Silindi:  "+foundedEvent.EventName);
            }
            else
            {
                Console.WriteLine("\nUYARI: Bu Id ile Kaydedilmiş Bir Etkinlik Bulunamadı.");
            }
        }

        public void RemoveEvent(string eventName, List<Event> Events)
        {
            Event foundedEvent = EventListOfTheOrganization.Find((eventItem) => eventName.Equals(eventItem.EventName));
            
            if(foundedEvent != null)
            {
                this.EventListOfTheOrganization.Remove(foundedEvent);
                Events.Remove(foundedEvent);
                Console.WriteLine("Etkinlik Silindi:  " + foundedEvent.EventName);
            }
            else
            {
                Console.WriteLine("\nUYARI: Bu İsimle Kaydedilmiş Bir Etkinlik Bulunamadı.");
            }
        }

        public void getAllParticipantsToEvent(int eventId)
        {
            Event foundedEvent = EventListOfTheOrganization.Find((eventItem) => eventItem.Id == eventId);

            if(foundedEvent != null)
            {
                foundedEvent.EventParticipantList.ForEach((participant) =>
                {
                    Console.WriteLine(participant.Id + " / " + participant.Username + " / " + participant.Email);
                });
            }
            else
            {
                Console.WriteLine("\nUYARI: Bu Id ile Kaydedilmiş Bir Etkinlik Bulunamadı.");
            }
        }

        public void getAllParticipantsToEvent(string eventName)
        {
            Event foundedEvent = EventListOfTheOrganization.Find((eventItem) => eventName.Equals(eventItem.EventName));

            if (foundedEvent != null)
            {
                foundedEvent.EventParticipantList.ForEach((participant) =>
                {
                    Console.WriteLine(participant.Id + " / " + participant.Username + " / " + participant.Email);
                });
            }
            else
            {
                Console.WriteLine("\nUYARI: Bu İsimle Kaydedilmiş Bir Etkinlik Bulunamadı.");
            }
        }
    }

    class Event
    { 
        // yapıcı blok
        public Event(string EventName, DateTime EventDate, string EventDescription, string EventLocation, string EventType, bool isEventCertified, 
            string EventHostOrganization, string EventContact, int EventCapacity) 
        {
            Id = new Random().Next();
            this.EventName = EventName;
            this.EventDate = EventDate; // (?)
            this.EventDescription = EventDescription;
            this.EventLocation = EventLocation;
            this.EventType = EventType;
            this.isEventCertified = isEventCertified;   
            this.EventHostOrganization = EventHostOrganization;
            this.EventContact = EventContact;
            this.EventCapacity = EventCapacity;
            EventParticipantList = new List<Participant>(); // nesne oluşturulurken listeyi de oluştur.
        }

        // etkinliğin özellikleri => kapsülleme (get; set;)
        public int Id { get; set; }
        public string EventName { get; set; } 
        public DateTime EventDate { get; set; } 
        public string EventDescription { get; set; }
        public string EventLocation { get; set; }
        public string EventType { get; set; }
        public bool isEventCertified { get; set; }
        public string EventHostOrganization { get; set; } // eventi düzenleyen organizasyon
        public string EventContact { get; set; }
        public List<Participant> EventParticipantList { get; set;} // evente katılanların listesi
        public int EventCapacity { get; set; }
    }

    // Organization sınıfında kullanılacak metotları tutan interface.
    interface IOrganizationEvent
    {
        public void DisplayAllEvents(List<Event> Events){} // tüm etkinlikleri göster
        public void CreateNewEvent(Event eventBody, List<Event> Events); // yeni etkinlik oluştur.
        public void DisplayEventsOfTheOrganization(List<Event> EventListOfTheOrganization);
        public void RemoveEvent(int eventId, List<Event> Events); // id ye göre event sil.
        public void RemoveEvent(string eventName, List<Event> Events); // isme göre event sil.
        public void getAllParticipantsToEvent(int eventId); // tüm katılımcıları getir.
        public void getAllParticipantsToEvent(string eventName); // tüm katılımcıları getir.
    }

    interface IParticipantEvent
    {
        public void DisplayAllEvents(List<Event> Events); // tüm eventleri göster.
        public void DisplayAttendedEvents(List<Event> AttendedEvents); // katıldığı eventleri göster.
        public void AttendEvent(int eventId, Participant participantBody, List<Event> Events); // evente katılım sağla.
        public void AttendEvent(string eventName, Participant participantBody, List<Event> Events); // evente katılım sağla.
        public void CancelAttendingTheEvent(int eventId, Participant participantBody, List<Event> AttendedEvents); // katılımı iptal et.
        public void CancelAttendingTheEvent(string eventName, Participant participantBody, List<Event> AttendedEvents); // katılımı iptal et.
    }

    public enum EventType
    {
        Konferans,
        Online,
        Konser,
        Spor,
        Seminer
    }

    class ConsoleDesign
    {
        public static void TextInTheMiddleOfRectangleConsoleDesign(string text)
        {
            Console.WriteLine("\n ----------------------------------------------------------------------");
            Console.WriteLine("|                                                                     |");
            Console.WriteLine("                      " + text + "                              ");
            Console.WriteLine("|                                                                     |");
            Console.WriteLine(" ----------------------------------------------------------------------");
        }
    }
}

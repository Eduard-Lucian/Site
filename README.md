# 🌍 TravelBooking - Platformă Web de Rezervări Turistice

Această platformă este un sistem complet de rezervări turistice, conceput să ofere o experiență de utilizare fluidă și o gestiune securizată a datelor, atât pentru clienți, cât și pentru administratorii de sistem.

Proiectul a fost construit de la zero, punând accent pe o arhitectură modulară, performanță și securitate. Acesta a fost inspirat de site-uri precum **Booking.com** sau **Airbnb**, TravelBooking este o platformă web completă (Full-Stack) dezvoltată pentru a digitaliza și simplifica experiența rezervărilor de vacanță. Proiectul simulează un mediu de business real din turism, punând accent pe viteză, securitate și un flux de utilizare extrem de intuitiv.

---

## 🎯 Ce face aplicația mai exact?

Am creat un mediu digital în care utilizatorii pot explora destinații și efectua rezervări, totul fiind susținut de un sistem robust de administrare în backend:

* **🏨 Gestiune Dinamică a Ofertelor:** Administratorii pot adăuga și edita unități de cazare. Un aspect important este **calculul automat al prețurilor**: prețul final al unei camere este generat dinamic în backend, adunând la tariful de bază costul fiecărei facilități selectate (Piscină, Spa, Wi-Fi, etc.), eliminând erorile umane.
* **🔐 Sistem de Autentificare și Securitate:** Am implementat ASP.NET Core Identity pentru gestionarea conturilor. Procesul de înregistrare este optimizat pentru UX: după crearea contului, utilizatorul este redirecționat la Login cu datele deja completate, reducând efortul de autentificare.
* **📸 Personalizarea Profilului:** Utilizatorii își pot gestiona identitatea digitală prin încărcarea unor fotografii de profil. Sistemul gestionează automat salvarea fișierelor pe server și afișarea lor dinamică în Navbar și meniul lateral, folosind inițialele numelui ca fallback elegant dacă poza lipsește.
* **🛡️ Administrare (Admin Panel):** Am creat o zonă restricționată exclusiv pentru administratori, care pot gestiona conturile utilizatorilor (schimbare roluri, ștergere) și pot supraveghea întreg inventarul de hoteluri.
* **🧳 Dashboard Clienți:** Fiecare utilizator are un istoric personalizat al rezervărilor, cu funcții rapide de filtrare a statusului (Plătite / Anulate) executate instantaneu prin JavaScript, fără reîncărcarea paginii.

---

## 💻 Detalii Tehnice (Arhitectură)

Pentru a asigura un cod curat, scalabil și ușor de testat, am adoptat arhitectura **Service-Repository Pattern**:

* **Backend:** ASP.NET Core MVC 8, C#, Entity Framework Core (SQL Server).
* **Service Layer:** Întreaga logică de business (validări, calcul prețuri, upload-uri) este extrasă din controlere și izolată în servicii, păstrând **controlerele „subțiri”**.
* **Frontend:** HTML5, CSS3 și Vanilla JavaScript, fără biblioteci vizuale externe, pentru a asigura o viteză maximă de randare.


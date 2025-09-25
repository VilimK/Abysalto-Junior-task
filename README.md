## Preduvjeti

- Docker Desktop

## Pokretanje (Docker)

otpakirati zip file te se pozicionirati gdje se nalazi docker-compose.yml

pokrenuti naredbu ---> docker compose up --build

nakon toga preko docker desktopa (ili preko terminala) pokrenuti sql server te api ako već nije pokrenut

## Pristup Endpointovima preko swaggera (ispis u konzoli)

Swagger UI: http://localhost:5000/swagger

## Dokumentacija
## 📖 Orders API Dokumentacija

#### **POST `/orders`**
- Parametri (body):
  ```json
  {
    "buyerName": "Ivan Ivković",
    "address": "Glavna 100, Zagreb",
    "contactNumber": "0914582582",
    "paymentTypeId": 1,
    "statusId": 1,
    "currencyId": 1,
    "orderItems": [
      { "articleId": 1, "quantity": 2 },
      { "articleId": 3, "quantity": 1 }
    ]
  }
Vraća: Order (novokreirana narudžba)
Opis: Kreira novu narudžbu s artiklima, računa ukupni iznos i sprema je u bazu.

#### GET /orders/active
Parametri: nema
Vraća: List<Order>
Opis: Dohvaća sve aktivne narudžbe (statusi PND i PRP).

#### GET /orders/completed
Parametri: nema
Vraća: List<Order>
Opis: Dohvaća sve dovršene narudžbe (status CMP).

#### PATCH /orders/{orderId}/statusupdate
Parametri (route):
orderId (int) – ID narudžbe koju treba ažurirati
Vraća: Order (ažurirana narudžba)
Opis: Ažurira status određene narudžbe (npr. iz Pending → Preparing → Completed).

#### GET /orders/{orderId}/amount
Parametri (route):
orderId (int) – ID narudžbe
Vraća: decimal (ukupni iznos narudžbe)
Opis: Vraća ukupan iznos određene narudžbe.

#### GET /orders/sorted-by-amount

Parametri: nema

Vraća: List<Order>

Opis: Dohvaća sve narudžbe, sortirane prema ukupnom iznosu od najmanjeg do najvećeg.


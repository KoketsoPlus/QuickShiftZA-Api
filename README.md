# QuickShift ZA 🇿🇦

> **Empowering South African youth and women entrepreneurs through local gig services**

QuickShift ZA is a cloud-powered skills marketplace that connects South African service providers — hairdressers, caterers, cleaners, gardeners and more — with customers in their local communities. Built for the **Huawei Code4Mzansi 2026** competition.

---

## 🌍 The Problem

Millions of skilled South Africans — hairdressers in Soweto, caterers in Durban, tailors in Cape Town — have no easy way to advertise their services or find customers beyond word of mouth. At the same time, customers struggle to find trusted, local, affordable service providers.

QuickShift ZA solves this by creating a digital marketplace specifically designed for South Africa's informal and semi-formal gig economy.

---

## ✅ The Solution

A full-stack web platform where:
- **Customers** post gigs describing the service they need, their location, and budget
- **Workers** browse available gigs and submit proposals
- **AI-assisted pricing** flags whether a proposed price is fair, too low, or too high
- **OBS-powered image uploads** allow workers to showcase their work portfolio
- **JWT-secured authentication** keeps user accounts safe

---

## 🏗️ Architecture

```
┌─────────────────────┐         ┌──────────────────────────┐
│   React Frontend    │ ──────► │   ASP.NET Core API       │
│   (Vite + Tailwind) │         │   (ECS - Ubuntu Server)  │
└─────────────────────┘         └────────────┬─────────────┘
                                             │
                    ┌────────────────────────┼────────────────────────┐
                    │                        │                        │
          ┌─────────▼──────┐    ┌────────────▼───────┐   ┌──────────▼──────────┐
          │  Huawei RDS    │    │   Huawei OBS       │   │  ModelArts (Future) │
          │  PostgreSQL    │    │   Image Storage    │   │  AI Price Analysis  │
          └────────────────┘    └────────────────────┘   └─────────────────────┘
```

---

## ☁️ Huawei Cloud Services Used

| Service | Purpose |
|---|---|
| **ECS (Elastic Cloud Server)** | Hosts the ASP.NET Core backend API on Ubuntu 22.04 |
| **RDS for PostgreSQL** | Stores all application data (users, gigs, proposals, ratings) |
| **OBS (Object Storage Service)** | Stores worker profile pictures and work portfolio images |
| **VPC + Security Groups** | Secure private networking between ECS and RDS |
| **EIP (Elastic IP)** | Public access to the backend API |
| **ModelArts** | Planned future integration for advanced AI price benchmarking |

---

## 🚀 Features

### For Customers
- Register and log in securely
- Post gigs with title, description, category, location and budget
- Get instant AI-assisted price feedback (fair / too low / too high)
- Browse worker proposals and accept the best offer

### For Workers
- Create a profile showcasing their skills and area
- Browse available gigs by category and location
- Submit proposals with a custom price and message
- Upload portfolio images to attract more customers

### Platform
- JWT-based authentication
- Role-based access (customer / worker)
- Price benchmark engine with SA market data
- Image upload to Huawei OBS
- Responsive mobile-first design

---

## 🛠️ Tech Stack

### Frontend
- React 18 + TypeScript
- Vite
- Tailwind CSS
- Shadcn/ui components
- Axios for API calls
- React Router v6
- Framer Motion animations

### Backend
- ASP.NET Core 9 (C#)
- Entity Framework Core
- PostgreSQL (Huawei RDS)
- JWT Authentication
- BCrypt password hashing
- AWS S3 SDK (S3-compatible with Huawei OBS)

---

## 📁 Project Structure

```
QuickShiftZA.Api/
├── Controllers/
│   ├── UsersController.cs       # Register, Login
│   ├── GigsController.cs        # Create, Browse gigs
│   ├── ProposalsController.cs   # Submit proposals
│   ├── BenchmarksController.cs  # Price benchmarks
│   └── ImagesController.cs      # OBS image upload
├── Models/
│   ├── User.cs
│   ├── Gig.cs
│   ├── Proposal.cs
│   ├── WorkerProfile.cs
│   ├── Rating.cs
│   └── PriceBenchmark.cs
├── Services/
│   ├── PriceCheckerService.cs   # Price fairness logic
│   └── ObsService.cs            # Huawei OBS integration
├── Data/
│   ├── AppDbContext.cs
│   └── SeedData.cs              # SA market price benchmarks
└── DTOs/
    ├── CreateGigDto.cs
    ├── CreateProposalDto.cs
    ├── RegisterDto.cs
    └── LoginDto.cs

quick-gig-za/ (Frontend)
├── src/
│   ├── pages/
│   │   ├── Home.tsx
│   │   ├── Login.tsx
│   │   ├── Signup.tsx
│   │   ├── BrowseGigs.tsx
│   │   ├── PostGig.tsx
│   │   ├── GigDetail.tsx
│   │   ├── MyGigs.tsx
│   │   └── Providers.tsx
│   ├── components/
│   │   ├── GigCard.tsx
│   │   ├── ProviderCard.tsx
│   │   └── Navbar.tsx
│   └── lib/
│       └── api.ts               # Axios API client
```

---

## ⚙️ Setup & Deployment

### Prerequisites
- .NET 9 SDK
- Node.js 18+
- PostgreSQL (or Huawei RDS)

### Backend Setup

```bash
# Clone the repository
git clone https://github.com/KoketsoPlus/QuickShiftZA-Api.git
cd QuickShiftZA-Api

# Create appsettings.json (never committed - contains secrets)
# See appsettings.example.json for structure

# Install dependencies
dotnet restore

# Run migrations
dotnet ef database update

# Start the API
dotnet run --urls "http://0.0.0.0:5000"
```

### Frontend Setup

```bash
cd quick-gig-za
npm install
npm run dev
```

### Environment Variables (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=YOUR_RDS_HOST;Port=5432;Database=quickshiftza;Username=root;Password=YOUR_PASSWORD"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_MIN_32_CHARS",
    "Issuer": "QuickShiftZA",
    "Audience": "QuickShiftZAUsers"
  },
  "OBS": {
    "AccessKey": "YOUR_HUAWEI_ACCESS_KEY",
    "SecretKey": "YOUR_HUAWEI_SECRET_KEY",
    "BucketName": "quickshiftza-media",
    "ServiceURL": "https://obs.af-south-1.myhuaweicloud.com"
  }
}
```

---

## 🔌 API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/users/register` | Register a new user |
| POST | `/api/users/login` | Login and get JWT token |
| GET | `/api/gigs` | Get all gigs (filter by area) |
| POST | `/api/gigs` | Create a new gig |
| GET | `/api/gigs/{id}` | Get a specific gig |
| DELETE | `/api/gigs/{id}` | Delete a gig |
| POST | `/api/proposals` | Submit a proposal |
| GET | `/api/proposals` | Get proposals |
| POST | `/api/images/upload` | Upload image to OBS |

---

## 💰 Price Benchmark Categories

QuickShift ZA uses real South African market data to validate gig pricing:

| Category | Areas Covered |
|---|---|
| Hairdressing | Johannesburg, Cape Town, Durban, Pretoria, Soweto |
| Catering | Johannesburg, Cape Town, Durban, Pretoria, Soweto |
| Cleaning | Johannesburg, Cape Town, Durban, Pretoria, Soweto |
| Gardening | Johannesburg, Cape Town, Durban, Pretoria, Soweto |
| Plumbing | Johannesburg, Cape Town, Durban, Pretoria, Soweto |
| Tutoring | Johannesburg, Cape Town, Durban, Pretoria, Soweto |

---

## 🔮 Future Enhancements

- **Huawei ModelArts integration** — Train an AI model on real SA gig pricing data for smarter price recommendations
- **In-app messaging** — Allow customers and workers to communicate directly
- **Payment integration** — Secure in-app payments using SA payment gateways
- **Rating system** — Allow customers to rate workers after job completion
- **Push notifications** — Notify workers of new gigs matching their skills
- **Mobile app** — React Native version for Android and iOS

---

## 👨‍💻 Team

Built with ❤️ for South Africa by **Koketso** for the **Huawei Code4Mzansi 2026** competition.

---

## 📄 License

MIT License — feel free to use and build on this project.

---

*QuickShift ZA — Shifting the way South Africa works, one gig at a time.* ⚡

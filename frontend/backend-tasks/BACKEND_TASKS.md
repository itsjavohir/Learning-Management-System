# Backend API contract — что нужно доделать/добавить

Фронт (React + FSD) готов и ждёт эти эндпоинты. Все ответы — camelCase JSON (как и сейчас: `LoginResponse` с `AccessToken` на C# уходит как `accessToken` в JSON — держим тот же стиль везде). Ошибки — `400/401/403/404` с телом `{ "message": "..." }`, фронт везде читает `err.response.data.message`.

Base URL уже настроен через `VITE_API_URL` (сейчас `.../api`), все пути ниже — относительно него.

---

## 0. Уже реализовано на фронте (просто фиксирую контракт, чтобы не разъехалось)

| Метод | Путь | Статус |
|---|---|---|
| POST | `/auth/login` | готово |
| POST | `/auth/forgot-password` | готово (channel 1=Email/2=Telegram) |
| POST | `/auth/reset-password` | готово, возвращает `{accessToken, refreshToken, mustChangePassword}` |
| POST | `/auth/change-password` | готово |
| POST | `/auth/refresh-token` | готово |
| GET/POST/PUT/DELETE | `/users` | готово (Create/Update/Delete через модалки) |
| GET/POST/PUT/DELETE | `/courses` | готово. ⚠️ **уточни**: `delete` сейчас шлёт `DELETE /courses` с `{id}` в body — лучше сделать стандартный `DELETE /courses/{id}`, я тогда поправлю фронт |
| GET/POST/PUT/DELETE | `/groups` | готово |
| GET/PUT | `/mentors/profile` | готово (собственный профиль ментора) |
| GET | `/theme/season` | готово (сезонная тема auth-страниц) |

---

## 1. `GET /api/auth/me` — профиль текущего юзера

Сейчас в Topbar захардкожено `"Admin" / "Administrator"`, потому что `LoginResponse` не отдаёт имя/роль. Нужно:

```json
GET /api/auth/me
200 OK
{
  "id": "guid",
  "firstName": "Javohir",
  "lastName": "...",
  "roleName": "Admin",
  "avatarUrl": null
}
```

Фронт: `entities/auth`, дёргается один раз при заходе в `AdminLayout`, кладётся в стор рядом с токенами.

---

## 2. `GET /api/dashboard/summary` — главная страница

Это самое важное — сейчас там моки в `entities/dashboard/model/useDashboardSummary.js`.

```
GET /api/dashboard/summary?branchId={guid|null}&date={yyyy-MM-dd}
```

```json
200 OK
{
  "studentsCount": 299,
  "mentorsCount": 31,
  "usersCount": 1158,
  "attendance": { "present": 282, "absent": 19, "late": 13 },
  "groups": [
    { "id": "guid", "name": ".Net February 2026", "studentsCount": 10, "capacity": 10, "absentCount": 0, "lateCount": 0 }
  ],
  "attendanceLog": [
    {
      "id": "guid",
      "fullName": "Qurbonzoda Afrosiyob",
      "reason": "текст причины",
      "roleName": "Mentor",
      "groupName": "Figma April 2026",
      "phone": "177602211, 905500501",
      "commentTime": "2026-05-14T16:08:00Z"
    }
  ],
  "dailyAttendance": [
    { "day": 1, "present": 24, "absent": 3 }
  ]
}
```

Плюс отдельно (селектор веток на дашборде сейчас пустой):

```json
GET /api/branches
200 OK
[ { "id": "guid", "name": "Дефакто" } ]
```

**Опционально, не блокирует основной релиз** — кнопки Export Excel/Word сейчас задизейблены:
```
GET /api/dashboard/attendance/export?format=xlsx|docx&branchId=&date=
→ отдаёт файл (Content-Disposition: attachment)
```

---

## 3. Students — подразделы (сейчас 3 пустые страницы)

`/users` (All students) уже работает как есть. Новые:

```json
GET /api/students?status=graduated
GET /api/students?status=left
```
Ответ — тот же формат, что уже отдаёт `GET /api/users` (переиспользуем существующую модель User + поле `status`/`isActive`, если уже есть — просто фильтруем).

```json
POST /api/enrollments
{ "studentId": "guid", "groupId": "guid", "startDate": "2026-06-01" }
```
Для формы Enroll ещё нужен список групп для выбора — это уже есть (`GET /api/groups`).

---

## 4. `GET/POST/PUT/DELETE /api/rewards`

```json
{
  "id": "guid",
  "studentId": "guid",
  "studentName": "...",
  "title": "Best project of the month",
  "points": 50,
  "awardedAt": "2026-05-01"
}
```

---

## 5. `GET/POST/PUT/DELETE /api/employees`

Отдельно от Users/Mentors — штатные сотрудники (не привязаны к группам).
```json
{ "id": "guid", "firstName": "...", "lastName": "...", "position": "Manager", "phoneNumber": "...", "email": "...", "isActive": true }
```

---

## 6. `GET /api/timetable`

```
GET /api/timetable?branchId=&weekStart=2026-05-11
```
```json
[
  { "id": "guid", "groupId": "guid", "groupName": "...", "dayOfWeek": 1, "startTime": "18:00", "endTime": "19:30", "room": "204" }
]
```

---

## 7. Administration — роли/права

```json
GET /api/roles
[ { "id": "guid", "name": "Admin", "permissions": ["users.write", "courses.write"] } ]

GET /api/permissions
[ "users.write", "users.read", "courses.write", ... ]  // просто список известных прав системы
```

---

## 8. `GET/POST /api/branches`

```json
{ "id": "guid", "name": "Дефакто филиал 1", "address": "...", "phone": "..." }
```
Используется и в дашборд-селекторе (пункт 2), и на отдельной странице Branches.

---

## 9. `GET/POST /api/sms-mailings`

```json
GET /api/sms-mailings
[ { "id": "guid", "text": "...", "audience": "all|group|student", "targetId": "guid|null", "sentAt": "2026-05-01T10:00:00Z", "recipientsCount": 120 } ]

POST /api/sms-mailings
{ "text": "...", "audience": "group", "targetId": "guid" }
```

---

## 10. Accounting

```json
GET /api/payments?studentId=
[ { "id": "guid", "studentId": "guid", "amount": 500, "currency": "TJS", "paidAt": "2026-05-10", "method": "cash|card|transfer" } ]

GET /api/invoices?studentId=
[ { "id": "guid", "studentId": "guid", "amount": 500, "dueDate": "2026-05-20", "status": "paid|pending|overdue" } ]
```

---

## Порядок по важности (мой совет)

1. **`GET /auth/me`** — 5 минут работы, а Topbar сразу перестанет врать про "Admin"
2. **`GET /dashboard/summary` + `GET /branches`** — это главная страница, без неё всё остальное вторично
3. Students (graduated/left/enroll) — переиспользует то, что уже есть у Users/Groups
4. Дальше по своему усмотрению — Rewards/Employees/TimeTable/Administration/Branches/SMS/Accounting, они друг от друга не зависят, можно любой порядок

Как пришлёшь готовые эндпоинты — говори какой конкретно, я сразу меняю мок-хук/заглушку на реальный `useQuery`, страницу переписывать не придётся (у всех уже есть точка подключения).

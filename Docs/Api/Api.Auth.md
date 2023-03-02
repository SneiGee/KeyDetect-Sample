# KeyDetect App API

- [KeyDetect App API](#keydetect-app-api)
  - [Authentication Table](#authentication-table)
    - [Register](#register)
    - [Register Request](#register-request)
    - [Register Response](#register-response)
    - [Login](#login)
    - [Login Request](#login-request)
    - [Login Response](#login-response)

## Authentication Table

### Register

```js
POST {{host}}/auth/register
```

### Register Request

```json
{
    "firstName":"Cesar",
    "lastName":"Espitia",
    "occupation":"Keyboardist",
    "email":"livecesar@gmail.com",
    "password":"ThePassword"
}
```

### Register Response

```js
200 OK
```

```json
{
    "id":"guid  like id kind of type",
    "firstName":"Cesar",
    "lastName":"Espitia",
    "occupation":"Keyboardist",
    "email":"livecesar@gmail.com",
    "token":"TheToken"
}
```

### Login

```js
POST {{host}}/auth/Login
```

### Login Request

```json
{
    "email":"livecesar@gmail.com",
    "password":"ThePassword"
}
```

### Login Response

```js
200 OK
```

```json
{
    "id":"guid  like id kind of type",
    "firstName":"Cesar",
    "lastName":"Espitia",
    "occupation":"Keyboardist",
    "email":"livecesar@gmail.com",
    "token":"TheToken"
}
```

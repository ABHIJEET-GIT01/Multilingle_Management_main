# WebAPI NOVO - Sample Working Payloads

This document contains sample working payloads for all API endpoints in the WebAPI NOVO Assignment project.

## Table of Contents

- [Authentication Controller](#authentication-controller)
- [Users Controller](#users-controller)
- [Roles Controller](#roles-controller)
- [Forms Controller](#forms-controller)
- [Response Formats](#response-formats)
- [Error Responses](#error-responses)

---

## Authentication Controller

**Base URL**: `http://localhost:5000/api/auth`

### 1. Login

**Endpoint**: `POST /api/auth/login`

**Description**: Authenticate user and receive access and refresh tokens

**Authentication**: None (Public)

**Request Body**:
```json
{
  "email": "admin@novo.com",
  "password": "Admin@123456"
}
```

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "Login successful",
  "data": {
    "userId": "12345678-1234-1234-1234-123456789012",
    "username": "admin",
    "email": "admin@novo.com",
    "firstName": "System",
    "lastName": "Administrator",
    "roles": ["Admin"],
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3OC0xMjM0LTEyMzQtMTIzNC0xMjM0NTY3ODkwMTIiLCJlbWFpbCI6ImFkbWluQG5vdm8uY29tIiwibmJmIjoxNjc0NDAwMDAwLCJleHAiOjE2NzQ0MDA5MDB9.signature",
    "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3OC0xMjM0LTEyMzQtMTIzNC0xMjM0NTY3ODkwMTIiLCJleHAiOjE2NzQ0ODYzODB9.signature"
  }
}
```

**Error Response (401 Unauthorized)**:
```json
{
  "success": false,
  "message": "Invalid email or password"
}
```

**Sample cURL**:
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@novo.com",
    "password": "Admin@123456"
  }'
```

---

### 2. Refresh Token

**Endpoint**: `POST /api/auth/refresh`

**Description**: Get a new access token using the refresh token

**Authentication**: None (Public)

**Request Body**:
```json
{
  "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3OC0xMjM0LTEyMzQtMTIzNC0xMjM0NTY3ODkwMTIiLCJleHAiOjE2NzQ0ODYzODB9.signature"
}
```

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "Token refreshed successfully",
  "data": {
    "userId": "12345678-1234-1234-1234-123456789012",
    "username": "admin",
    "email": "admin@novo.com",
    "firstName": "System",
    "lastName": "Administrator",
    "roles": ["Admin"],
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3OC0xMjM0LTEyMzQtMTIzNC0xMjM0NTY3ODkwMTIiLCJlbWFpbCI6ImFkbWluQG5vdm8uY29tIiwibmJmIjoxNjc0NDAwMDAwLCJleHAiOjE2NzQ0MDA5MDB9.signature",
    "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3OC0xMjM0LTEyMzQtMTIzNC0xMjM0NTY3ODkwMTIiLCJleHAiOjE2NzQ0ODYzODB9.signature"
  }
}
```

**Error Response (401 Unauthorized)**:
```json
{
  "success": false,
  "message": "Invalid or expired refresh token"
}
```

**Sample cURL**:
```bash
curl -X POST http://localhost:5000/api/auth/refresh \
  -H "Content-Type: application/json" \
  -d '{
    "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }'
```

---

### 3. Revoke Token

**Endpoint**: `POST /api/auth/revoke`

**Description**: Revoke a refresh token to invalidate it

**Authentication**: Not required

**Request Body**:
```json
{
  "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3OC0xMjM0LTEyMzQtMTIzNC0xMjM0NTY3ODkwMTIiLCJleHAiOjE2NzQ0ODYzODB9.signature"
}
```

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "Token revoked successfully"
}
```

**Error Response (200 OK - with failure)**:
```json
{
  "success": false,
  "message": "Failed to revoke token"
}
```

**Sample cURL**:
```bash
curl -X POST http://localhost:5000/api/auth/revoke \
  -H "Content-Type: application/json" \
  -d '{
    "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }'
```

---

## Users Controller

**Base URL**: `http://localhost:5000/api/users`

**Authentication**: JWT Token Required (except Create)

### 1. Get All Users

**Endpoint**: `GET /api/users?pageNumber=1&pageSize=10`

**Description**: Retrieve paginated list of all users

**Authentication**: Required

**Query Parameters**:
- `pageNumber` (optional, default: 1): Page number for pagination
- `pageSize` (optional, default: 10): Number of records per page

**Headers**:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
Accept-Language: en (or hi, mr for localization)
```

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "Users loaded successfully",
  "data": [
    {
      "id": "12345678-1234-1234-1234-123456789012",
      "username": "admin",
      "email": "admin@novo.com",
      "firstName": "System",
      "lastName": "Administrator",
      "roles": ["Admin"],
      "createdDate": "2024-01-15T10:30:00Z",
      "updatedDate": "2024-01-15T10:30:00Z"
    },
    {
      "id": "87654321-4321-4321-4321-210987654321",
      "username": "john.doe",
      "email": "john@example.com",
      "firstName": "John",
      "lastName": "Doe",
      "roles": ["User"],
      "createdDate": "2024-01-16T14:20:00Z",
      "updatedDate": "2024-01-16T14:20:00Z"
    }
  ],
  "totalCount": 2,
  "pageNumber": 1,
  "pageSize": 10
}
```

**Sample cURL**:
```bash
curl -X GET "http://localhost:5000/api/users?pageNumber=1&pageSize=10" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json"
```

---

### 2. Get User By ID

**Endpoint**: `GET /api/users/{id}`

**Description**: Retrieve details of a specific user by ID

**Authentication**: Required

**Path Parameters**:
- `id`: User ID (GUID)

**Headers**:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
```

**Example URL**: `GET /api/users/12345678-1234-1234-1234-123456789012`

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "Users loaded successfully",
  "data": {
    "id": "12345678-1234-1234-1234-123456789012",
    "username": "admin",
    "email": "admin@novo.com",
    "firstName": "System",
    "lastName": "Administrator",
    "roles": ["Admin"],
    "createdDate": "2024-01-15T10:30:00Z",
    "updatedDate": "2024-01-15T10:30:00Z"
  }
}
```

**Error Response (404 Not Found)**:
```json
{
  "success": false,
  "message": "User not found"
}
```

**Sample cURL**:
```bash
curl -X GET http://localhost:5000/api/users/12345678-1234-1234-1234-123456789012 \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json"
```

---

### 3. Create User

**Endpoint**: `POST /api/users`

**Description**: Create a new user account

**Authentication**: Not required

**Request Body**:
```json
{
  "username": "john.doe",
  "email": "john@example.com",
  "password": "SecurePassword@123",
  "firstName": "John",
  "lastName": "Doe",
  "roleIds": []
}
```

**Alternative Request (with roles)**:
```json
{
  "username": "jane.smith",
  "email": "jane@example.com",
  "password": "StrongPass@2024",
  "firstName": "Jane",
  "lastName": "Smith",
  "roleIds": ["87654321-4321-4321-4321-210987654321"]
}
```

**Success Response (201 Created)**:
```json
{
  "success": true,
  "message": "User created successfully",
  "data": {
    "id": "87654321-4321-4321-4321-210987654321",
    "username": "john.doe",
    "email": "john@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "roles": [],
    "createdDate": "2024-01-16T14:20:00Z",
    "updatedDate": "2024-01-16T14:20:00Z"
  }
}
```

**Error Response (400 Bad Request)**:
```json
{
  "success": false,
  "message": "Validation failed",
  "errors": [
    "Email is already registered",
    "Username is already taken"
  ]
}
```

**Validation Rules**:
- `username`: 3-20 characters, alphanumeric with underscores
- `email`: Valid email format, must be unique
- `password`: Minimum 8 characters, must contain uppercase, lowercase, digit, and special character
- `firstName`, `lastName`: Optional, max 100 characters each
- `roleIds`: Optional array of role GUIDs

**Sample cURL**:
```bash
curl -X POST http://localhost:5000/api/users \
  -H "Content-Type: application/json" \
  -d '{
    "username": "john.doe",
    "email": "john@example.com",
    "password": "SecurePassword@123",
    "firstName": "John",
    "lastName": "Doe",
    "roleIds": []
  }'
```

---

### 4. Update User

**Endpoint**: `PUT /api/users/{id}`

**Description**: Update an existing user (Admin only)

**Authentication**: Required (Admin role)

**Path Parameters**:
- `id`: User ID (GUID)

**Headers**:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
```

**Request Body**:
```json
{
  "username": "john.doe.updated",
  "email": "john.updated@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "roleIds": ["87654321-4321-4321-4321-210987654321"]
}
```

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "User updated successfully",
  "data": {
    "id": "87654321-4321-4321-4321-210987654321",
    "username": "john.doe.updated",
    "email": "john.updated@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "roles": ["User"],
    "createdDate": "2024-01-16T14:20:00Z",
    "updatedDate": "2024-01-16T15:45:00Z"
  }
}
```

**Error Response (403 Forbidden)**:
```json
{
  "success": false,
  "message": "You do not have permission to perform this action"
}
```

**Sample cURL**:
```bash
curl -X PUT http://localhost:5000/api/users/87654321-4321-4321-4321-210987654321 \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -d '{
    "username": "john.doe.updated",
    "email": "john.updated@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "roleIds": ["87654321-4321-4321-4321-210987654321"]
  }'
```

---

### 5. Delete User

**Endpoint**: `DELETE /api/users/{id}`

**Description**: Delete a user account (Admin only)

**Authentication**: Required (Admin role)

**Path Parameters**:
- `id`: User ID (GUID)

**Headers**:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
```

**Example URL**: `DELETE /api/users/87654321-4321-4321-4321-210987654321`

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "User deleted successfully"
}
```

**Error Response (404 Not Found)**:
```json
{
  "success": false,
  "message": "User not found"
}
```

**Error Response (403 Forbidden)**:
```json
{
  "success": false,
  "message": "You do not have permission to perform this action"
}
```

**Sample cURL**:
```bash
curl -X DELETE http://localhost:5000/api/users/87654321-4321-4321-4321-210987654321 \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json"
```

---

## Roles Controller

**Base URL**: `http://localhost:5000/api/roles`

**Authentication**: JWT Token Required for some endpoints

### 1. Get All Roles

**Endpoint**: `GET /api/roles?pageNumber=1&pageSize=10`

**Description**: Retrieve paginated list of all roles

**Authentication**: Not strictly required (but can be used)

**Query Parameters**:
- `pageNumber` (optional, default: 1): Page number for pagination
- `pageSize` (optional, default: 10): Number of records per page

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "Roles loaded successfully",
  "data": [
    {
      "id": "11111111-1111-1111-1111-111111111111",
      "name": "Admin",
      "description": "Administrator with full system access",
      "createdDate": "2024-01-15T10:30:00Z"
    },
    {
      "id": "22222222-2222-2222-2222-222222222222",
      "name": "User",
      "description": "Regular user with limited permissions",
      "createdDate": "2024-01-15T10:30:00Z"
    }
  ],
  "totalCount": 2,
  "pageNumber": 1,
  "pageSize": 10
}
```

**Sample cURL**:
```bash
curl -X GET "http://localhost:5000/api/roles?pageNumber=1&pageSize=10" \
  -H "Content-Type: application/json"
```

---

### 2. Get Role By ID

**Endpoint**: `GET /api/roles/{id}`

**Description**: Retrieve details of a specific role by ID

**Authentication**: Optional

**Path Parameters**:
- `id`: Role ID (GUID)

**Example URL**: `GET /api/roles/11111111-1111-1111-1111-111111111111`

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "Roles loaded successfully",
  "data": {
    "id": "11111111-1111-1111-1111-111111111111",
    "name": "Admin",
    "description": "Administrator with full system access",
    "createdDate": "2024-01-15T10:30:00Z"
  }
}
```

**Error Response (404 Not Found)**:
```json
{
  "success": false,
  "message": "Role not found"
}
```

**Sample cURL**:
```bash
curl -X GET http://localhost:5000/api/roles/11111111-1111-1111-1111-111111111111 \
  -H "Content-Type: application/json"
```

---

### 3. Create Role

**Endpoint**: `POST /api/roles`

**Description**: Create a new role (Admin only)

**Authentication**: Required (Admin role)

**Headers**:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
```

**Request Body**:
```json
{
  "name": "Manager",
  "description": "Manager with elevated permissions"
}
```

**Alternative Request (minimal)**:
```json
{
  "name": "Editor",
  "description": null
}
```

**Success Response (201 Created)**:
```json
{
  "success": true,
  "message": "Role created successfully",
  "data": {
    "id": "33333333-3333-3333-3333-333333333333",
    "name": "Manager",
    "description": "Manager with elevated permissions",
    "createdDate": "2024-01-16T15:45:00Z"
  }
}
```

**Error Response (400 Bad Request)**:
```json
{
  "success": false,
  "message": "Validation failed",
  "errors": [
    "Name is required",
    "Name must be between 3 and 50 characters"
  ]
}
```

**Error Response (409 Conflict)**:
```json
{
  "success": false,
  "message": "Validation failed"
}
```

**Validation Rules**:
- `name`: 3-50 characters, must be unique
- `description`: Optional, max 500 characters

**Sample cURL**:
```bash
curl -X POST http://localhost:5000/api/roles \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Manager",
    "description": "Manager with elevated permissions"
  }'
```

---

### 4. Update Role

**Endpoint**: `PUT /api/roles/{id}`

**Description**: Update an existing role (Admin only)

**Authentication**: Required (Admin role)

**Path Parameters**:
- `id`: Role ID (GUID)

**Headers**:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
```

**Request Body**:
```json
{
  "name": "Manager",
  "description": "Updated manager role with full permissions"
}
```

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "Role updated successfully",
  "data": {
    "id": "33333333-3333-3333-3333-333333333333",
    "name": "Manager",
    "description": "Updated manager role with full permissions",
    "createdDate": "2024-01-16T15:45:00Z"
  }
}
```

**Error Response (409 Conflict)**:
```json
{
  "success": false,
  "message": "Role not found"
}
```

**Sample cURL**:
```bash
curl -X PUT http://localhost:5000/api/roles/33333333-3333-3333-3333-333333333333 \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Manager",
    "description": "Updated manager role with full permissions"
  }'
```

---

### 5. Delete Role

**Endpoint**: `DELETE /api/roles/{id}`

**Description**: Delete a role (Admin only)

**Authentication**: Required (Admin role)

**Path Parameters**:
- `id`: Role ID (GUID)

**Headers**:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
```

**Example URL**: `DELETE /api/roles/33333333-3333-3333-3333-333333333333`

**Success Response (200 OK)**:
```json
{
  "success": true,
  "message": "Role deleted successfully"
}
```

**Error Response (404 Not Found)**:
```json
{
  "success": false,
  "message": "Role not found"
}
```

**Sample cURL**:
```bash
curl -X DELETE http://localhost:5000/api/roles/33333333-3333-3333-3333-333333333333 \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json"
```

---

## Forms Controller

**Base URL**: `http://localhost:5000/api/forms`

**Authentication**: Required (but can be disabled)

### 1. Get Login Form

**Endpoint**: `GET /api/forms/login-form?language=en`

**Description**: Retrieve login form structure for specified language

**Authentication**: Required

**Query Parameters**:
- `language` (optional via header Accept-Language): Form language (en, hi, mr)

**Headers**:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
Accept-Language: en
```

**Success Response (200 OK)**:
```json
{
  "formName": "login",
  "formTitle": "Login",
  "fields": [
    {
      "fieldName": "email",
      "fieldType": "email",
      "label": "Email Address",
      "placeholder": "Enter your email",
      "required": true,
      "validationRules": {
        "pattern": "^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$",
        "minLength": 5,
        "maxLength": 256
      }
    },
    {
      "fieldName": "password",
      "fieldType": "password",
      "label": "Password",
      "placeholder": "Enter your password",
      "required": true,
      "validationRules": {
        "minLength": 8
      }
    }
  ],
  "language": "en"
}
```

**Alternative Response (Hindi - Accept-Language: hi)**:
```json
{
  "formName": "login",
  "formTitle": "लॉगिन",
  "fields": [
    {
      "fieldName": "email",
      "fieldType": "email",
      "label": "ईमेल पता",
      "placeholder": "अपना ईमेल दर्ज करें",
      "required": true,
      "validationRules": {
        "pattern": "^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$",
        "minLength": 5,
        "maxLength": 256
      }
    },
    {
      "fieldName": "password",
      "fieldType": "password",
      "label": "पासवर्ड",
      "placeholder": "अपना पासवर्ड दर्ज करें",
      "required": true,
      "validationRules": {
        "minLength": 8
      }
    }
  ],
  "language": "hi"
}
```

**Sample cURL (English)**:
```bash
curl -X GET "http://localhost:5000/api/forms/login-form" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -H "Accept-Language: en"
```

**Sample cURL (Hindi)**:
```bash
curl -X GET "http://localhost:5000/api/forms/login-form" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -H "Accept-Language: hi"
```

---

### 2. Get Register Form

**Endpoint**: `GET /api/forms/register-form?language=en`

**Description**: Retrieve registration form structure for specified language

**Authentication**: Required

**Query Parameters**:
- `language` (optional via header Accept-Language): Form language (en, hi, mr)

**Headers**:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
Accept-Language: en
```

**Success Response (200 OK)**:
```json
{
  "formName": "register",
  "formTitle": "Register",
  "fields": [
    {
      "fieldName": "username",
      "fieldType": "text",
      "label": "Username",
      "placeholder": "Choose a username",
      "required": true,
      "validationRules": {
        "minLength": 3,
        "maxLength": 20,
        "pattern": "^[a-zA-Z0-9_]+$"
      }
    },
    {
      "fieldName": "email",
      "fieldType": "email",
      "label": "Email Address",
      "placeholder": "Enter your email",
      "required": true,
      "validationRules": {
        "pattern": "^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$",
        "minLength": 5,
        "maxLength": 256
      }
    },
    {
      "fieldName": "password",
      "fieldType": "password",
      "label": "Password",
      "placeholder": "Enter a strong password",
      "required": true,
      "validationRules": {
        "minLength": 8,
        "pattern": "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"
      }
    },
    {
      "fieldName": "firstName",
      "fieldType": "text",
      "label": "First Name",
      "placeholder": "Enter your first name",
      "required": false,
      "validationRules": {
        "maxLength": 100
      }
    },
    {
      "fieldName": "lastName",
      "fieldType": "text",
      "label": "Last Name",
      "placeholder": "Enter your last name",
      "required": false,
      "validationRules": {
        "maxLength": 100
      }
    }
  ],
  "language": "en"
}
```

**Alternative Response (Marathi - Accept-Language: mr)**:
```json
{
  "formName": "register",
  "formTitle": "नोंदणी",
  "fields": [
    {
      "fieldName": "username",
      "fieldType": "text",
      "label": "वापरकर्तानाव",
      "placeholder": "वापरकर्तानाव निवडा",
      "required": true,
      "validationRules": {
        "minLength": 3,
        "maxLength": 20,
        "pattern": "^[a-zA-Z0-9_]+$"
      }
    },
    {
      "fieldName": "email",
      "fieldType": "email",
      "label": "ईमेल पता",
      "placeholder": "आपला ईमेल प्रविष्ट करा",
      "required": true,
      "validationRules": {
        "pattern": "^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$",
        "minLength": 5,
        "maxLength": 256
      }
    }
  ],
  "language": "mr"
}
```

**Sample cURL (English)**:
```bash
curl -X GET "http://localhost:5000/api/forms/register-form" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -H "Accept-Language: en"
```

**Sample cURL (Marathi)**:
```bash
curl -X GET "http://localhost:5000/api/forms/register-form" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -H "Accept-Language: mr"
```

---

## Response Formats

### Success Response with Data

```json
{
  "success": true,
  "message": "Operation completed successfully",
  "data": {
    "id": "12345678-1234-1234-1234-123456789012",
    "name": "Example"
  }
}
```

### Success Response with List

```json
{
  "success": true,
  "message": "Operation completed successfully",
  "data": [
    { "id": "1", "name": "Item 1" },
    { "id": "2", "name": "Item 2" }
  ],
  "totalCount": 2,
  "pageNumber": 1,
  "pageSize": 10
}
```

### Success Response without Data

```json
{
  "success": true,
  "message": "Operation completed successfully"
}
```

### Error Response

```json
{
  "success": false,
  "message": "Error description",
  "errors": [
    "Validation error 1",
    "Validation error 2"
  ]
}
```

---

## Error Responses

### 400 Bad Request
```json
{
  "success": false,
  "message": "Validation failed",
  "errors": [
    "Field is required",
    "Invalid format"
  ]
}
```

### 401 Unauthorized
```json
{
  "success": false,
  "message": "Invalid credentials or token expired"
}
```

### 403 Forbidden
```json
{
  "success": false,
  "message": "You do not have permission to perform this action"
}
```

### 404 Not Found
```json
{
  "success": false,
  "message": "Resource not found"
}
```

### 409 Conflict
```json
{
  "success": false,
  "message": "Resource already exists or conflict occurred"
}
```

### 500 Internal Server Error
```json
{
  "success": false,
  "message": "An unexpected error occurred"
}
```

---

## Testing Workflow Example

### Step 1: Login
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@novo.com",
    "password": "Admin@123456"
  }'
```

### Step 2: Copy the accessToken from response

### Step 3: Use token for authenticated requests
```bash
curl -X GET "http://localhost:5000/api/users?pageNumber=1&pageSize=10" \
  -H "Authorization: Bearer YOUR_ACCESS_TOKEN_HERE" \
  -H "Content-Type: application/json"
```

### Step 4: Create a new user
```bash
curl -X POST http://localhost:5000/api/users \
  -H "Content-Type: application/json" \
  -d '{
    "username": "testuser",
    "email": "test@example.com",
    "password": "TestPass@123",
    "firstName": "Test",
    "lastName": "User",
    "roleIds": []
  }'
```

### Step 5: Refresh token when expired
```bash
curl -X POST http://localhost:5000/api/auth/refresh \
  -H "Content-Type: application/json" \
  -d '{
    "refreshToken": "YOUR_REFRESH_TOKEN_HERE"
  }'
```

---

## Common Validation Rules

### Username
- Length: 3-20 characters
- Pattern: Alphanumeric and underscores only
- Must be unique

### Email
- Valid email format: `user@domain.com`
- Max length: 256 characters
- Must be unique

### Password
- Minimum 8 characters
- Must contain uppercase letter (A-Z)
- Must contain lowercase letter (a-z)
- Must contain number (0-9)
- Must contain special character (@$!%*?&)

### Role Name
- Length: 3-50 characters
- Must be unique

### Role Description
- Maximum 500 characters
- Optional

---

## Language Support

Supported languages for forms and messages:
- `en` - English
- `hi` - Hindi (हिन्दी)
- `mr` - Marathi (मराठी)

Use `Accept-Language` header to specify desired language:
```
Accept-Language: en
Accept-Language: hi
Accept-Language: mr
```

If not specified, defaults to English (en).

---

**Last Updated**: 2024
**Framework**: ASP.NET Core 10

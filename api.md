# Swagger

```sh
curl 'http://localhost:5000/weatherforecast'
curl 'http://localhost:5000/swagger'
```

# Register

```sh
curl 'http://localhost:5000/api/v1/Account/register' \
  -X POST \
  -H 'Content-Type: application/json' \
  --data-raw '{"email":"nguyentrucxjnh@gmail.com","password":"Aa@123456","confirmPassword":"Aa@123456"}'
```

# Login

```sh
curl 'http://localhost:5000/api/v1/Account/login' \
  -X POST \
  -H 'Content-Type: application/json' \
  --data-raw '{"email":"nguyentrucxjnh@gmail.com","password":"Aa@123456","rememberMe":false}'
```

# Refresh token

```sh
curl 'http://localhost:5000/api/v1/Account/refresh' \
  -X POST \
  -H 'Content-Type: application/json' \
  --data-raw '{"accessToken":"","refreshToken":""}'
```

# Get Current user

```sh
curl 'http://localhost:5000/api/v1/Account/current' \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer ACCESS_TOKEN'
```

# Get Customers

```sh
curl 'http://localhost:5000/api/v1/Customer' \
  -H 'Content-Type: application/json' \
  -H 'Authorization: Bearer ACCESS_TOKEN'
```

# Create Customer

# Update Customer

# Delete Customer

# Get a Customer by id

# Get Customer History

# Get Customers By Pagination

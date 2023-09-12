# Create HTTPS Certficate 

## Windows 
```ps1
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\coffeeapi.pfx -p pa55w0rd!

dotnet dev-certs https --trust

dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\teaapi.pfx -p pa55w0rd!

dotnet dev-certs https --trust
```

## MacOs

```bash
dotnet dev-certs https -v -ep ${HOME}/.aspnet/https/coffeeapi.pfx -p 1234@tech

dotnet dev-certs https --trust

dotnet dev-certs https -v -ep ${HOME}/.aspnet/https/teaapi.pfx -p 1234@tech

dotnet dev-certs https --trust
```

# .NET User Secrets
For each project you will initialize user secrets.
```bash 
dotnet user-secrets init
```

For the TeaAPI you will set the following user secrets
```bash 
dotnet user-secrets set "Kestrel:Certificates:Development:Password" "pa55wOrd!"
```

## View your user secrets 

On Windows
C:\Users\luisalvar\AppData\Roaming\Microsoft\UserSectets

On MacOs
\Users\luisalvar\\.microsoft\usersecrets\

## Set the 
Run for both projects
dotnet user-secrets set "Kestrel:Certificates:Development:Password" []


## Docker Compare
```yaml
version: '3'
services:
  coffeeapi:
    build: coffeeapi/
    ports:
      - "8080:80"
      - "8081:443"
    environment:
      ASPNETCORE_URLS: "https://+;http://+"
      ASPNETCORE_HTTPS_PORT: "8081"
      ASPNETCORE_ENVIRONMENT: "Development"
    volumes:
      - ${HOME}/.microsoft/usersecrets/:/root/.microsoft/usersecrets
      - ${HOME}/.aspnet/https:/root/.aspnet/https/
```
For MacOs: 
- User Secrets is located in ${HOME}/.microsoft/usersecrets/
- For Https credificates this is located in ${HOME}/.aspnet/https

We want to copy these items into docker under root/.microsoft and root/.aspnet

### Docker Run 
Use docker-compose up --build 

docker-compose down

### Envoy 

```yaml
admin:
  access_log_path: /tmp/admin_access.log 
  address:
    socket_address:
      protocol: TCP
      address: 0.0.0.0
      port_value: 9901
```
A special case listerner, admin interface on port 9901

```yaml
static_resources:
  listeners:
  - name : listerner_0
    address:
      socket_address:
        protocol: TCP 
        address: 0.0.0.0
        port_vaule: 10000
  filter_chains:
  - filters:
    - name: envoy.filters.network.http_connection_manager
      type_config:


```
cd envoy 
docker build -t envoygateway .
docker run -p 9901:9901 -p 10000:10000 envoygateway

## Https Config

```bash 
openssl req -config https.config -new -out csr.pem
```

If you get prompted to enter a value for localhost just type localhost

```bash 
openssl x509 -req -days 365 -extfile https.config -extensions v3_req -in csr.pem -signkey key.pem -out https.crt
```

## Add to Docker Compose
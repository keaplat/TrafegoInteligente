# Projeto TrafegoInteligenteFIAP

Aplicação ASP.NET Core containerizada com Docker e orquestrada com Docker Compose.


## Pré-requisitos

Antes de começar, certifique-se de que você tem os seguintes softwares instalados:

- **Git**
- **Docker** e **Docker Compose**

## Como Clonar o Repositório

Abra o terminal e execute os seguintes comandos:

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio
```
Como Executar o Projeto
Executando com Docker Compose
Iniciar os serviços:

```bash
Copiar código
docker-compose up -d
```
O parâmetro -d executa os contêineres em segundo plano (modo desacoplado).

Verificar os contêineres em execução:

```bash
docker-compose ps
```
A aplicação estará acessível em:
```bash
URL: http://localhost:8080
```
Abra um navegador e navegue até o endereço acima para acessar a aplicação TrafegoInteligenteFIAP.

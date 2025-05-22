# AWS Backup Tracker

[🇺🇸 English](README.en.md)

Este projeto é uma solução para backup automatizado de buckets S3 da AWS, com API em .NET 8, script de backup em Bash e orquestração via Docker Compose.

## Funcionalidades
- Backup diário automático de um bucket S3 para o container
- API REST para consultar status e histórico dos backups
- Logs detalhados de cada execução

## Requisitos
- Docker
- Docker Compose
- Conta AWS com credenciais válidas

## Como usar

1. **Clone o repositório:**
   ```sh
   git clone <url-do-repo>
   cd aws-backup-tracker
   ```

2. **Configure as variáveis de ambiente AWS**
   Edite o arquivo `docker-compose.yml` e preencha:
   ```yaml
   environment:
     - AWS_ACCESS_KEY_ID=seu_access_key_id
     - AWS_SECRET_ACCESS_KEY=sua_secret_access_key
     - AWS_DEFAULT_REGION=sua_regiao
   ```

3. **Ajuste o nome do bucket no script**
   Edite `api/scripts/backup.sh` e defina o nome do seu bucket em `BUCKET_NAME`.

4. **Suba a aplicação:**
   ```sh
   docker compose up --build
   ```

5. **Acesse a API:**
   - Histórico: [http://localhost:5110/backup/history](http://localhost:5110/backup/history)
   - Status: [http://localhost:5110/backup/status](http://localhost:5110/backup/status)

## Estrutura do Projeto
- `api/` - Código da API .NET 8
- `api/scripts/backup.sh` - Script de backup
- `s3_backup/` - Diretório de backups e logs (persistente)

## Observações
- O backup roda automaticamente ao iniciar o container e diariamente via cron.
- O frontend pode ser adicionado futuramente ao `docker-compose.yml`.

---
[🇺🇸 English](README.en.md) 
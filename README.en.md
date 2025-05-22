# AWS Backup Tracker

[🇧🇷 Português](README.md)

This project is a solution for automated backup of AWS S3 buckets, featuring a .NET 8 API, Bash backup script, and orchestration via Docker Compose.

## Features
- Automatic daily backup of an S3 bucket to the container
- REST API to check backup status and history
- Detailed logs for each execution

## Requirements
- Docker
- Docker Compose
- AWS account with valid credentials

## How to use

1. **Clone the repository:**
   ```sh
   git clone <repo-url>
   cd aws-backup-tracker
   ```

2. **Configure AWS environment variables**
   Edit the `docker-compose.yml` file and fill in:
   ```yaml
   environment:
     - AWS_ACCESS_KEY_ID=your_access_key_id
     - AWS_SECRET_ACCESS_KEY=your_secret_access_key
     - AWS_DEFAULT_REGION=your_region
   ```

3. **Set your bucket name in the script**
   Edit `api/scripts/backup.sh` and set your bucket name in `BUCKET_NAME`.

4. **Start the application:**
   ```sh
   docker compose up --build
   ```

5. **Access the API:**
   - History: [http://localhost:5110/backup/history](http://localhost:5110/backup/history)
   - Status: [http://localhost:5110/backup/status](http://localhost:5110/backup/status)

## Project Structure
- `api/` - .NET 8 API code
- `api/scripts/backup.sh` - Backup script
- `s3_backup/` - Backups and logs directory (persistent)

## Notes
- The backup runs automatically on container startup and daily via cron.
- A frontend can be added later to the `docker-compose.yml`.

---
[🇧🇷 Português](README.md) 
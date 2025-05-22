FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN apt-get update && \
    apt-get install -y cron bash awscli jq && \
    rm -rf /var/lib/apt/lists/*

COPY --from=build /app .

COPY scripts/backup.sh /usr/local/bin/backup.sh
RUN chmod +x /usr/local/bin/backup.sh

COPY crontab /etc/cron.d/backup-cron
RUN chmod 0644 /etc/cron.d/backup-cron && crontab /etc/cron.d/backup-cron

RUN mkdir -p /root/s3_backup/files /root/s3_backup/logs

COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

CMD ["/entrypoint.sh"] 

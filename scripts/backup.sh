#!/bin/bash

set -e

BUCKET_NAME="railima-aws-backup-tracker"
BACKUP_DIR="$HOME/s3_backup/files"
LOG_DIR="$HOME/s3_backup/logs"

verify_directory() {
    local dir=$1
    if [ ! -d "$dir" ]; then
        mkdir -p "$dir"
    fi
}

check_aws_credentials() {
    if ! aws sts --profile formacaoaws get-caller-identity >/dev/null 2>&1; then
        echo "Credenciais AWS inválidas"
        return 1
    fi
}

sync_files() {
    local output
    output=$(aws s3 --profile formacaoaws sync "s3://$BUCKET_NAME" "$BACKUP_DIR" --delete 2>&1)
    echo "$output"
}

save_log() {
    local status=$1
    local duration=$2
    local output=$3

    local timestamp
    timestamp=$(date +"%Y-%m-%dT%H:%M:%S")

    verify_directory "$LOG_DIR"

    local log_file="$LOG_DIR/log_$(date +"%Y-%m-%dT%H-%M-%S").json"

    cat <<EOF > "$log_file"
{
  "timestamp": "$timestamp",
  "status": "$status",
  "duration": "$duration",
  "output": $(echo "$output" | jq -Rs .)
}
EOF
}

start=$(date +%s)

verify_directory "$BACKUP_DIR"
verify_directory "$LOG_DIR"

if ! check_aws_credentials; then
    duration="0s"
    save_log "error" "$duration" "Erro: credenciais AWS inválidas"
    exit 1
fi

echo "Iniciando backup do bucket s3://$BUCKET_NAME..."

if output=$(sync_files); then
    end=$(date +%s)
    duration="$((end - start))s"
    echo "Backup concluído com sucesso"
    save_log "success" "$duration" "$output"
else
    end=$(date +%s)
    duration="$((end - start))s"
    echo "Erro durante o backup"
    save_log "error" "$duration" "$output"
    exit 1
fi

exit 0
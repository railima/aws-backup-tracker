#!/bin/bash
/usr/local/bin/backup.sh
service cron start
exec dotnet AwsBackupTracker.dll
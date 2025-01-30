# How To Run

## Spin up the container

```bash
docker-compose up -d
```

## Open Azure Data Studio

1. Open Azure Data Studio
2. New Connection
3. Use this connection string: `Server=localhost,1435;User Id=sa;Password=YourStrong!Passw0rd;`
4. Optionally, you can set name to the connection and save it for future use
5. Click Connect

## How To Restore Database

```sql
RESTORE FILELISTONLY
FROM DISK = '/backups/Company_SD_Full.bak';

RESTORE DATABASE [Company_SD]
FROM DISK = '/backups/Company_SD_Full.bak'
WITH MOVE 'Company_SD' TO '/var/opt/mssql/data/Company_SD.mdf',
     MOVE 'Company_SD_Log' TO '/var/opt/mssql/data/Company_SD_log.ldf';
```

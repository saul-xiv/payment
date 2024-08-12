FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

RUN apt-get update && \
    apt-get install -y curl apt-transport-https gnupg2 && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql17 mssql-tools unixodbc-dev && \
    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc && \
    /bin/bash -c "source ~/.bashrc"

ENV SA_PASSWORD=ComplexPassword123
ENV ACCEPT_EULA=Y

EXPOSE 1433

#USER mssql

#CMD /bin/bash -c "/opt/mssql/bin/sqlservr && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -Q \"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PaymentDb') BEGIN CREATE DATABASE [PaymentDb]; END\" && wait"
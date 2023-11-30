FROM registry.okd.local/dotnet/sdk:6.0

ENV DOTNET_NUGET_SOURCES=https://nuget.okd.local/repository/api.nuget-group/index.json
ENV DOTNET_RESTORE_SOURCES="${DOTNET_NUGET_SOURCES}"

ENV PROJECT_SOLUTION_PATH="OrcHestrador.WebApi"

# ADICIONADO AUTORIDADE CERTIFICADORA PARA ALPINE, DEBIAN, UBUNTU E DISTRIBUIES DERIVADAS
RUN curl -fsSL https://gitlab.setic.ro.gov.br/publico/ca-trust/-/raw/master/openshift_ca.crt -o /usr/local/share/ca-certificates/openshift_ca.crt
RUN curl -fsSL https://gitlab.setic.ro.gov.br/publico/ca-trust/-/raw/master/portainer_ca.crt -o /usr/local/share/ca-certificates/portainer_ca.crt
RUN update-ca-certificates

# COPY CSPROJ AND RESTORE AS DISTINCT LAYERS
COPY ./ /source

# SETA O DIRETORIO BASE PARA DOTNET RESTORE E PUBLISH
WORKDIR /source/src/"${PROJECT_SOLUTION_PATH}"

# RESTAURAO EXPLCITA PARA PACOTES NUGET
RUN dotnet restore --source "${DOTNET_NUGET_SOURCES}"

# COMPILA O CODIGO PARA IMPLANTAO EM UM SISTEMA DE HOSPEDAGEM
RUN dotnet publish --no-restore -c release -o /app

# SETA O DIRETORIO BASE PARA ENTRYPOINT
WORKDIR /app

EXPOSE 8080 443
ENTRYPOINT dotnet "${PROJECT_SOLUTION_PATH}".dll
	
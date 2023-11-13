#!/bin/bash
set -eu -o pipefail

dotnet restore /TestAccountManagement/TestAccountManagement.csproj
dotnet test /TestAccountManagement/TestAccountManagement.csproj
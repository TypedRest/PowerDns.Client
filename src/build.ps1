Param ([string]$Version = "0.1-debug")
$ErrorActionPreference = "Stop"
pushd $(Split-Path -Path $MyInvocation.MyCommand.Definition -Parent)

dotnet clean "Axoom.Provisioning.PowerDns.sln"
dotnet msbuild /t:Restore /t:Build /p:Configuration=Release /p:Version=$Version "Axoom.Provisioning.PowerDns.sln"
dotnet test --configuration Release --no-build "Axoom.Provisioning.PowerDns.UnitTests/Axoom.Provisioning.PowerDns.UnitTests.csproj"

popd

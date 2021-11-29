﻿Param ($Version = "1.0-dev")
$ErrorActionPreference = "Stop"
pushd $PSScriptRoot

function Run-DotNet {
    ..\0install.ps1 run --batch --version 6.0.. https://apps.0install.net/dotnet/sdk.xml @args
    if ($LASTEXITCODE -ne 0) {throw "Exit Code: $LASTEXITCODE"}
}

if ($env:CI) { $ci = "/p:ContinuousIntegrationBuild=True" }
Run-DotNet msbuild /v:Quiet /t:Restore /t:Build $ci /p:Configuration=Release /p:Version=$Version

popd

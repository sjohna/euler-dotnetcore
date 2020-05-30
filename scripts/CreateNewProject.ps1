[CmdletBinding()]
param 
(
    [Parameter()]
    [string]
    $projectName
)

Push-Location

Set-Location $PSScriptRoot
Set-Location ..

mkdir $projectName

Set-Location $projectName

dotnet new console
dotnet add reference ..\EulerLib\EulerLib.csproj

Set-Location ..

dotnet sln add .\${projectName}\${projectName}.csproj

Pop-Location
properties {
    $Configuration = "Release"
    $WindowsSource = ".\src\Windows"

    $Version = Get-BuildNumberFromCISystem
    if (-not $Version) { $Version = "1.0" }
    $Version = Get-NormalizedVersion $Version
    $InformationalVersion = Get-InformationalVersion $Version

    $Artifacts = ".\dist"
    
    $NuGetExe = "$WindowsSource\.nuget\nuget.exe"
    $NuGetOutput = "$WindowsSource\packages"

    $NUnitRunnersVersion = "2.6.2"
    $NUnitExe = "$NuGetOutput\NUnit.Runners.$NUnitRunnersVersion\tools\nunit-console.exe"
}

include .\psake_ext.ps1

task default -depends Clean,Generate-AssemblyInfo,Compile,Reset-AssemblyInfo,CreateArtifacts,RunUnitTests

task Clean {
    Exec { msbuild /p:Configuration=Debug /t:Clean "$WindowsSource\TrafikverketFarjor.Web.sln" }
    Exec { msbuild /p:Configuration=Release /t:Clean "$WindowsSource\TrafikverketFarjor.Web.sln" }
}

task Generate-AssemblyInfo  {
    Get-ChildItem -Recurse -Filter AssemblyInfo.cs | foreach { Generate-AssemblyInfo `
        -file $_.FullName `
        -title "NastaFarja.se $version" `
        -company "" `
        -product "NastaFarja.se $version" `
        -version $version `
        -informationalVersion $InformationalVersion `
        -copyright "Copyright (c) 42A Consulting, Adam Brengesjo 2012-2013"
    }
}

task Reset-AssemblyInfo {
    Get-ChildItem -Recurse -Filter AssemblyInfo.cs | foreach {
        git checkout HEAD -- $_.FullName
    }
}

task Compile -depends Clean {
    Exec { msbuild /p:Configuration=$Configuration /t:Build "$WindowsSource\TrafikverketFarjor.Web.sln" }
    Exec { msbuild /p:Configuration=$Configuration /t:Package "$WindowsSource\TrafikverketFarjor.Web\TrafikverketFarjor.Web.csproj" }
}

task CreateArtifacts -depends Compile {
    function Invoke-Robocopy([string]$source, [string]$destination) {
        ROBOCOPY /MIR $source $destination
        if ($LastExitCode -gt 5) {
            throw "ROBOCOPY returned with exit code $LastExitCode"
        }
    }

    if (Test-Path "$Artifacts") {
        Remove-Item -Force -Recurse "$Artifacts"
    }

    Invoke-Robocopy "$WindowsSource\TrafikverketFarjor.Tests\bin\$Configuration" "$Artifacts\TrafikverketFarjor.Tests"
    Invoke-Robocopy "$WindowsSource\TrafikverketFarjor.Web\obj\$Configuration\Package\PackageTmp" "$Artifacts\TrafikverketFarjor.Web"
}

task InstallNUnitRunners {
    Exec { & "$NuGetExe" install NUnit.Runners -version "$NUnitRunnersVersion" -o "$NuGetOutput" }
}

task RunUnitTests -depends InstallNUnitRunners {
    Exec { & "$NUnitExe" "$Artifacts\TrafikverketFarjor.Tests\TrafikverketFarjor.Tests.dll" /noresult /nologo /exclude:Webtests }
}

task RunWebApiTests -depends InstallNUnitRunners {
    Exec { & "$NUnitExe" "$Artifacts\TrafikverketFarjor.Tests\TrafikverketFarjor.Tests.dll" /noresult /nologo /include:WebApiTests }
}

task RunWebTests -depends InstallNUnitRunners {
    Exec { & "$NUnitExe" "$Artifacts\TrafikverketFarjor.Tests\TrafikverketFarjor.Tests.dll" /noresult /nologo /include:WebDriverTests }
}

task Deploy {
    Synchronize-FtpFolder "$Artifacts\TrafikverketFarjor.Web"
}

function Synchronize-FtpFolder([string]$Path) {
    function Invoke-WinSCP {
    param(
        [string]$sessionUrl,
        [string]$sourcePath = (Resolve-Path "."),
        [string]$remotePath = "/"
    )
        & ".\tools\WinSCP.com" `
            /console `
            "/script=$(Resolve-Path '.\FtpDeployment.config')" `
            "/log=FtpLog-$((get-date).ToString('yyyyMMddHHmmss')).log" `
            "/parameter" `
            "//" `
            $sessionUrl `
            (Resolve-Path $sourcePath) `
            $remotePath

        if ($LastExitCode -ne 0) {
            throw ".\tools\WinSCP.exe returned with exit code $LastExitCode"
        }
    }

    Invoke-WinSCP `
        -sessionUrl "ftp://${env:WEBDEPLOY_USER}:${env:WEBDEPLOY_PASS}@${env:WEBDEPLOY_HOST}" `
        -sourcePath "$Path" `
        -remotePath "${env:WEBDEPLOY_PATH}"
}
properties {
    $Configuration = "Release"
    $WindowsSource = ".\src\Windows"

    $Artifacts = ".\dist"
    
    $NuGetExe = "$WindowsSource\.nuget\nuget.exe"
    $NuGetOutput = "$WindowsSource\packages"

    $NUnitRunnersVersion = "2.6.2"
    $NUnitExe = "$NuGetOutput\NUnit.Runners.$NUnitRunnersVersion\tools\nunit-console.exe"
}

task default -depends Clean,Compile,CreateArtifacts,RunUnitTests

task Clean {
    Exec { msbuild /p:Configuration=Debug /t:Clean "$WindowsSource\TrafikverketFarjor.Web.sln" }
    Exec { msbuild /p:Configuration=Release /t:Clean "$WindowsSource\TrafikverketFarjor.Web.sln" }
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
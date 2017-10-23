[CmdletBinding()]
param(
    [string] $BuildRunner = $Env:BuildRunner,
    [string] $NuGetPath = $Env:NuGet,
    [string] $MsBuildPath = $Env:MsBuildExe,
    [string] $SourcesPath = $Env:SourcesPath,
    [string] $Configuration = $Env:Configuration,
    [string] $Platform = $Env:Platform,
    [string] $Targets = $Env:Targets,
    [string] $VersionFormat = $Env:VersionFormat,
    [string] $BuildCounter = $Env:BuildCounter,
    [string] $PrereleaseTag = $Env:PrereleaseTag,
    [string] $PackageVersion = $Env:PackageVersion,
    [string] $EnableNugetPackageRestore = $Env:EnableNuGetPackageRestore,    
    [string] $GallioEcho = $Env:GallioEcho,
    [string] $Xunit192Path = $Env:XUnit192Path,
    [string] $Xunit20Path = $Env:XUnit20Path,
    [string] $VsTestConsole = $Env:VsTestConsole,
    [string] $MsTestExe = $Env:MsTestExe,

    [string] $GitPath = $Env:GitPath,
    [string] $GitVersion = $Env:GitVersion,

    # comma-delimeted list of namespace filters to use with dotnet test
    [string] $TestNamespaceFilters = $Env:TestNamespaceFilters
)

function Exit-ScriptOnFailure {
    if($LASTEXITCODE -ne 0) {
        Write-Error 'Script failed'
        exit 1
    }
}

function Clear-Assemblies {
    $bin = Get-ChildItem $SourcesPath -Filter bin -Directory -Recurse -Name
    $obj = Get-ChildItem $SourcesPath -Filter obj -Directory -Recurse -Name
    foreach($dir in ($bin + $obj)) {
        Remove-Item $dir -Force -Recurse
    }
}

Set-Location $SourcesPath

Clear-Assemblies

dotnet restore
Exit-ScriptOnFailure
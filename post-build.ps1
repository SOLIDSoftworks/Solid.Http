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

function Get-TestFilters() {
    $filters = @()
    if($TestNamespaceFilters) {
        $filters = $TestNamespaceFilters.Split(',')
    }

    if(!$filters.Count) {
        $filters += '.Test.'
        $filters += '.Tests.'
    }

    $filters
}

function Invoke-Tests{
    $projects = Get-ChildItem -Path $SourcesPath -Filter *.csproj -Recurse -File -Name
    $filters = Get-TestFilters
    $exits = 0
    foreach($project in $projects) {
        $found = ''
        foreach($filter in $filters) {
            if($project.Contains($filter)) {
                Write-Verbose "$project uses filter '$filter'"
                $found = $filter
                break
            }
        }
        if(!$found) {
            continue
        }
        $p = "$SourcesPath\$project"
        $d = [IO.Path]::GetDirectoryName($p)
        $bin = Get-ChildItem $d -Filter "bin\$Configuration"  -Directory -Recurse -Name
        if(!$bin) {
            Write-Verbose "$d wasn't built. Skipping..."
            continue
        }
        Write-Host "Running tests in $p"
        dotnet test $p --configuration $Configuration --no-build --no-restore --filter "FullyQualifiedName~$found"
        $exits += $LASTEXITCODE
    }
    if($exits -gt 0) {
        Write-Error 'Test run failed'
        $LASTEXITCODE = 1
    }
}

function Invoke-Package{
    $projects = Get-ChildItem -Path $SourcesPath -Filter *.csproj -Recurse -File -Name
    $filters = Get-TestFilters
    $exits = 0
    foreach($project in $projects) {
        $found = ''
        foreach($filter in $filters) {
            if($project.Contains($filter)) {
                $found = $filter
                break
            }
        }
        if($found) {
            Write-Verbose "$project is a test project"
            continue
        }
        $p = "$SourcesPath\$project"
        $d = [IO.Path]::GetDirectoryName($p)
        $bin = Get-ChildItem $d -Filter "bin\$Configuration"  -Directory -Recurse -Name
        if(!$bin) {
            Write-Verbose "$d wasn't built. Skipping..."
            continue
        }
        Write-Host "Packaging $p"
        dotnet pack $p --configuration $Configuration --no-build --no-restore --no-dependencies --version-suffix $PrereleaseTag
        $exits += $LASTEXITCODE
    }
    if($exits -gt 0) {
        Write-Error 'Test run failed'
        $LASTEXITCODE = 1
    }
}

Invoke-Tests
Exit-ScriptOnFailure

Invoke-Package
Exit-ScriptOnFailure
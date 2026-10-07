[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..\..")).Path
$source = Join-Path $repoRoot "checkpoints\step-3-deliberate-bug\TicketEndpoints.cs"
$target = Join-Path $repoRoot "src\SupportApi\Endpoints\TicketEndpoints.cs"

if (-not (Test-Path -LiteralPath $source -PathType Leaf)) {
    throw "Demo checkpoint not found: $source"
}

if (Test-Path -LiteralPath $target -PathType Leaf) {
    $sourceHash = (Get-FileHash -LiteralPath $source -Algorithm SHA256).Hash
    $targetHash = (Get-FileHash -LiteralPath $target -Algorithm SHA256).Hash

    if ($sourceHash -eq $targetHash) {
        Write-Host "Security review demo is already staged at $target"
        exit 0
    }

    throw "Refusing to overwrite an existing TicketEndpoints.cs. Move or commit your file before staging the demo."
}

Copy-Item -LiteralPath $source -Destination $target
Write-Host "Staged the vulnerable endpoint at $target"
Write-Host "Run: dotnet test SupportApi.slnx"


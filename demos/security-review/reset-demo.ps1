[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot "..\..")).Path
$source = Join-Path $repoRoot "checkpoints\step-3-deliberate-bug\TicketEndpoints.cs"
$target = Join-Path $repoRoot "src\SupportApi\Endpoints\TicketEndpoints.cs"

if (-not (Test-Path -LiteralPath $target -PathType Leaf)) {
    Write-Host "Security review demo is already reset."
    exit 0
}

if (-not (Test-Path -LiteralPath $source -PathType Leaf)) {
    throw "Demo checkpoint not found: $source"
}

$sourceHash = (Get-FileHash -LiteralPath $source -Algorithm SHA256).Hash
$targetHash = (Get-FileHash -LiteralPath $target -Algorithm SHA256).Hash

if ($sourceHash -ne $targetHash) {
    throw "Refusing to delete TicketEndpoints.cs because it differs from the staged demo checkpoint."
}

Remove-Item -LiteralPath $target
Write-Host "Removed the staged vulnerable endpoint and restored the baseline source tree."


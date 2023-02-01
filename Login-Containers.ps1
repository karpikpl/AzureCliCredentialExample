$quote = "`"";

if ($PSEdition -eq "Desktop")
{
    $quote = "`\`""
}

$containers = docker ps --filter "label=hasazcli=true" --format "{{.ID}} {{.Names}}" | ConvertFrom-CSV -Header @("ID", "Name") -Delimiter " "

foreach($container in $containers)
{
    Write-Host "$($container.Name):"
    docker exec -it $($container.ID) bash -c "if az account show > /dev/null 2>&1; then echo $($quote)already logged in$($quote); else az login; fi"
}

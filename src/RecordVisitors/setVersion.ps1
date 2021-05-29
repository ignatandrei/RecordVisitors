
function WriteVersion($filename, $version){

Write-Host "Starting versioning of " $fileName " with version " $version 
$xml=[xml](get-content $fileName)
if ($xml.Project.PropertyGroup.Version -eq $null) 
{ 
        $xml.Project.PropertyGroup.AppendChild($xml.ImportNode(([xml]"<Version/>").DocumentElement,$true)) 
}

$xml.SelectNodes("/Project/PropertyGroup/Version")[0].InnerText = $version 
$xml.Save($fileName)

}

$version = Get-Date -Format "yyyy.MM.dd.Hmm"
Get-ChildItem -Path .\ -Filter *.csproj -Recurse -File -Name| ForEach-Object {

WriteVersion $_  $version
}

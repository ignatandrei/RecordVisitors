
$data1=@"
<!--<add name=                                />
    <add name=                                />
    <add name=                                />-->
a,b,c,d
1,2,3,4
"@

$state='content'
$data  -split "`n" |
ForEach-Object {
  If ($_ -match '^<!--') {
    $state='comment'
    return $null  # because `continue` doesn't work in a foreach-object
  }
  If ($_ -match '-->$') {
    $state='content'
    return $null
  }
  If ($state -eq 'content') {
    $_
  }
}


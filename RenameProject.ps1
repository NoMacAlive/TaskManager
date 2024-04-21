param (
    [string]$oldProjectName,
    [string]$newProjectName
)

# Rename solution file
Get-ChildItem *.sln | Rename-Item -NewName { $_.Name -replace $oldProjectName, $newProjectName }

# Rename project folder
Rename-Item $oldProjectName -NewName $newProjectName

# Rename project file
Get-ChildItem $newProjectName -Recurse -Filter *.csproj | Rename-Item -NewName { $_.Name -replace $oldProjectName, $newProjectName }

# Update solution file contents
(Get-Content "$newProjectName\$newProjectName.sln") | ForEach-Object {
    $_ -replace $oldProjectName, $newProjectName
} | Set-Content "$newProjectName\$newProjectName.sln"

# Update project file contents
(Get-Content "$newProjectName\$newProjectName.csproj") | ForEach-Object {
    $_ -replace $oldProjectName, $newProjectName
} | Set-Content "$newProjectName\$newProjectName.csproj"

# Update namespace in project files
Get-ChildItem $newProjectName -Recurse -Filter *.cs | ForEach-Object {
    (Get-Content $_.FullName) | ForEach-Object {
        $_ -replace "namespace $oldProjectName", "namespace $newProjectName"
    } | Set-Content $_.FullName
}

Write-Host "Project '$oldProjectName' has been renamed to '$newProjectName'."

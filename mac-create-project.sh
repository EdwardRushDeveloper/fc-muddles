
read -p "Enter the project name: " projectName

echo "ProjectName - $projectName"

dotnet new console -o "$projectName"
@echo off
nuget pack ..\Nancy\ELFinder.Connector.Nancy\ELFinder.Connector.Nancy.csproj -IncludeReferencedProjects -Prop Configuration=Release -OutputDirectory .\Release
nuget pack ..\ASP.NET\ELFinder.Connector.ASPNet\ELFinder.Connector.ASPNet.csproj -IncludeReferencedProjects -Prop Configuration=Release -OutputDirectory .\Release
nuget pack ..\Core\ELFinder.Connector\ELFinder.Connector.csproj -IncludeReferencedProjects -Prop Configuration=Release -OutputDirectory .\Release
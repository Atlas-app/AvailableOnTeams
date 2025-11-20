; =====================================================================
; AvailableOnTeams - Inno Setup installer script
; Creates a minimal installer with an optional "Run at Windows startup"
; =====================================================================

#define MyAppName "AvailableOnTeams"
#define MyExeName "Atlas.AvailableOnTeams.exe"

[Setup]
AppName={#MyAppName}
AppVersion=1.0.0
AppPublisher="Atlas"
DefaultDirName={autopf}\{#MyAppName}
OutputBaseFilename=AvailableOnTeamsSetup

SetupIconFile="Atlas.AvailableOnTeams.App\Assets\AvailableOnTeams.ico"

Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Files]
Source:"Atlas.AvailableOnTeams.App\bin\publish\{#MyExeName}"; DestDir:"{app}"; Flags: ignoreversion

[Tasks]
Name: "autostart"; Description: "Run AvailableOnTeams at Windows startup"; Flags: unchecked

[Icons]
Name:"{group}\{#MyAppName}"; Filename:"{app}\{#MyExeName}"
Name:"{userdesktop}\{#MyAppName}"; Filename:"{app}\{#MyExeName}"
Name:"{userstartup}\{#MyAppName}"; Filename:"{app}\{#MyExeName}"; Tasks: autostart

[Run]
Filename:"{app}\{#MyExeName}"; Description:"Launch application now"; Flags: nowait postinstall skipifsilent

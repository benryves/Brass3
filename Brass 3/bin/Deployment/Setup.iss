; Setup Script for Brass 3.
; Ben Ryves / Bee Development 2007.
[Setup]
AppName=Brass
AppVerName=Brass 3 Beta 4
AppPublisher=Bee Development
AppPublisherURL=http://www.bee-dev.com/?go=brass
AppSupportURL=http://www.bee-dev.com/?go=brass
AppUpdatesURL=http://www.bee-dev.com/?go=brass
DefaultDirName={pf}\Brass
DefaultGroupName=Brass
AllowNoIcons=yes
OutputDir=..\Deployment
OutputBaseFilename=BrassSetup
Compression=lzma
SolidCompression=yes
InfoBeforeFile=InfoBefore.rtf
InfoAfterFile=Release Notes.rtf
ChangesEnvironment=yes
ChangesAssociations=yes

[Files]
; Main executables:
Source: "..\Release\Brass.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: "Brass";
Source: "..\Release\Help.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: "Tools\Help";
Source: "..\Release\BrassGuiBuild.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: "Tools\GuiBuilder";

; Bundled plugins:
Source: "..\Release\Core.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Core";
Source: "..\Release\Z80.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Z80";
Source: "..\Release\Chip8.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Chip8";
Source: "..\Release\TiCalc.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\TiCalc";
Source: "..\Release\Sega.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Sega";
Source: "..\Release\Variables.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Variables";
Source: "..\Release\Legacy.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Legacy";
Source: "..\Release\ImageManipulation.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\ImageManipulation";

; Includes:
Source: "Include\TI\ti83plus.inc"; DestDir: "{app}\Include\TI"; Flags: ignoreversion; Components: "Include\TI\ti83plus";
Source: "Include\TI\ti73.inc"; DestDir: "{app}\Include\TI"; Flags: ignoreversion; Components: "Include\TI\ti73";
Source: "Include\TI\ti83.inc"; DestDir: "{app}\Include\TI"; Flags: ignoreversion; Components: "Include\TI\ti83";
Source: "Include\TI\ion83.inc"; DestDir: "{app}\Include\TI"; Flags: ignoreversion; Components: "Include\TI\TI83\Ion";
Source: "Include\TI\venus.inc"; DestDir: "{app}\Include\TI"; Flags: ignoreversion; Components: "Include\TI\TI83\Venus";
Source: "Include\TI\ion8x.inc"; DestDir: "{app}\Include\TI"; Flags: ignoreversion; Components: "Include\TI\TI83Plus\Ion";
Source: "Include\TI\mirage.inc"; DestDir: "{app}\Include\TI"; Flags: ignoreversion; Components: "Include\TI\TI83Plus\Mirage";
Source: "Include\TI\dcs6.inc"; DestDir: "{app}\Include\TI"; Flags: ignoreversion; Components: "Include\TI\TI83Plus\DoorsCS";

; Templates:
Source: "Templates\TI\Program.brassproj"; DestDir: "{app}\Templates\TI"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Templates\TI\Resources\Icons\MirageOS.gif"; DestDir: "{app}\Templates\TI\Resources\Icons"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Templates\TI\Resources\Icons\Venus.gif"; DestDir: "{app}\Templates\TI\Resources\Icons"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Templates\TI\Resources\Icons\DoorsCS.gif"; DestDir: "{app}\Templates\TI\Resources\Icons"; Flags: ignoreversion; Components: "Templates\TI\Program";

Source: "Templates\SMS\32KB.brassproj"; DestDir: "{app}\Templates\SMS"; Flags: ignoreversion; Components: "Templates\SMS\32KB";

; Samples:
Source: "Samples\TI Calculator Program\Demo.brassproj"; DestDir: "{userdocs}\Brass Projects\Samples\TI Calculator Program"; Flags: ignoreversion; Components: "Samples\TIProgram";
Source: "Samples\TI Calculator Program\Demo.asm"; DestDir: "{userdocs}\Brass Projects\Samples\TI Calculator Program"; Flags: ignoreversion; Components: "Samples\TIProgram";

Source: "Samples\Sega Master System Program\Registers.brassproj"; DestDir: "{userdocs}\Brass Projects\Samples\Sega Master System Program"; Flags: ignoreversion; Components: "Samples\SMS32KB";
Source: "Samples\Sega Master System Program\Registers.asm"; DestDir: "{userdocs}\Brass Projects\Samples\Sega Master System Program"; Flags: ignoreversion; Components: "Samples\SMS32KB";
Source: "Samples\Sega Master System Program\Video.asm"; DestDir: "{userdocs}\Brass Projects\Samples\Sega Master System Program"; Flags: ignoreversion; Components: "Samples\SMS32KB";
Source: "Samples\Sega Master System Program\Font.inc"; DestDir: "{userdocs}\Brass Projects\Samples\Sega Master System Program"; Flags: ignoreversion; Components: "Samples\SMS32KB";
Source: "Samples\Sega Master System Program\Resources.inc"; DestDir: "{userdocs}\Brass Projects\Samples\Sega Master System Program"; Flags: ignoreversion; Components: "Samples\SMS32KB";

; Documentation:
Source: "Release Notes.rtf"; DestDir: "{app}\Documentation"; Flags: ignoreversion;

[Components]
Name: "Brass"; Description: "Brass Compiler"; Types: full compact custom; Flags: fixed;

Name: "Tools"; Description: "Tools"; Types: full custom;
Name: "Tools\Help"; Description: "Help Viewer"; Types: full custom;
Name: "Tools\GuiBuilder"; Description: "GUI Builder"; Types: full custom;

Name: "Samples"; Description: "Projects Directory"; Types: full custom;
Name: "Samples\TIProgram"; Description: "Sample Texas Instruments Calculator Program"; Types: full custom;
Name: "Samples\SMS32KB"; Description: "Sample Sega Master System Program"; Types: full custom;

Name: "Plugins"; Description: "Plugins"; Types: full compact custom;
Name: "Plugins\Core"; Description: "Core Collection"; Types: full compact custom;
Name: "Plugins\Z80"; Description: "Z80 Assembler"; Types: full custom;
Name: "Plugins\Chip8"; Description: "Chip-8 Assembler"; Types: full custom;
Name: "Plugins\TiCalc"; Description: "Texas Instruments Calculator Collection"; Types: full custom;
Name: "Plugins\Sega"; Description: "Sega 8-bit Collection"; Types: full custom;
Name: "Plugins\Variables"; Description: "Variable Allocation"; Types: full custom;
Name: "Plugins\Legacy"; Description: "Legacy Collection"; Types: full custom;
Name: "Plugins\ImageManipulation"; Description: "Image Manipulation"; Types: full custom;

Name: "Include"; Description: "Include Files"; Types: full custom;
Name: "Include\TI"; Description: "Texas Instruments Calculators"; Types: full custom;
Name: "Include\TI\TI83"; Description: "TI-83"; Types: full custom;
Name: "Include\TI\TI83\Ion"; Description: "Ion"; Types: full custom;
Name: "Include\TI\TI83\Venus"; Description: "Venus"; Types: full custom;
Name: "Include\TI\TI83plus"; Description: "TI-83 Plus"; Types: full custom;
Name: "Include\TI\TI83Plus\Ion"; Description: "Ion"; Types: full custom;
Name: "Include\TI\TI83Plus\Mirage"; Description: "MirageOS"; Types: full custom;
Name: "Include\TI\TI83Plus\DoorsCS"; Description: "DoorsCS"; Types: full custom;
Name: "Include\TI\TI73"; Description: "TI-73"; Types: full custom;

Name: "Templates"; Description: "Project Templates"; Types: full custom;
Name: "Templates\TI"; Description: "Texas Instruments Calculators"; Types: full custom;
Name: "Templates\TI\Program"; Description: "TI-83/TI-83 Plus Assembly Program"; Types: full custom;
Name: "Templates\SMS"; Description: "Sega Master System"; Types: full custom;
Name: "Templates\SMS\32KB"; Description: "32KB Master System / Game Gear ROM"; Types: full custom;


[Registry]
; Register .brassproj file type:
Root: HKCR; Subkey: ".brassproj"; ValueType: string; ValueData: "Brass.Project";
Root: HKCR; Subkey: "Brass.Project"; ValueType: string; ValueData: "Brass Project";
Root: HKCR; Subkey: "Brass.Project\shell\Build\command"; ValueType: string; ValueData: """{app}\BrassGuiBuild.exe"" ""%1"""; Components: "Tools\GuiBuilder";

; Register environment variables:
Root: HKLM; Subkey: "System\CurrentControlSet\Control\Session Manager\Environment"; ValueName: "Brass"; ValueType: string; ValueData: "{app}";
Root: HKLM; Subkey: "System\CurrentControlSet\Control\Session Manager\Environment"; ValueName: "Brass.Include"; ValueType: string; ValueData: "{app}\Include";
Root: HKLM; Subkey: "System\CurrentControlSet\Control\Session Manager\Environment"; ValueName: "Brass.Templates"; ValueType: string; ValueData: "{app}\Templates";

[INI]
Filename: "{app}\Documentation\Brass Website.url"; Section: "InternetShortcut"; Key: "URL"; String: "http://www.bee-dev.com/?go=brass";

[Icons]
Name: "{group}\Release Notes"; Filename: "{app}\Documentation\Release Notes.rtf";
Name: "{group}\Brass Manual"; Filename: "{app}\Help.exe"; Components: "Tools\Help";
Name: "{group}\Projects Directory"; Filename: "{userdocs}\Brass Projects"; Components: "Samples";
Name: "{group}\{cm:ProgramOnTheWeb,Brass}"; Filename: "{app}\Documentation\Brass Website.url";
Name: "{group}\{cm:UninstallProgram,Brass}"; Filename: "{uninstallexe}";

[UninstallDelete]
Type: files; Name: "{app}\Documentation\Brass Website.url";

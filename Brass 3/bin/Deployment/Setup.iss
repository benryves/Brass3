; Setup Script for Brass 3.
; Ben Ryves / Bee Development 2007-2019.
[Setup]
AppName=Brass
AppVerName=Brass 3 Beta 14
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
Source: "..\Release\Skybound.VisualStyles.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Tools\Help";
Source: "..\Release\ProjectBuilder.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: "Tools\ProjectBuilder";
Source: "..\Release\ProjectBuilder.exe.config"; DestDir: "{app}"; Flags: ignoreversion; Components: "Tools\ProjectBuilder";

; Bundled plugins:
Source: "..\Release\Core.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Core";
Source: "..\Release\Z80.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Z80";
Source: "..\Release\Chip8.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Chip8";
Source: "..\Release\TiCalc.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\TiCalc";
Source: "..\Release\Sega.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Sega";
Source: "..\Release\Scripting.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Scripting";
Source: "..\Release\Variables.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Variables";
Source: "..\Release\Legacy.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\Legacy";
Source: "..\Release\ImageManipulation.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: "Plugins\ImageManipulation";


; Bundled debuggers:
Source: "..\Release\PindurTI Debugger.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: "Debuggers\PindurTI";
Source: "..\Release\pindurti.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: "Debuggers\PindurTI";
Source: "..\Release\PindurTI Debugger.exe.config"; DestDir: "{app}"; Flags: ignoreversion; Components: "Debuggers\PindurTI";


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

Source: "Debug\TI\ROMs\ROMs.txt"; DestDir: "{app}\Debug\TI\ROMs"; Flags: ignoreversion; Components: "Templates\TI";
Source: "Debug\TI\TI8X.debug"; DestDir: "{app}\Debug\TI"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Debug\TI\TI8X.Ion.debug"; DestDir: "{app}\Debug\TI"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Debug\TI\TI8X.MirageOS.debug"; DestDir: "{app}\Debug\TI"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Debug\TI\TI83.debug"; DestDir: "{app}\Debug\TI"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Debug\TI\TI83.Ion.debug"; DestDir: "{app}\Debug\TI"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Debug\TI\TI83.Venus.debug"; DestDir: "{app}\Debug\TI"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Debug\TI\Shells\Ion.8xg"; DestDir: "{app}\Debug\TI\Shells"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Debug\TI\Shells\Ion.83g"; DestDir: "{app}\Debug\TI\Shells"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Debug\TI\Shells\MirageOS.8xk"; DestDir: "{app}\Debug\TI\Shells"; Flags: ignoreversion; Components: "Templates\TI\Program";
Source: "Debug\TI\Shells\Venus.83g"; DestDir: "{app}\Debug\TI\Shells"; Flags: ignoreversion; Components: "Templates\TI\Program";

Source: "Templates\TI\Application.brassproj"; DestDir: "{app}\Templates\TI"; Flags: ignoreversion; Components: "Templates\TI\Application";

Source: "Templates\SMS\32KB.brassproj"; DestDir: "{app}\Templates\SMS"; Flags: ignoreversion; Components: "Templates\SMS\32KB";

; Samples:
Source: "Samples\TI Calculator Program\Demo.brassproj"; DestDir: "{userdocs}\Brass Projects\Samples\TI Calculator Program"; Flags: ignoreversion; Components: "Samples\TIProgram";
Source: "Samples\TI Calculator Program\Demo.asm"; DestDir: "{userdocs}\Brass Projects\Samples\TI Calculator Program"; Flags: ignoreversion; Components: "Samples\TIProgram";

Source: "Samples\TI Calculator Application\Demo.brassproj"; DestDir: "{userdocs}\Brass Projects\Samples\TI Calculator Application"; Flags: ignoreversion; Components: "Samples\TIApplication";
Source: "Samples\TI Calculator Application\Demo.asm"; DestDir: "{userdocs}\Brass Projects\Samples\TI Calculator Application"; Flags: ignoreversion; Components: "Samples\TIApplication";

Source: "Samples\Sega Master System Program\Registers.brassproj"; DestDir: "{userdocs}\Brass Projects\Samples\Sega Master System Program"; Flags: ignoreversion; Components: "Samples\SMS32KB";
Source: "Samples\Sega Master System Program\Registers.asm"; DestDir: "{userdocs}\Brass Projects\Samples\Sega Master System Program"; Flags: ignoreversion; Components: "Samples\SMS32KB";
Source: "Samples\Sega Master System Program\Video.asm"; DestDir: "{userdocs}\Brass Projects\Samples\Sega Master System Program"; Flags: ignoreversion; Components: "Samples\SMS32KB";
Source: "Samples\Sega Master System Program\Font.inc"; DestDir: "{userdocs}\Brass Projects\Samples\Sega Master System Program"; Flags: ignoreversion; Components: "Samples\SMS32KB";
Source: "Samples\Sega Master System Program\Resources.inc"; DestDir: "{userdocs}\Brass Projects\Samples\Sega Master System Program"; Flags: ignoreversion; Components: "Samples\SMS32KB";

; Documentation:
Source: "Release Notes.rtf"; DestDir: "{app}\Documentation"; Flags: ignoreversion;

; Developer Notes:
Source: "..\Release\Brass.chm"; DestDir: "{app}\Documentation"; Flags: ignoreversion; Components: "Developer\Reference";
Source: "..\Release\Brass.xml"; DestDir: "{app}"; Flags: ignoreversion; Components: "Developer\XML";

; Translations:
Source: "..\Release\nl\Brass.resources.dll"; DestDir: "{app}\nl"; Flags: ignoreversion; Components: "Translations\Dutch";

[Components]
Name: "Brass"; Description: "Brass Compiler"; Types: full compact custom; Flags: fixed;

Name: "Tools"; Description: "Tools"; Types: full custom;
Name: "Tools\Help"; Description: "Help Viewer"; Types: full custom;
Name: "Tools\ProjectBuilder"; Description: "Project Builder"; Types: full custom;

Name: "Debuggers"; Description: "Debuggers"; Types: full custom;
Name: "Debuggers\PindurTI"; Description: "Pindur TI"; Types: full custom;

Name: "Samples"; Description: "Projects Directory"; Types: full custom;
Name: "Samples\TIProgram"; Description: "Sample Texas Instruments Calculator Program"; Types: full custom;
Name: "Samples\TIApplication"; Description: "Sample Texas Instruments Calculator Application"; Types: full custom;
Name: "Samples\SMS32KB"; Description: "Sample Sega Master System Program"; Types: full custom;

Name: "Plugins"; Description: "Plugins"; Types: full compact custom;
Name: "Plugins\Core"; Description: "Core Collection"; Types: full compact custom;
Name: "Plugins\Z80"; Description: "Z80 Assembler"; Types: full custom;
Name: "Plugins\Chip8"; Description: "Chip-8 Assembler"; Types: full custom;
Name: "Plugins\TiCalc"; Description: "Texas Instruments Calculator Collection"; Types: full custom;
Name: "Plugins\Sega"; Description: "Sega 8-bit Collection"; Types: full custom;
Name: "Plugins\Scripting"; Description: "Scripting"; Types: full custom;
Name: "Plugins\Variables"; Description: "Variable Allocation"; Types: full custom;
Name: "Plugins\Legacy"; Description: "Legacy Collection"; Types: full custom;
Name: "Plugins\ImageManipulation"; Description: "Image Manipulation"; Types: full custom;

Name: "Translations"; Description: "Translations"; Types: full custom;
Name: "Translations\Dutch"; Description: "Dutch"; Types: full custom;

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
Name: "Templates\TI\Application"; Description: "TI-73/TI-83 Plus Application"; Types: full custom;
Name: "Templates\SMS"; Description: "Sega Master System"; Types: full custom;
Name: "Templates\SMS\32KB"; Description: "32KB Master System / Game Gear ROM"; Types: full custom;

Name: "Developer"; Description: "Developer Information"; Types: full custom;
Name: "Developer\Reference"; Description: "Class Library Reference"; Types: full custom;
Name: "Developer\XML"; Description: "XML Documentation Comments"; Types: full custom;


[Registry]
; Register .brassproj file type:
Root: HKCR; Subkey: ".brassproj"; ValueType: string; ValueData: "Brass.Project";
Root: HKCR; Subkey: "Brass.Project"; ValueType: string; ValueData: "Brass Project";
Root: HKCR; Subkey: "Brass.Project\shell\Build\command"; ValueType: string; ValueData: """{app}\ProjectBuilder.exe"" ""%1"""; Components: "Tools\ProjectBuilder";

; Register environment variables:
Root: HKLM; Subkey: "System\CurrentControlSet\Control\Session Manager\Environment"; ValueName: "Brass"; ValueType: string; ValueData: "{app}";
Root: HKLM; Subkey: "System\CurrentControlSet\Control\Session Manager\Environment"; ValueName: "Brass.Include"; ValueType: string; ValueData: "{app}\Include";
Root: HKLM; Subkey: "System\CurrentControlSet\Control\Session Manager\Environment"; ValueName: "Brass.Templates"; ValueType: string; ValueData: "{app}\Templates";
Root: HKLM; Subkey: "System\CurrentControlSet\Control\Session Manager\Environment"; ValueName: "Brass.Debug"; ValueType: string; ValueData: "{app}\Debug";

[INI]
Filename: "{app}\Documentation\Brass Website.url"; Section: "InternetShortcut"; Key: "URL"; String: "http://www.bee-dev.com/?go=brass";

[Icons]
Name: "{group}\Documentation\Release Notes"; Filename: "{app}\Documentation\Release Notes.rtf";
Name: "{group}\Brass Manual"; Filename: "{app}\Help.exe"; Components: "Tools\Help";
Name: "{group}\Projects Directory"; Filename: "{userdocs}\Brass Projects"; Components: "Samples";
Name: "{group}\Brass Project Builder"; Filename: "{app}\ProjectBuilder.exe"; Components: "Tools\ProjectBuilder";
Name: "{group}\Developer Tools\Class Library Reference"; Filename: "{app}\Documentation\Brass.chm"; Components: "Developer\Reference";
Name: "{group}\{cm:ProgramOnTheWeb,Brass}"; Filename: "{app}\Documentation\Brass Website.url";
Name: "{group}\{cm:UninstallProgram,Brass}"; Filename: "{uninstallexe}";

[UninstallDelete]
Type: files; Name: "{app}\Documentation\Brass Website.url";

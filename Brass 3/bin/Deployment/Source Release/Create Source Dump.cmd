del Source.zip
set path=%path%;"C:\Program Files\7-Zip"
svn co "file:///D:/SVN/Brass 3" -q SVNDump
robocopy SVNDump CleanDump /e /a+:r /xd .svn
pushd CleanDump
7z u -tzip -mx=9 ..\Source.zip *
popd
rmdir SVNDump /s /q
rmdir CleanDump /s /q
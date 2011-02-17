if '' == '%1' goto notgood
if exist .git goto done
  call git init .
  call attrib -h .git
  call git config user.name "p4p4"
  call git config user.email p4p4p4@web.de
  call echo. >> Readme.txt
  call git add Readme.txt
  call git commit -m "first commit"
  call git remote add origin git@github.com:p4p4/%1.git
  goto fine
  :: git push origin master
:done
  echo work already done!
:notgood
  echo no rgs given ! plz gv project name
:fine

@ECHO OFF
chcp 1252
CLS
COLOR 1F

:MENU
CLS
COLOR 1F

ECHO ============= MENU NAME =============
ECHO -------------------------------------
ECHO  1 - Delete obj/bin solution
ECHO  2 - Delete packages solution
ECHO  3 - Selection 3
ECHO  4 - Selection 4
ECHO  5 - Selection 5
ECHO  6 - Selection 6
ECHO  7 - Selection 7
ECHO -------------------------------------
ECHO  8 - Selection 8
ECHO -------------------------------------
ECHO  9 - Selection 9
ECHO -------------------------------------
ECHO ==========PRESS 'Q' TO QUIT==========
ECHO.

SET INPUT=
SET /P INPUT=Please select a number:

IF /I '%INPUT%'=='1' GOTO Selection1
IF /I '%INPUT%'=='2' GOTO Selection2
IF /I '%INPUT%'=='3' GOTO Selection3
IF /I '%INPUT%'=='4' GOTO Selection4
IF /I '%INPUT%'=='5' GOTO Selection5
IF /I '%INPUT%'=='6' GOTO Selection6
IF /I '%INPUT%'=='7' GOTO Selection7
IF /I '%INPUT%'=='8' GOTO Selection8
IF /I '%INPUT%'=='9' GOTO Selection9
IF /I '%INPUT%'=='Q' GOTO Quit

CLS

ECHO ============INVALID INPUT============
ECHO -------------------------------------
ECHO Please select a number from the Main
echo Menu [1-9] or select 'Q' to quit.
ECHO -------------------------------------
ECHO ======PRESS ANY KEY TO CONTINUE======

PAUSE > NUL
GOTO MENU

:Selection1

for /d /r . %%d in (bin,obj,Debug) do @if exist "%%d" rd /s/q "%%d"


GOTO Finalize


:Selection2

for /d /r . %%d in (packages) do @if exist "%%d" rd /s/q "%%d"

GOTO Finalize

:Selection3

color 4C

PAUSE > NUL
GOTO MENU

:Selection4

color 5D

PAUSE > NUL
GOTO MENU

:Selection5

color 0A

PAUSE > NUL
GOTO MENU

:Selection6

color 6E

PAUSE > NUL
GOTO MENU

:Selection7

color 5D

PAUSE > NUL
GOTO MENU

:Selection8

color 3F

PAUSE > NUL
GOTO MENU

:Selection9

color 19

PAUSE > NUL
GOTO MENU

:Quit
CLS

ECHO ==============THANKYOU===============
ECHO -------------------------------------
ECHO ======PRESS ANY KEY TO CONTINUE======

PAUSE>NUL
EXIT

:Finalize
CLS

ECHO ==============THANKYOU===============
ECHO -------------------------------------
ECHO		Command executed success
ECHO -------------------------------------
ECHO ======PRESS ANY KEY TO CONTINUE======

PAUSE > NUL
GOTO MENU
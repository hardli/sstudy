del/s *.dcu
del/s *.~dfm
del/s *.~pas
del/s *.~ddp
del/s *.ddp
del/s *.~dpr
del/s *.~pa
del/s *.dof
del/s *.cfg
del/s *.RVF
del/s *.TBF
del/s *.TMS
del/s *.TXTF
del/s *.TXTS
del/s *.pj
del/s *.json
del/s *.ttme
del/s *.tcho
del/s *.tnol
del/s *.tclo
del LogFiles\*.txt

set saveini=Setup.ini

echo [Server] >%saveini%
set saveValue=
echo ip=%saveValue% >>%saveini%
echo port1=6001 >>%saveini%
echo port2=6002 >>%saveini%
echo port3=6003 >>%saveini%
echo refresh=100 >>%saveini%

echo [Send] >>%saveini%
echo sendtime=2 >>%saveini%
echo refresh=100 >>%saveini%
echo singalsend=1 >>%saveini%

echo [NetTime] >>%saveini%
echo urltime= >>%saveini%
echo autotime=0 >>%saveini%

echo [Other] >>%saveini%
echo noblank=1 >>%saveini%
echo Language=0 >>%saveini%
echo useuser=0 >>%saveini%
echo ncurtxtfOrd=0 >>%saveini%
echo ncurtxtsOrd=0 >>%saveini%
echo ncurtmOrd=0 >>%saveini%
echo ncurtbfOrd=0 >>%saveini%

echo [Advanvc_Area] >>%saveini%
echo bShow_Area_Temperature=0 >>%saveini%
echo bShow_Area_Humidity=0 >>%saveini%
echo bShow_Area_Noise=0 >>%saveini%

echo [SendStyle] >>%saveini%
echo nSendStyle=2 >>%saveini%
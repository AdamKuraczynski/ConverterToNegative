.code 
MyProc1 proc 

; x1 	  RCX
; x2 	  RDX
; wyjœcie RAX

mov RAX, 100	;100 do RAX
sub RAX, RDX	;100 - x2
imul RAX, RCX	;(100 - x2) * x1

mov R8, 255	;255 do RBX
sub R8, RCX	;255 - x1
imul R8, RDX	;(255 - x1) * x2

add RAX, R8	;(100 - x2) * x1 + (255 - x1) * x2

sub RDX, RDX	;zerowanie RDX
mov RCX, 100	;100 do RCX

div RCX		;((100 - x2) * x1 + (255 - x1) * x2) / 100

ret 
MyProc1 endp 
end